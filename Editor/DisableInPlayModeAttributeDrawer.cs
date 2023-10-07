using UnityEditor;
using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Custom Property Drawer for <see cref="DisableInPlayModeAttribute"/>.
    /// <para>It forces to disable the property when in play mode.</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public sealed class DisableInPlayModeAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Application.isPlaying)
            {
                GUI.enabled = false;
                property.TryDrawCustomPropertyField(position, label);
                GUI.enabled = true;
            }
            else property.TryDrawCustomPropertyField(position, label);
        }
    }
}