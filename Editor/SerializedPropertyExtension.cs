using UnityEditor;
using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    public static class SerializedPropertyExtension
    {
        /// <summary>
        /// Uses <see cref="PropertyDrawerFinder"/> and tries to draw a CustomPropertyField using the given params.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="position"></param>
        /// <param name="label"></param>
        public static void TryDrawCustomPropertyField(this SerializedProperty property, Rect position, GUIContent label)
        {
            var hasCustomDrawer = PropertyDrawerFinder.TryFindCustomDrawer(property, out PropertyDrawer drawer);
            if (hasCustomDrawer) drawer.OnGUI(position, property, label);
            else EditorGUI.PropertyField(position, property, label);
        }
    }
}