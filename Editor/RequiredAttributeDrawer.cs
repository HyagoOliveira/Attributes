using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public sealed class RequiredAttributeDrawer : PropertyDrawer
{
    private bool isValueNull = false;
    private const float HELP_BOX_HEIGHT = 32F;
    private const string DEFAULT_ERROR_MESSAGE_FORMAT_TEXT = "Field '{0}' requires a value!";

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + (isValueNull ? HELP_BOX_HEIGHT + 2f : 0f);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = base.GetPropertyHeight(property, label);
        EditorGUI.PropertyField(position, property, label);

        if (isValueNull = IsPropertyValueNull(property))
        {
            RequiredAttribute requiredAttribute = attribute as RequiredAttribute;
            string message = string.IsNullOrEmpty(requiredAttribute.message) ?
                string.Format(DEFAULT_ERROR_MESSAGE_FORMAT_TEXT, property.displayName) :
                requiredAttribute.message;

            position.y += position.height + 2f;
            position.height = HELP_BOX_HEIGHT;
            EditorGUI.HelpBox(position, message, MessageType.Error);
        }
    }

    private bool IsPropertyValueNull(SerializedProperty property)
    {
        return 
            property.propertyType == SerializedPropertyType.String && string.IsNullOrEmpty(property.stringValue) ||
            property.propertyType == SerializedPropertyType.ExposedReference && property.exposedReferenceValue == null ||
            property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null;
    }
}