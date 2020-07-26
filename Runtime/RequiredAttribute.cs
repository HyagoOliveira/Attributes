using System;
using UnityEngine;

/// <summary>
/// Property Attribute for required fields.
/// <para>You can use it on strings, exposed references and object references types.</para>
/// <para>An error message will be displayed bellow the attribute if its value is not set, null or empty.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class RequiredAttribute : PropertyAttribute
{
    /// <summary>
    /// The message displayed when the field is not set.
    /// </summary>
    public readonly string message;

    /// <summary>
    /// Draws an error message bellow the attribute if it value is not set.
    /// </summary>
    /// <param name="message">The message displayed when the field is not set.</param>
    public RequiredAttribute(string message = null)
    {
        this.message = message;
    }
}