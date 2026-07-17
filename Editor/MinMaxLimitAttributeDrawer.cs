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
            var isValid = property.propertyType is
                SerializedPropertyType.Vector2 or
                SerializedPropertyType.Vector2Int;
            if (!isValid) return new HelpBox(
                "MinMaxLimitAttribute is only compatible with Vector2 or Vector2Int types.",
                HelpBoxMessageType.Error
            );

            var root = asset.Instantiate();
            var label = root.Q<Label>("Label");
            var lowLimit = root.Q<Label>("LowLimit");
            var highLimit = root.Q<Label>("HighLimit");
            var lowValue = root.Q<FloatField>("LowValue");
            var highValue = root.Q<FloatField>("HighValue");
            var slider = root.Q<MinMaxSlider>("MinMaxSlider");
            var minMaxLimit = attribute as MinMaxLimitAttribute;
            var adapter = PropertyAdapterFactory(property);
            var min = minMaxLimit.GetMin();
            var max = minMaxLimit.GetMax();

            label.text = property.displayName;
            label.tooltip = property.tooltip;
            lowLimit.text = min.ToString();
            highLimit.text = max.ToString();

            slider.lowLimit = min;
            slider.highLimit = max;

            var initialValue = adapter.GetValue();
            slider.SetValueWithoutNotify(initialValue);
            lowValue.SetValueWithoutNotify(initialValue.x);
            highValue.SetValueWithoutNotify(initialValue.y);

            slider.RegisterValueChangedCallback(evt =>
            {
                var value = adapter.Round(evt.newValue);

                adapter.SetValue(value);
                lowValue.SetValueWithoutNotify(value.x);
                highValue.SetValueWithoutNotify(value.y);

                property.serializedObject.ApplyModifiedProperties();
            });

            lowValue.RegisterValueChangedCallback(evt =>
            {
                var current = adapter.GetValue();
                var rounded = adapter.Round(evt.newValue);
                var clamped = Mathf.Clamp(rounded, min, current.y);

                adapter.SetValue(new Vector2(clamped, current.y));
                property.serializedObject.ApplyModifiedProperties();
            });

            highValue.RegisterValueChangedCallback(evt =>
            {
                var current = adapter.GetValue();
                var rounded = adapter.Round(evt.newValue);
                var clamped = Mathf.Clamp(rounded, current.x, max);

                adapter.SetValue(new Vector2(current.x, clamped));
                property.serializedObject.ApplyModifiedProperties();
            });

            // Tracking external changes to update the UI
            root.TrackPropertyValue(property, prop =>
            {
                var current = adapter.GetValue();
                slider.SetValueWithoutNotify(current);
                lowValue.SetValueWithoutNotify(current.x);
                highValue.SetValueWithoutNotify(current.y);
            });

            return root;
        }

        private static AbstractPropertyAdapter PropertyAdapterFactory(SerializedProperty property) => property.propertyType switch
        {
            SerializedPropertyType.Vector2 => new Vector2Adapter(property),
            SerializedPropertyType.Vector2Int => new Vector2IntAdapter(property),
            _ => null
        };

        private abstract class AbstractPropertyAdapter
        {
            protected readonly SerializedProperty property;

            public AbstractPropertyAdapter(SerializedProperty property) => this.property = property;

            public abstract Vector2 GetValue();
            public abstract void SetValue(Vector2 value);
            public abstract float Round(float value);
            public Vector2 Round(Vector2 value) => new(
                Round(value.x),
                Round(value.y)
            );
        }

        private sealed class Vector2Adapter : AbstractPropertyAdapter
        {
            public Vector2Adapter(SerializedProperty property) : base(property) { }
            public override float Round(float value) => MathF.Round(value, 1);
            public override Vector2 GetValue() => property.vector2Value;
            public override void SetValue(Vector2 value) => property.vector2Value = value;
        }

        private sealed class Vector2IntAdapter : AbstractPropertyAdapter
        {
            public Vector2IntAdapter(SerializedProperty property) : base(property) { }
            public override float Round(float value) => MathF.Round(value);
            public override Vector2 GetValue() => property.vector2IntValue;
            public override void SetValue(Vector2 value) => property.vector2IntValue = Vector2Int.CeilToInt(value);
        }
    }
}