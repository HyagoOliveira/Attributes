using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Property Attribute for objects that can only select assets references.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AssetsOnlyAttribute : PropertyAttribute
    {
        /// <summary>
        /// The type of your property.
        /// </summary>
        public readonly Type type;

        /// <summary>
        /// Draws a object field with the given type that can only select assets references.
        /// </summary>
        /// <param name="type">The type of your property.</param>
        public AssetsOnlyAttribute(Type type)
        {
            this.type = type;
        }
    }
}