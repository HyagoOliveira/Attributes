using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Finds custom property drawer for a given <see cref="SerializedProperty"/>.
    /// Code modified from https://gist.github.com/wappenull/2391b3c23dd20ede74483d0da4cab3f1
    /// </summary>
    internal static class PropertyDrawerFinder
    {
        struct TypeAndFieldInfo
        {
            internal Type type;
            internal FieldInfo fi;
        }

        private static readonly Dictionary<int, TypeAndFieldInfo> pathHashVsType = new();
        private static readonly Dictionary<Type, PropertyDrawer> typeVsDrawerCache = new();

        public static bool TryFindCustomDrawer(SerializedProperty property, out PropertyDrawer drawer)
        {
            drawer = FindCustomDrawer(property);
            return drawer != null;
        }

        /// <summary>
        /// Searches the custom Property Drawer for the given serialized property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns>The PropertyDrawer or null if no custom property drawer was found.</returns>
        public static PropertyDrawer FindCustomDrawer(SerializedProperty property)
        {
            var pathHash = GetUniquePropertyPathHash(property);

            if (!pathHashVsType.TryGetValue(pathHash, out TypeAndFieldInfo tfi))
            {
                tfi.type = GetPropertyType(property, out tfi.fi);
                pathHashVsType[pathHash] = tfi;
            }

            if (tfi.type == null) return null;

            if (!typeVsDrawerCache.TryGetValue(tfi.type, out PropertyDrawer drawer))
            {
                drawer = FindDrawer(tfi.type);
                typeVsDrawerCache.Add(tfi.type, drawer);
            }

            if (drawer != null)
            {
                // Drawer created by custom way like this will not have "fieldInfo" field installed.
                // It is an optional, but some user code in advanced drawer might use it.
                // To install it, we must use reflection again, the backing field name is "internal FieldInfo m_FieldInfo"
                // See ref file in UnityCsReference (2019) project. Note that name could changed in future update.
                // unitycsreference\Editor\Mono\ScriptAttributeGUI\PropertyDrawer.cs
                var fieldInfoBacking = typeof(PropertyDrawer).GetField("m_FieldInfo", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfoBacking?.SetValue(drawer, tfi.fi);
            }

            return drawer;
        }

        /// <summary>
        /// Returns custom property drawer for type if one could be found, or null if
        /// no custom property drawer could be found. Does not use cached values, so it's resource intensive.
        /// </summary>
        public static PropertyDrawer FindDrawer(Type propertyType)
        {
            var cpdType = typeof(CustomPropertyDrawer);
            var typeField = cpdType.GetField("m_Type", BindingFlags.NonPublic | BindingFlags.Instance);
            var childField = cpdType.GetField("m_UseForChildren", BindingFlags.NonPublic | BindingFlags.Instance);

            // Optimization note:
            // For benchmark (on DungeonLooter 0.8.4)
            // - Original, search all assemblies and classes: 250 msec
            // - optimized: search only specific name assembly and classes: 5 msec
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                // optimization: filter only "*Editor" assembly
                if (!assem.FullName.Contains("Editor")) continue;

                foreach (Type candidate in assem.GetTypes())
                {
                    // optimization: filter only "*Drawer" class name, like "SomeTypeDrawer"
                    if (!candidate.Name.Contains("Drawer")) continue;

                    // See if this is a class that has [CustomPropertyDrawer( typeof( T ) )]
                    foreach (Attribute attribute in candidate.GetCustomAttributes(typeof(CustomPropertyDrawer)))
                    {
                        if (attribute.GetType().IsSubclassOf(typeof(CustomPropertyDrawer)) || attribute.GetType() == typeof(CustomPropertyDrawer))
                        {
                            var drawerAttribute = (CustomPropertyDrawer)attribute;
                            var drawerType = (Type)typeField.GetValue(drawerAttribute);
                            var isValidAttribute = drawerType == propertyType ||
                                ((bool)childField.GetValue(drawerAttribute) && propertyType.IsSubclassOf(drawerType)) ||
                                ((bool)childField.GetValue(drawerAttribute) && IsGenericSubclass(drawerType, propertyType));

                            if (isValidAttribute && candidate.IsSubclassOf(typeof(PropertyDrawer)))
                            {
                                // Technical note: PropertyDrawer.fieldInfo will not available via this drawer
                                // It has to be manually setup by caller.
                                var drawer = (PropertyDrawer)Activator.CreateInstance(candidate);
                                return drawer;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static Type GetPropertyType(SerializedProperty property, out FieldInfo fi)
        {
            // To see real property type, must dig into object that hosts it.
            GetPropertyFieldInfo(property, out Type resolvedType, out fi);
            return resolvedType;
        }

        private static int GetUniquePropertyPathHash(SerializedProperty property)
        {
            int hash = property.serializedObject.targetObject.GetType().GetHashCode();
            hash += property.propertyPath.GetHashCode();
            return hash;
        }

        private static void GetPropertyFieldInfo(SerializedProperty property, out Type resolvedType, out FieldInfo fi)
        {
            string[] fullPath = property.propertyPath.Split('.');

            // fi is FieldInfo in perspective of parentType (property.serializedObject.targetObject)
            // NonPublic to support [SerializeField] vars
            Type parentType = property.serializedObject.targetObject.GetType();
            fi = parentType.GetField(fullPath[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            resolvedType = fi.FieldType;

            for (int i = 1; i < fullPath.Length; i++)
            {
                // To properly handle array and list
                // This has deeper rabbit hole, see
                // unitycsreference\Editor\Mono\ScriptAttributeGUI\ScriptAttributeUtility.cs GetFieldInfoFromPropertyPath
                // here we will simplify it for now (could break)

                // If we are at 'Array' section like in `tiles.Array.data[0].tilemodId`
                if (IsArrayPropertyPath(fullPath, i))
                {
                    if (fi.FieldType.IsArray)
                        resolvedType = fi.FieldType.GetElementType();
                    else if (IsListType(fi.FieldType, out Type underlying))
                        resolvedType = underlying;

                    i++; // skip also the 'data[x]' part
                    // In this case, fi is not updated, FieldInfo stay the same pointing to 'tiles' part
                }
                else
                {
                    fi = resolvedType.GetField(fullPath[i]);
                    resolvedType = fi.FieldType;
                }
            }
        }

        private static bool IsArrayPropertyPath(string[] fullPath, int i)
        {
            // Also search for array pattern, thanks user https://gist.github.com/kkolyan
            // like `tiles.Array.data[0].tilemodId`
            // This is just a quick check, actual check in Unity uses RegEx
            if (fullPath[i] == "Array" && i + 1 < fullPath.Length && fullPath[i + 1].StartsWith("data"))
                return true;
            return false;
        }

        private static bool IsListType(Type t, out Type containedType)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
            {
                containedType = t.GetGenericArguments()[0];
                return true;
            }

            containedType = null;
            return false;
        }

        private static bool IsGenericSubclass(Type parent, Type child)
        {
            if (!parent.IsGenericType) return false;

            var currentType = child;
            var isAccessor = false;
            while (!isAccessor && currentType != null)
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == parent.GetGenericTypeDefinition())
                {
                    isAccessor = true;
                    break;
                }
                currentType = currentType.BaseType;
            }
            return isAccessor;
        }
    }
}