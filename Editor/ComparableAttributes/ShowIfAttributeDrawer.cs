using UnityEngine;
using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : AbstractComparableAttributeDrawer<ShowIfAttribute>
    {
        private float propertyHeight;

        public override float GetPropertyHeight(SerializedProperty _, GUIContent __) => propertyHeight;

        protected override void DrawProperty(bool isCoditionMet, Rect position, SerializedProperty property, GUIContent label)
        {
            if (isCoditionMet)
            {
                EditorGUI.PropertyField(position, property, label);
                propertyHeight = base.GetPropertyHeight(property, label);
            }
            else propertyHeight = 0;
        }
    }
}