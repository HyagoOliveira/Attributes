using UnityEngine;

namespace ActionCode.Attributes.Editor
{
    public class ReferenceComparableAttribute : IComparableAttribute
    {
        public readonly object other;
        public readonly EntityId current;

        public ReferenceComparableAttribute(EntityId current, object other)
        {
            this.other = other;
            this.current = current;
        }

        public bool HasMetCondition()
        {
            if (other is null) return current != null;
            if (other is EntityId id) return current == id;
            return false;
        }
    }
}