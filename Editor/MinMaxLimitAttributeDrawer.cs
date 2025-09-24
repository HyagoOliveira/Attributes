using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxLimitAttribute))]
    public class MinMaxLimitAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var limit = (MinMaxLimitAttribute)attribute;
            var value = GetMinMaxValues(property);
            var container = new VisualElement();
            var slider = new MinMaxSlider(
                property.displayName,
                value.x,
                value.y,
                limit.min,
                limit.max
            );

            //container.style.flexDirection = FlexDirection.Row;
            //container.Add(new Label(limit.min.ToString()));
            container.Add(slider);
            //container.Add(new Label(limit.max.ToString()));

            return container;
        }

        private static Vector2 GetMinMaxValues(SerializedProperty property) => property.propertyType switch
        {
            SerializedPropertyType.Vector2 => property.vector2Value,
            SerializedPropertyType.Vector2Int => property.vector2IntValue,
            _ => throw new NotSupportedException($"Property type {property.propertyType} is not supported.")
        };
    }
}