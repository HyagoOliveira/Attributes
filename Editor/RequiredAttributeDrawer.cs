using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom Property Drawer for <see cref="RequiredAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Draws an error message bellow the attribute if its value is not set, null or empty.
    /// </remarks>
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public sealed class RequiredAttributeDrawer : PropertyDrawer
    {
        private bool isNullValue = false;
        private const float helpBoxHeight = 32F;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) + GetHelpBoxHeight();

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
                position.height = helpBoxHeight;
                EditorGUI.HelpBox(position, message, MessageType.Error);
            }
        }

        private bool IsPropertyValueNotSet(SerializedProperty property) =>
            property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue) ||
            property.propertyType == SerializedPropertyType.ExposedReference && property.exposedReferenceValue == null ||
            property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null;

        private float GetHelpBoxHeight() => isNullValue ? helpBoxHeight + 2f : 0f;
    }
}