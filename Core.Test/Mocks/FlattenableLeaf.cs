using System;
using System.Collections.Generic;

namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    struct FlattenableLeaf<T> : INode<T>, IEquatable<FlattenableLeaf<T>>
    {
        public FlattenableLeaf(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public bool Equals(FlattenableLeaf<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is FlattenableLeaf<T> leaf && Equals(leaf);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(FlattenableLeaf<T> left, FlattenableLeaf<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FlattenableLeaf<T> left, FlattenableLeaf<T> right)
        {
            return !left.Equals(right);
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}