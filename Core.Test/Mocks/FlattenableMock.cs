using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    struct FlattenableMock<T> : IFlattenable<T>, INode<T>
    {
        private T[] data;

        public FlattenableMock(T[] data, T value)
        {
            this.data = data;
            Value = value;
            var list = new List<T>() { value };
            list.AddRange(data);
            FlatView = list;

        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public int Count => data.Length;

        public T this[int index] => data[index];

        public IReadOnlyList<T> FlatView { get; }
        public int FlatCount => FlatView.Count;
        public T Value { get; }
    }
}
