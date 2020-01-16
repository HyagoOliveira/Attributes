using System;
using UnityEngine;

/// <summary>
/// Tag Property Attribute. Use this attribute with a string field.
/// <para>It'll replace the string field by a tag popup.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class TagAttribute : PropertyAttribute
{
}