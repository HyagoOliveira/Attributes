using UnityEditor;
using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom Property Drawer for <see cref="AssetsOnlyAttribute"/>.
    /// <para>It forces to display only assets on object field.</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(AssetsOnlyAttribute))]
    public class AssetsOnlyAttributeDrawer : PropertyDrawer
    {
        private int lineCount = 1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            return lineCount * height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            lineCount = 1;
            var isReferenceType = property.propertyType == SerializedPropertyType.ObjectReference;

            label = EditorGUI.BeginProperty(position, label, property);

            if (isReferenceType)
            {
                DisplayAssetsOnlyObjectField(position, property, label);
            }
            else
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                DisplayErrorTypeMessage(position);
            }

            EditorGUI.EndProperty();
        }

        private void DisplayAssetsOnlyObjectField(Rect position, SerializedProperty property, GUIContent label)
        {
            var asset = attribute as AssetsOnlyAttribute;
            var reference = property.objectReferenceValue;
            reference = EditorGUI.ObjectField(position, label, reference, asset.type, false);

            if (reference)
            {
                var isAsset = AssetDatabase.IsMainAsset(reference);
                if (!isAsset) reference = null;
            }

            property.objectReferenceValue = reference;
        }

        private void DisplayErrorTypeMessage(Rect position)
        {
            lineCount = 2;
            EditorGUI.HelpBox(position, "AssetsOnly attribute only works with Object References.", MessageType.Error);
        }
    }
}