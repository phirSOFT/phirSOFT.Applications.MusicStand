using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal class SessionSet : ISessionSet, INotifyCollectionChanged
    {
        public IEnumerator<ISessionSetItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ISessionSetItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ISessionSetItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ISessionSetItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ISessionSetItem item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(ISessionSetItem item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ISessionSetItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ISessionSetItem this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IReadOnlyList<ISessionSetItem> GetFlatView()
        {
            throw new NotImplementedException();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public IReadOnlyList<ISessionSetItem> FlatView { get; }
        public int FlatCount { get; }
    }
}
