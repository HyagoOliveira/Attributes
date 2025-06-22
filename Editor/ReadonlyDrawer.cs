using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public sealed class ReadonlyDrawer : PropertyDrawer
    {
        // Old way. It does not work well for Unity >= 6 since it does not use UI Toolkit.
        /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }*/

        // New away using UI Toolkit.
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new PropertyField(property);
            field.SetEnabled(false);
            return field;
        }
    }
}