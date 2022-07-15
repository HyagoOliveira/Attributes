namespace ActionCode.Attributes.Editor
{
    /// <summary>
    /// Interface used on objects able to be a Comparable Attribute.
    /// </summary>
    public interface IComparableAttribute
    {
        /// <summary>
        /// Checks if the current condition is met.
        /// </summary>
        /// <returns></returns>
        bool HasMetCondition();
    }
}