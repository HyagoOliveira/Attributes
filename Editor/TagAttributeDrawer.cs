using UnityEditor;
using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public sealed class TagAttributeDrawer : PropertyDrawer
    {
        private GUIContent[] tagsContent;

        private const string NOT_STRING_ERROR_FORMAT_MESSAGE = "Field '{0}' should be a string!";

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
            if (property.propertyType == SerializedPropertyType.String)
            {
                int index = 0;
                string value = property.stringValue;
                for (int i = 0; i < tagsContent.Length; i++)
                {
                    if (string.Equals(tagsContent[i].text, value))
                    {
                        index = i;
                    }
                }

                label = EditorGUI.BeginProperty(position, label, property);
                index = EditorGUI.Popup(position, label, index, tagsContent);
                EditorGUI.EndProperty();

                property.stringValue = tagsContent[index].text;
            }
            else
            {
                EditorGUI.HelpBox(position,
                    string.Format(NOT_STRING_ERROR_FORMAT_MESSAGE, property.displayName),
                    MessageType.Error);
            }
        }
    }
}