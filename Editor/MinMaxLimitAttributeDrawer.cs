using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxLimitAttribute))]
    public class MinMaxLimitAttributeDrawer : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset asset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = asset.Instantiate();
            var label = root.Q<Label>("Label");
            var lowLimit = root.Q<Label>("LowLimit");
            var highLimit = root.Q<Label>("HighLimit");
            var lowValue = root.Q<FloatField>("LowValue");
            var highValue = root.Q<FloatField>("HighValue");
            var slider = root.Q<MinMaxSlider>("MinMaxSlider");
            var minMaxLimit = attribute as MinMaxLimitAttribute;

            label.text = property.displayName;
            label.tooltip = property.tooltip;
            lowLimit.text = minMaxLimit.min.ToString();
            highLimit.text = minMaxLimit.max.ToString();

            slider.lowLimit = minMaxLimit.min;
            slider.lowLimit = minMaxLimit.max;
            slider.BindProperty(property);

            slider.RegisterValueChangedCallback(evt =>
            {
                lowValue.value = (int)evt.newValue.x;
                highValue.value = (int)evt.newValue.y;

                property.serializedObject.ApplyModifiedProperties();
            });

            /*var limit = (MinMaxLimitAttribute)attribute;
            var value = GetMinMaxValues(property);
            var container = new VisualElement();
            var slider = new MinMaxSlider(
                property.displayName,
                value.x,
                value.y,
                limit.min,
                limit.max
            );*/

            //container.style.flexDirection = FlexDirection.Row;
            //container.Add(new Label(limit.min.ToString()));
            //container.Add(slider);
            //container.Add(new Label(limit.max.ToString()));

            return root;
        }

        private static Vector2 GetMinMaxValues(SerializedProperty property) => property.propertyType switch
        {
            SerializedPropertyType.Vector2 => property.vector2Value,
            SerializedPropertyType.Vector2Int => property.vector2IntValue,
            _ => throw new NotSupportedException($"Property type {property.propertyType} is not supported.")
        };
    }
}