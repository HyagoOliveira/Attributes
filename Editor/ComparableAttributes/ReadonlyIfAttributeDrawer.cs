using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ReadonlyIfAttribute))]
    public class ReadonlyIfAttributeDrawer : AbstractComparableAttributeDrawer<ReadonlyIfAttribute>
    {
        protected override void DrawProperty(bool isCoditionMet, Rect position, SerializedProperty property, GUIContent label)
        {
            if (isCoditionMet)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
            else EditorGUI.PropertyField(position, property, label);
        }
    }
}