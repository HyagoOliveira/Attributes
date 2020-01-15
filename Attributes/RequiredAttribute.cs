using System;
using UnityEngine;

/// <summary>
/// You can use this attribute on strings, exposed reference and object reference types. 
/// If the value is null or empty, a error message will be displayed on Inspector bellow the given attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class RequiredAttribute : PropertyAttribute
{
    public readonly string message;

    public RequiredAttribute(string message = null)
    {
        this.message = message;
    }
}
