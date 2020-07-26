using System;
using UnityEngine;

/// <summary>
/// Property Attribute for tags fields. Use it with a string field.
/// <para>It will replace the string field by a Tag Popup.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class TagAttribute : PropertyAttribute
{
}