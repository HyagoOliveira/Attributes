using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom Property Drawer for <see cref="RequiredAttribute"/>.
    /// <para>Draws an error message bellow the attribute if its value is not set, null or empty.</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public sealed class RequiredAttributeDrawer : PropertyDrawer
    {
        private bool isNullValue = false;
        private const float HELP_BOX_HEIGHT = 32F;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + GetHelpBoxHeight();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = base.GetPropertyHeight(property, label);

            EditorGUI.PropertyField(position, property, label);

            if (isNullValue = IsPropertyValueNotSet(property))
            {
                var requiredAttribute = attribute as RequiredAttribute;
                var hasAttributeMessage = !string.IsNullOrEmpty(requiredAttribute.message);
                var message = hasAttributeMessage ?
                    requiredAttribute.message :
                    string.Format("Field '{0}' requires a value!", property.displayName);

                position.y += position.height + 2f;
                position.height = HELP_BOX_HEIGHT;
                EditorGUI.HelpBox(position, message, MessageType.Error);
            }
        }

        private bool IsPropertyValueNotSet(SerializedProperty property)
        {
            return
                property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue) ||
                property.propertyType == SerializedPropertyType.ExposedReference && property.exposedReferenceValue == null ||
                property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null;
        }

        private float GetHelpBoxHeight()
        {
            return isNullValue ? HELP_BOX_HEIGHT + 2f : 0f;
        }
    }
}