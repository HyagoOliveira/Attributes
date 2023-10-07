using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    public abstract class AbstractComparableAttributeDrawer<T> : PropertyDrawer where T : AbstractComparableAttribute
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var comparableAttibute = attribute as T;
            var comparableProperty = FindPropertyByPath(property, comparableAttibute.property);

            if (comparableProperty == null)
            {
                Debug.LogErrorFormat(
                    "The isn't any serialized '{0}' property present on '{1}'",
                    comparableAttibute.property,
                    property.serializedObject.targetObject.name
                );
                return;
            }

            var comparableField = ComparableAttributeFactory.Create(comparableAttibute, comparableProperty);
            DrawProperty(comparableField.HasMetCondition(), position, property, label);
        }

        protected abstract void DrawProperty(bool isConditionMet, Rect position, SerializedProperty property, GUIContent label);

        private SerializedProperty FindPropertyByPath(SerializedProperty property, string name)
        {
            const char pathSeparator = '.';
            var pathSuffix = pathSeparator + property.name;
            var newPathSuffix = pathSeparator + name;
            var path = ReplaceLastPath(property.propertyPath, pathSuffix, newPathSuffix);
            return property.serializedObject.FindProperty(path);
        }

        private static string ReplaceLastPath(string source, string oldValue, string newValue)
        {
            var place = source.LastIndexOf(oldValue);
            if (place == -1) return newValue.Replace(".", string.Empty);
            return source.Remove(place, oldValue.Length).Insert(place, newValue);
        }
    }
}