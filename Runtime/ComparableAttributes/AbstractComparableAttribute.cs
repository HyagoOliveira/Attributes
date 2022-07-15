using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Abstract class for Comparable Attributes.
    /// <para>Use it to create comparable property attributes.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public abstract class AbstractComparableAttribute : PropertyAttribute
    {
        public readonly object value;
        public readonly string property;
        public readonly LogicalOperatorType operatorType;

        /// <summary>
        /// Compares the given property using the other params.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        /// <param name="operatorType">The Comparison Operator to use.</param>
        /// <param name="value">The value to compare with the property.</param>
        public AbstractComparableAttribute(string property, LogicalOperatorType operatorType, object value)
        {
            this.value = value;
            this.property = property;
            this.operatorType = operatorType;
        }
    }
}