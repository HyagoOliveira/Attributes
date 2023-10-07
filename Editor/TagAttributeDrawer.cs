using UnityEditor;
using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom Property Drawer for <see cref="TagAttribute"/>.
    /// <para>It replaces the string field by a Tag Popup.</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public sealed class TagAttributeDrawer : PropertyDrawer
    {
        private readonly GUIContent[] tagsContent;

        public TagAttributeDrawer()
        {
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            tagsContent = new GUIContent[tags.Length];
            for (int i = 0; i < tagsContent.Length; i++)
            {
                tagsContent[i] = new GUIContent(tags[i]);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var isString = property.propertyType == SerializedPropertyType.String;

            label = EditorGUI.BeginProperty(position, label, property);

            if (isString) DisplayTagsPopup(position, property, label);
            else
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                DisplayErrorMessage(position);
            }

            EditorGUI.EndProperty();
        }

        private void DisplayTagsPopup(Rect position, SerializedProperty property, GUIContent label)
        {
            var index = 0;
            var value = property.stringValue;

            for (int i = 0; i < tagsContent.Length; i++)
            {
                if (string.Equals(tagsContent[i].text, value))
                    index = i;
            }

            index = EditorGUI.Popup(position, label, index, tagsContent);
            property.stringValue = tagsContent[index].text;
        }

        private void DisplayErrorMessage(Rect position)
        {
            const string message = "This field should be a string!";
            EditorGUI.HelpBox(position, message, MessageType.Error);
        }
    }
}