using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(MaxAttribute))]
    public sealed class MaxAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var maxAttr = attribute as MaxAttribute;
            var propertyField = new PropertyField(property);

            // Using TrackPropertyValue to track changes without managing event listeners
            propertyField.TrackPropertyValue(property, (p) =>
            {
                if (p.propertyType == SerializedPropertyType.Float)
                {
                    if (p.floatValue > maxAttr.max)
                    {
                        p.floatValue = maxAttr.max;
                        p.serializedObject.ApplyModifiedProperties();
                    }
                }
                else if (p.propertyType == SerializedPropertyType.Integer)
                {
                    var max = (int)maxAttr.max;
                    if (p.intValue > max)
                    {
                        p.intValue = max;
                        p.serializedObject.ApplyModifiedProperties();
                    }
                }
            });

            return propertyField;
        }
    }
}