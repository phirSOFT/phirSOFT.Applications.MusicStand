using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    /// <summary>
    ///     Represents a collection that can be flatten. If any item added to the list implements <see cref="IList{T}" />
    ///     theese items are interpreted as child item and included to the flat view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial class FlattenableList<T> : IList<T>, IMonitorChildren, IFlattenable<T>
    {
        private readonly HashSet<IFlattenable<T>> _children = new HashSet<IFlattenable<T>>();
        private readonly IEqualityComparer<T> _comparer = EqualityComparer<T>.Default;
        private readonly Lazy<FlattenedListView> _flatView;

        private readonly LinkedList<(bool SingleNode, IReadOnlyList<T> Content)> _items =
            new LinkedList<(bool SingleNode, IReadOnlyList<T> Content)>();

        private int? _cachedChildrenCount = 0;

        private int _unmonitoredChildren;

        public FlattenableList()
        {
            _flatView = new Lazy<FlattenedListView>(() => new FlattenedListView(this));
        }

        private int UnmonitoredChildren
        {
            get => _unmonitoredChildren;
            set
            {
                bool notify = (_unmonitoredChildren == 0) ^ (value == 0);
                _unmonitoredChildren = value;
                if (notify)
                    OnCanMonitorAllChrildrenChanged();
            }
        }

        public IReadOnlyList<T> FlatView => _flatView.Value;

        int IFlattenable<T>.FlatCount
        {
            get
            {
                if (CanMonitorAllChildren)
                {
                    return Count + (int)(_cachedChildrenCount ?? (_cachedChildrenCount = _children.Sum(f => f.FlatCount)));
                }
                return Count + _children.Sum(f => f.FlatCount);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _items.First;
            while (currentNode != null)
            {
                if (currentNode.Value.SingleNode)
                    yield return (T)currentNode.Value.Content;
                else
                    foreach (T item in currentNode.Value.Content) yield return item;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item is IFlattenable<T> flattenable)
            {
                _items.AddLast((true, flattenable));
                AddChild(flattenable);
            }
            else if (_items.Last?.Value.SingleNode ?? true)
            {
                _items.AddLast((false, new List<T> { item }));
            }
            else
            {
                ((List<T>)_items.Last.Value.Content).Add(item);
            }

            Count++;

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
        }

        public void Clear()
        {
            _children.ToList().ForEach(RemoveChild);
            _items.Clear();
            Count = 0;
            UnmonitoredChildren = 0;
            _cachedChildrenCount = 0;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Array does not have enough space to contain this collection",
                    nameof(arrayIndex));

            foreach ((bool singleNode, IReadOnlyList<T> content) in _items)
                if (singleNode)
                {
                    array[arrayIndex++] = (T)content;
                }
                else
                {
                    ((List<T>)content).CopyTo(array, arrayIndex);
                    arrayIndex += content.Count;
                }
        }

        public bool Remove(T item)
        {
            LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node = _items.First;
            var currentIndex = 0;

            bool searchForSingleNode = item is IFlattenable<T>;

            while (node != null)
            {
                if (node.Value.SingleNode)
                {
                    if (searchForSingleNode && _comparer.Equals((T)node.Value.Content, item))
                    {
                        _items.Remove(node);
                        Count--;
                        RemoveChild((IFlattenable<T>)item);
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                            item, currentIndex));
                        return true;
                    }

                    ++currentIndex;
                }
                else
                {

                    var list = (List<T>)node.Value.Content;
                    if (!searchForSingleNode)
                    {
                        int index = list.IndexOf(item);
                        if (index >= 0)
                        {
                            list.RemoveAt(index);
                            Count--;
                            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Remove,
                                item, currentIndex + index));
                            return true;
                        }
                    }

                    currentIndex += list.Count;
                }


                node = node.Next;
            }

            return false;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            var index = 0;
            foreach ((bool singlenode, IReadOnlyList<T> content) in _items)
            {
                if (singlenode)
                {
                    if (_comparer.Equals((T)content, item))
                        return index;
                    ++index;
                }
                else
                {
                    int innerIndex = ((List<T>)content).IndexOf(item);
                    if (innerIndex >= 0)
                        return index + innerIndex;
                    index += content.Count;
                }


            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index has to be between 0 and {Count}");
            if (index == Count)
            {
                Add(item);
                return;
            }

            if (item is IFlattenable<T> flattenable)
                InsertInternal(index, flattenable);
            else
                InsertInternal(index, item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            Count++;
        }

        public void RemoveAt(int index)
        {
            (int nodeIndex, LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node) =
                FindContainingNode(index);
            if (node.Value.SingleNode || node.Value.Content.Count == 1)
            {
                if (node.Value.SingleNode)
                    RemoveChild((IFlattenable<T>)node.Value.Content);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                    node.Value.SingleNode ? (T)node.Value.Content : node.Value.Content[index: 0], index));
                _items.Remove(node);
            }
            else
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                    node.Value.Content[index - nodeIndex], index));
                ((List<T>)node.Value.Content).RemoveAt(index - nodeIndex);
            }

            Count--;
        }

        public T this[int index]
        {
            get
            {
                (int nodeIndex, LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node) =
                    FindContainingNode(index);
                return node.Value.SingleNode ? (T)node.Value.Content : node.Value.Content[index - nodeIndex];
            }
            set
            {
                (int nodeIndex, LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node) =
                    FindContainingNode(index);
                T oldValue;
                if (node.Value.SingleNode)
                {
                    oldValue = (T)node.Value.Content;
                    RemoveChild((IFlattenable<T>)oldValue);
                    if (value is IFlattenable<T> flattenable)
                    {
                        AddChild(flattenable);
                        node.Value = (true, flattenable);
                    }
                    else if (!node.Previous?.Value.SingleNode ?? false)
                    {
                        ((List<T>)node.Previous.Value.Content).Add(value);
                        _items.Remove(node);
                    }
                    else if (!node.Next?.Value.SingleNode ?? false)
                    {
                        ((List<T>)node.Next.Value.Content).Insert(index: 0, value);
                        _items.Remove(node);
                    }
                    else
                    {
                        node.Value = (false, new List<T> { value });
                    }
                }
                // required to scope flattenable
                else
                {
                    var currentNodeItems = (List<T>)node.Value.Content;
                    oldValue = currentNodeItems[index - nodeIndex];
                    if (value is IFlattenable<T> flattenable)
                    {
                        AddChild(flattenable);
                        if (nodeIndex < index)
                        {
                            int splitIndex = index - nodeIndex;
                            int moveCount = currentNodeItems.Count - splitIndex;

                            node = _items.AddAfter(node,
                                (false, currentNodeItems.GetRange(splitIndex + 1, moveCount - 1)));
                            currentNodeItems.RemoveRange(splitIndex, moveCount);
                        }
                        else
                        {
                            currentNodeItems.RemoveAt(index: 0);
                        }

                        _items.AddBefore(node, (true, flattenable));
                    }
                    else
                    {
                        currentNodeItems[index - nodeIndex] = value;
                    }
                }

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,
                    value, oldValue, index));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool CanMonitorAllChildren => UnmonitoredChildren == 0;

        public event EventHandler CanMonitorAllChildrenChanged;

        private void AddChild(IFlattenable<T> item)
        {
            _children.Add(item);
            if (item is IMonitorChildren monitor)
            {
                monitor.CanMonitorAllChildrenChanged += MonitorableChildChanged;
                if (!CanMonitorAllChildren) return;
                monitor.CollectionChanged += MonitoredChildChanged;
                _cachedChildrenCount = _cachedChildrenCount + item.FlatCount;
            }
            else
            {
                UnmonitoredChildren++;
            }
        }

        private void MonitoredChildChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Move)
                _cachedChildrenCount = null;
        }

        private void MonitorableChildChanged(object sender, EventArgs e)
        {
            var monitor = (IMonitorChildren)sender;
            if (monitor.CanMonitorAllChildren)
            {
                monitor.CollectionChanged += MonitoredChildChanged;
                _cachedChildrenCount = _cachedChildrenCount + ((IFlattenable<T>)sender).FlatCount;
            }
            else
            {
                monitor.CollectionChanged -= MonitoredChildChanged;
                _cachedChildrenCount = null;
            }
        }

        private void RemoveChild(IFlattenable<T> child)
        {
            _children.Remove(child);
            if (child is IMonitorChildren monitor)
            {
                monitor.CanMonitorAllChildrenChanged -= MonitorableChildChanged;
                if (!CanMonitorAllChildren) return;
                monitor.CollectionChanged -= MonitoredChildChanged;
                _cachedChildrenCount = _cachedChildrenCount - child.FlatCount;
            }
            else
            {
                UnmonitoredChildren--;
            }
        }

        private void InsertInternal(int index, T item)
        {
            (int nodeIndex, LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node) =
                FindContainingNode(index);

            if (nodeIndex == index && (!node.Previous?.Value.SingleNode ?? false))
                ((List<T>)node.Previous.Value.Content).Add(item);
            else if (nodeIndex < index)
                ((List<T>)node.Value.Content).Insert(index - nodeIndex, item);
            else
                _items.AddBefore(node, (false, new List<T> { item }));
        }

        private void InsertInternal(int index, IFlattenable<T> item)
        {
            AddChild(item);
            (int nodeIndex, LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> node) =
                FindContainingNode(index);

            if (nodeIndex < index)
            {
                var currentNodeItems = (List<T>)node.Value.Content;
                int splitIndex = index - nodeIndex;
                int moveCount = currentNodeItems.Count - splitIndex;

                node = _items.AddAfter(node, (false, currentNodeItems.GetRange(splitIndex, moveCount)));
                currentNodeItems.RemoveRange(splitIndex, moveCount);
            }

            _items.AddBefore(node, (true, item));
        }


        private (int NodeIndex, LinkedListNode<(bool, IReadOnlyList<T>)> Node) FindContainingNode(int index)
        {
            LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> forwardNode = _items.First;
            LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> backwadNode = _items.Last;

            var forwardIndex = 0;
            int backwardIndex = Count;

            while (forwardIndex < backwardIndex)
            {
                Debug.Assert(forwardNode != null, nameof(forwardNode) + " != null");
                int forwardStepSize = forwardNode.Value.SingleNode ? 1 : forwardNode.Value.Content.Count;
                forwardIndex += forwardStepSize;
                if (forwardIndex > index)
                    return (forwardIndex - forwardStepSize, forwardNode);

                Debug.Assert(backwadNode != null, nameof(backwadNode) + " != null");
                backwardIndex -= backwadNode.Value.SingleNode ? 1 : backwadNode.Value.Content.Count;
                if (backwardIndex <= index)
                    return (backwardIndex, backwadNode);

                // forward and backward are still not null, since the loop
                // will terminate before they reach the and of the linked list.
                forwardNode = forwardNode.Next;
                backwadNode = backwadNode.Previous;
            }

            return default;
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnCanMonitorAllChrildrenChanged()
        {
            CanMonitorAllChildrenChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}