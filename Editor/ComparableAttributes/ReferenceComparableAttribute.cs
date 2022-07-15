namespace ActionCode.Attributes.Editor
{
    public class ReferenceComparableAttribute : IComparableAttribute
    {
        public readonly object other;
        public readonly int current;

        public ReferenceComparableAttribute(int current, object other)
        {
            this.other = other;
            this.current = current;
        }

        public bool HasMetCondition()
        {
            if (other is null) return current != 0;
            if (other is int intValue) return current == intValue;
            return false;
        }
    }
}