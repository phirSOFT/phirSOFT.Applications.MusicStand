using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    class SessionSetItemGroupMock : ISessionSetItemGroup, INotifyCollectionChanged
    {
        private readonly ObservableCollection<ISessionSetItem> _items = new ObservableCollection<ISessionSetItem>();
        
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public IEnumerator<ISessionSetItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _items).GetEnumerator();
        }

        public void Add(ISessionSetItem item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(ISessionSetItem item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(ISessionSetItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(ISessionSetItem item)
        {
            return _items.Remove(item);
        }

        public int Count => _items.Count;

        public bool IsReadOnly => ((ICollection<ISessionSetItem>) _items).IsReadOnly;

        public int IndexOf(ISessionSetItem item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, ISessionSetItem item)
        {
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public ISessionSetItem this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public IReadOnlyList<ISessionSetItem> FlatView { get; }
        public int FlatCount { get; }
    }
}