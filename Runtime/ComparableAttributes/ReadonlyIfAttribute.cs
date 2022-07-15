namespace ActionCode.Attributes
{
    /// <summary>
    /// Attribute for showing readonly fields when the given condition is met.
    /// <para>Use it to disallow changes in properties based on the current state of the object.</para>
    /// </summary>
    public sealed class ReadonlyIfAttribute : AbstractComparableAttribute
    {
        /// <summary>
        /// Disallows changes in this field only if the given property is equals to the value.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        /// <param name="value">The value to compare with the property.</param>
        public ReadonlyIfAttribute(string property, object value)
            : base(property, LogicalOperatorType.Equals, value) { }

        /// <summary>
        /// Disallows changes in this field only if the given property is not null.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        public ReadonlyIfAttribute(string property)
            : base(property, LogicalOperatorType.NotEqual, null) { }

        /// <summary>
        /// Disallows changes in this field only if the given condition is met.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        /// <param name="operatorType">The Comparison Operator to use.</param>
        /// <param name="value">The value to compare with the property.</param>
        public ReadonlyIfAttribute(string property, LogicalOperatorType operatorType, object value)
            : base(property, operatorType, value) { }
    }
}