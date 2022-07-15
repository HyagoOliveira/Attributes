using UnityEditor;

namespace ActionCode.Attributes.Editor
{
    public static class ComparableAttributeFactory
    {
        public static IComparableAttribute Create(AbstractComparableAttribute attribute, SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.Generic => throw new System.NotImplementedException(),
                SerializedPropertyType.Integer => new ComparableAttribute<int>(property.intValue, (int)attribute.value, attribute.operatorType),
                SerializedPropertyType.Boolean => new ComparableAttribute<bool>(property.boolValue, (bool)attribute.value, attribute.operatorType),
                SerializedPropertyType.Float => new ComparableAttribute<float>(property.floatValue, (float)attribute.value, attribute.operatorType),
                SerializedPropertyType.String => new ComparableAttribute<string>(property.stringValue, (string)attribute.value, attribute.operatorType),
                SerializedPropertyType.Color => throw new System.NotImplementedException(),
                SerializedPropertyType.ObjectReference => new ReferenceComparableAttribute(property.objectReferenceInstanceIDValue, attribute.value),
                SerializedPropertyType.LayerMask => new ComparableAttribute<int>(property.intValue, (int)attribute.value, attribute.operatorType),
                SerializedPropertyType.Enum => new ComparableAttribute<int>(property.enumValueIndex, (int)attribute.value, attribute.operatorType),
                SerializedPropertyType.Vector2 => throw new System.NotImplementedException(),
                SerializedPropertyType.Vector3 => throw new System.NotImplementedException(),
                SerializedPropertyType.Vector4 => throw new System.NotImplementedException(),
                SerializedPropertyType.Rect => throw new System.NotImplementedException(),
                SerializedPropertyType.ArraySize => throw new System.NotImplementedException(),
                SerializedPropertyType.Character => throw new System.NotImplementedException(),
                SerializedPropertyType.AnimationCurve => throw new System.NotImplementedException(),
                SerializedPropertyType.Bounds => throw new System.NotImplementedException(),
                SerializedPropertyType.Gradient => throw new System.NotImplementedException(),
                SerializedPropertyType.Quaternion => throw new System.NotImplementedException(),
                SerializedPropertyType.ExposedReference => new ReferenceComparableAttribute(property.exposedReferenceValue.GetInstanceID(), attribute.value),
                SerializedPropertyType.FixedBufferSize => throw new System.NotImplementedException(),
                SerializedPropertyType.Vector2Int => throw new System.NotImplementedException(),
                SerializedPropertyType.Vector3Int => throw new System.NotImplementedException(),
                SerializedPropertyType.RectInt => throw new System.NotImplementedException(),
                SerializedPropertyType.BoundsInt => throw new System.NotImplementedException(),
                SerializedPropertyType.ManagedReference => throw new System.NotImplementedException(),
                SerializedPropertyType.Hash128 => throw new System.NotImplementedException(),
                _ => null
            };
        }
    }
}