using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Disables the property when in play mode.
    /// <para>Use this to prevent users from editing a property when in play mode.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class DisableInPlayModeAttribute : PropertyAttribute
    { }
}