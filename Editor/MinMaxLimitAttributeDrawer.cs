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
                lowValue.value = (int)evt.newValue.x;
                highValue.value = (int)evt.newValue.y;

                property.serializedObject.ApplyModifiedProperties();
            });

            return root;
        }
    }
}