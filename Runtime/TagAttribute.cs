using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Property Attribute for tags fields. Use it with string fields.
    /// <para>It will replace the string field by a Tag Popup.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class TagAttribute : PropertyAttribute
    { }
}