using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxLimitAttribute))]
    public class MinMaxLimitAttributeDrawer : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset asset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
                return new HelpBox("MinMaxLimitAttribute is only compatible with Vector2.", HelpBoxMessageType.Error);

            var root = asset.Instantiate();
            var label = root.Q<Label>("Label");
            var lowLimit = root.Q<Label>("LowLimit");
            var highLimit = root.Q<Label>("HighLimit");
            var lowValue = root.Q<FloatField>("LowValue");
            var highValue = root.Q<FloatField>("HighValue");
            var slider = root.Q<MinMaxSlider>("MinMaxSlider");
            var minMaxLimit = attribute as MinMaxLimitAttribute;
            var min = minMaxLimit.GetMin();
            var max = minMaxLimit.GetMax();

            label.text = property.displayName;
            label.tooltip = property.tooltip;
            lowLimit.text = min.ToString();
            highLimit.text = max.ToString();

            slider.lowLimit = min;
            slider.highLimit = max;
            slider.BindProperty(property);

            slider.RegisterValueChangedCallback(evt =>
            {
                var value = new Vector2(
                    Round(evt.newValue.x),
                    Round(evt.newValue.y)
                );
                property.vector2Value = value;
                lowValue.SetValueWithoutNotify(value.x);
                highValue.SetValueWithoutNotify(value.y);

                property.serializedObject.ApplyModifiedProperties();
            });

            lowValue.RegisterValueChangedCallback(evt =>
            {
                var newValue = Mathf.Clamp(evt.newValue, min, max);
                property.vector2Value = new Vector2(newValue, property.vector2Value.y);
                property.serializedObject.ApplyModifiedProperties();
            });

            highValue.RegisterValueChangedCallback(evt =>
            {
                var newValue = Mathf.Clamp(evt.newValue, min, max);
                property.vector2Value = new Vector2(property.vector2Value.x, newValue);
                property.serializedObject.ApplyModifiedProperties();
            });

            return root;
        }

        private static float Round(float value) => (float)System.Math.Round(value, 1);
    }
}