using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    struct FlattenableMock<T> : IFlattenable<INode<T>>, INode<T>, IEquatable<FlattenableMock<T>>
    {
        private readonly INode<T>[] _data;

        public FlattenableMock(INode<T>[] data, T value)
        {
            this._data = data;
            Value = value;
            var list = new List<INode<T>>();
            FlatView = list;

   
            if(data!=null)
                list.AddRange(data);

        }

        public FlattenableMock(T[] data, T value)
            :this(data.Select(i => (INode<T>) new FlattenableLeaf<T>(i)).ToArray(), value)
        {
            

        }

        public IEnumerator<INode<T>> GetEnumerator()
        {
            return ((IList<INode<T>>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int Count => _data.Length;

        public INode<T> this[int index] => _data[index];

        public IReadOnlyList<INode<T>> FlatView { get; }
        public int FlatCount => FlatView.Count;
        public T Value { get; }


        public bool Equals(FlattenableMock<T> other)
        {
            return Equals(_data, other._data) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FlattenableMock<T> && Equals((FlattenableMock<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_data != null ? _data.GetHashCode() : 0) * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }

        public static bool operator ==(FlattenableMock<T> left, FlattenableMock<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FlattenableMock<T> left, FlattenableMock<T> right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            var inner = _data != null ? string.Join<INode<T>>(", ", _data) : "null";
            return $"{Value.ToString()} -> {{{inner}}}";
        }
    }
}
