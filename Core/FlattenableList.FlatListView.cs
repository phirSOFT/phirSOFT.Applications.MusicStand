using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal partial class FlattenableList<T>
    {
        private class FlattenedListView : IReadOnlyList<T>
        {
            private readonly FlattenableList<T> _parent;

            public FlattenedListView(FlattenableList<T> parent)
            {
                _parent = parent;
            }

            public IEnumerator<T> GetEnumerator()
            {
                foreach (T item in _parent)
                {
                    yield return item;
                    if (!(item is IFlattenable<T> flattenable)) continue;

                    foreach (T subItem in flattenable.FlatView)
                    {
                        yield return subItem;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => ((IFlattenable<T>)_parent).FlatCount;

            public T this[int index]
            {
                get
                {
                    LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> forwardNode = _parent._items.First;
                    LinkedListNode<(bool SingleNode, IReadOnlyList<T> Content)> backwadNode = _parent._items.Last;

                    var forwardIndex = 0;
                    int backwardIndex = Count;

                    while (forwardIndex < backwardIndex)
                    {
                        Debug.Assert(forwardNode != null, nameof(forwardNode) + " != null");
                        IReadOnlyList<T> list = GetList(forwardNode);
                        int forwardStepSize = list.Count;

                        if (forwardIndex + forwardStepSize > index)
                        {
                            return list[index - forwardIndex];
                        }
                        forwardIndex += forwardStepSize;

                        Debug.Assert(backwadNode != null, nameof(backwadNode) + " != null");
                        list = GetList(backwadNode);
                        backwardIndex -= list.Count;
                        if (backwardIndex <= index)
                            return list[index - backwardIndex];

                        // forward and backward are still not null, since the loop
                        // will terminate before they reach the and of the linked list.
                        forwardNode = forwardNode.Next;
                        backwadNode = backwadNode.Previous;
                    }

                    return default;
                }
            }

            private static IReadOnlyList<T> GetList(LinkedListNode<(bool, IReadOnlyList<T>)> node)
            {
                return node.Value.Item1 ? ((IFlattenable<T>) node.Value.Item2).FlatView : node.Value.Item2;
            }
        }
    }
}