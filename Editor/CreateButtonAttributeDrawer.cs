using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="CreateButtonAttribute"/>.
    /// <para>Adds a Create Button if no reference is set.</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(CreateButtonAttribute))]
    public sealed class CreateButtonAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var hasValue = property.objectReferenceValue != null;
            var isObjectReferenceType = property.propertyType == SerializedPropertyType.ObjectReference;
            var drawCreateButton = isObjectReferenceType && !hasValue;

            if (!drawCreateButton)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            const float space = 2F;
            var buttonSize = position.height + 4F;

            position.width -= buttonSize + space;

            EditorGUI.PropertyField(position, property, label);

            position.x += position.width + space;
            position.width = buttonSize;

            var wasCreateButtonDown = GUI.Button(position, EditorGUIUtility.IconContent("CreateAddNew"), GUI.skin.button);
            if (wasCreateButtonDown)
            {
                var createButtonAttribute = attribute as CreateButtonAttribute;
                property.objectReferenceValue = SaveAsset(createButtonAttribute);
            }
        }

        private static Object SaveAsset(CreateButtonAttribute attribute)
        {
            var name = attribute.Type.Name;
            var path = EditorUtility.SaveFilePanelInProject(
                title: "Save " + name,
                defaultName: name,
                extension: "asset",
                message: "Please enter a filename to save the asset.",
                attribute.Path
            ).Trim();

            var invalidPath = string.IsNullOrEmpty(path);
            if (invalidPath) return null;

            var settings = ScriptableObject.CreateInstance(attribute.Type);

            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();

            Selection.activeObject = settings;
            return settings;
        }
    }
}