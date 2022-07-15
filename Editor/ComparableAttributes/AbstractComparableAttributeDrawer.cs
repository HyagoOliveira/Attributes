using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    public abstract class AbstractComparableAttributeDrawer<T> : PropertyDrawer where T : AbstractComparableAttribute
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var comparableAttibute = attribute as T;
            var comparableProperty = property.serializedObject.FindProperty(comparableAttibute.property);

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

        protected abstract void DrawProperty(bool isCoditionMet, Rect position, SerializedProperty property, GUIContent label);
    }
}