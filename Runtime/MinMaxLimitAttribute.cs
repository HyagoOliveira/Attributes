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
        private readonly float min;
        private readonly float max;

        /// <summary>
        /// Attribute for a min/max representation of a slider range.
        /// <b>Only compatible with Vector2</b>.
        /// </summary>
        /// <remarks>The minimum value of the slider limit is set to 0.</remarks>
        /// <param name="max">The maximum value of the slider limit.</param>
        public MinMaxLimitAttribute(float max) : this(0F, max)
        { }

        /// <summary>
        /// Attribute for a min/max representation of a slider range. 
        /// <b>Only compatible with Vector2</b>.
        /// </summary>
        /// <param name="min">The minimum value of the slider limit.</param>
        /// <param name="max">The maximum value of the slider limit.</param>
        public MinMaxLimitAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// The minimum value of the slider limit.
        /// </summary>
        /// <returns></returns>
        public float GetMin() => MathF.Min(min, max);

        /// <summary>
        /// The maximum value of the slider limit.
        /// </summary>
        /// <returns></returns>
        public float GetMax() => MathF.Max(min, max);
    }
}