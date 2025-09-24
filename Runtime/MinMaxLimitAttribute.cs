using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Property Attribute for a min/max representation of a slider range.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinMaxLimitAttribute : PropertyAttribute
    {
        /// <summary>
        /// The minimum value of the slider limit.
        /// </summary>
        public readonly float min;

        /// <summary>
        /// The maximum value of the slider limit.
        /// </summary>
        public readonly float max;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">The minimum value of the slider limit.</param>
        /// <param name="max">The maximum value of the slider limit.</param>
        public MinMaxLimitAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}