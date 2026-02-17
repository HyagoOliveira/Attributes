using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Use this attribute to clamp integer and floating point values to a maximum value in the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MaxAttribute : PropertyAttribute
    {
        /// <summary>
        /// The maximum value allowed for this field.
        /// </summary>
        public readonly float max;

        /// <summary>
        /// Creates a new attribute.
        /// </summary>
        /// <param name="max">The maximum value allowed for this field.</param>
        public MaxAttribute(float max)
        {
            this.max = max;
        }
    }
}