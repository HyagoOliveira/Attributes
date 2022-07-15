using System;

namespace ActionCode.Attributes.Editor
{
    public class ComparableAttribute<T> : IComparableAttribute where T : IComparable
    {
        public readonly T other;
        public readonly T current;
        public readonly LogicalOperatorType operatorType;

        public ComparableAttribute(T current, T other, LogicalOperatorType operatorType)
        {
            this.other = other;
            this.current = current;
            this.operatorType = operatorType;
        }

        public bool HasMetCondition()
        {
            var comparason = current.CompareTo(other);
            return operatorType switch
            {
                LogicalOperatorType.Equals => comparason == 0,
                LogicalOperatorType.NotEqual => comparason != 0,
                LogicalOperatorType.GreaterThan => comparason > 0,
                LogicalOperatorType.SmallerThan => comparason < 0,
                LogicalOperatorType.SmallerOrEqual => comparason <= 0,
                LogicalOperatorType.GreaterOrEqual => comparason >= 0,
                _ => false
            };
        }
    }
}