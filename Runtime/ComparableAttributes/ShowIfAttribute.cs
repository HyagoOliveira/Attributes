namespace ActionCode.Attributes
{
    /// <summary>
    /// Attribute for showing fields when the given condition is met.
    /// <para>Use it to show properties based on the current state of the object.</para>
    /// </summary>
    public sealed class ShowIfAttribute : AbstractComparableAttribute
    {
        /// <summary>
        /// Shows this field only if the given property is equals to the value.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        /// <param name="value">The value to compare with the property.</param>
        public ShowIfAttribute(string property, object value)
            : base(property, LogicalOperatorType.Equals, value) { }

        /// <summary>
        /// Shows this field only if the given property is not null.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        public ShowIfAttribute(string property)
            : base(property, LogicalOperatorType.NotEqual, null) { }

        /// <summary>
        /// Shows this field only if the given condition is met.
        /// </summary>
        /// <param name="property">The name of the property to compare (case sensitive).</param>
        /// <param name="operatorType">The Comparison Operator to use.</param>
        /// <param name="value">The value to compare with the property.</param>
        public ShowIfAttribute(string property, LogicalOperatorType operatorType, object value)
            : base(property, operatorType, value) { }
    }
}