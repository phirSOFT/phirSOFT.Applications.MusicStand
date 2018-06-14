using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using phirSOFT.Applications.MusicStand.Contract;
using phirSOFT.Applications.MusicStand.Core.Test.Mocks;

namespace phirSOFT.Applications.MusicStand.Core.Test
{
    [TestFixture]
    public class FlattenableListTests
    {
        [Test]
        public void AddTest_01_PureItems(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed
        )
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                list.Add(new FlattenableLeaf<int>(number));
                Assert.AreEqual(number, list[i].Value);
                Assert.AreEqual(i + 1, list.Count);
                Assert.AreEqual(i + 1, ((IFlattenable<INode<int>>) list).FlatCount);
            }
        }

        [Test]
        public void AddTest_02_PureEmptyContainers(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed
        )
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                list.Add(new FlattenableMock<int>((INode<int>[]) null, number));
                Assert.AreEqual(number, list[i].Value);
                Assert.AreEqual(i + 1, list.Count);
                Assert.AreEqual(i + 1, ((IFlattenable<INode<int>>) list).FlatCount);
            }
        }


        [Test]
        public void AddTest_03_Intermixed_EmptyContainer(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed
        )
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node = rnd.NextDouble() > 0.5
                    ? (INode<int>) new FlattenableLeaf<int>(number)
                    : new FlattenableMock<int>((INode<int>[]) null, number);


                list.Add(node);
                Assert.AreEqual(number, list[i].Value);
                Assert.AreEqual(i + 1, list.Count);
                Assert.AreEqual(i + 1, ((IFlattenable<INode<int>>) list).FlatCount);
            }
        }


        [Test]
        public void AddTest_04_FilledContainers(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed
        )
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var total = 0;
            for (var i = 0; i < count; i++)
            {
                total++;
                int number = rnd.Next();
                var data = new int[number % 1024];
                for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                total += number % 1024;
                list.Add(new FlattenableMock<int>(data, number));
                Assert.AreEqual(number, list[i].Value);
                Assert.AreEqual(i + 1, list.Count);
                Assert.AreEqual(total, ((IFlattenable<INode<int>>) list).FlatCount);
            }
        }

        [Test]
        public void AddTest_05_Intermixed_PreFilledContainers(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var total = 0;
            for (var i = 0; i < count; i++)
            {
                total++;
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    total += number % 1024;
                    node = new FlattenableMock<int>(data, number);
                }


                list.Add(node);
                Assert.AreEqual(number, list[i].Value);
                Assert.AreEqual(i + 1, list.Count);
                Assert.AreEqual(total, ((IFlattenable<INode<int>>) list).FlatCount);
            }
        }

        [Test]
        public void ClearTest(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }


                list.Add(node);
            }

            list.Clear();
            CollectionAssert.IsEmpty(list);
        }

        [Test]
        public void ContainsTest(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var generated = new HashSet<int>();

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                if (!generated.Contains(number))
                    Assert.False(list.Contains(node));
                generated.Add(number);

                list.Add(node);
                Assert.True(list.Contains(node));
            }

            list.Clear();
            CollectionAssert.IsEmpty(list);
        }

        [Test]
        public void CopyToTest_01_Sucess(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed,
            [Range(from: 0, to: 3)] int offset,
            [Range(from: 0, to: 3)] int overhead)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var added = new List<INode<int>>(count);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                added.Add(node);
                list.Add(node);
            }

            var array = new INode<int>[count + offset + overhead];
            list.CopyTo(array, offset);
            CollectionAssert.AreEqual(added, array.Skip(offset).Take(count));
        }

        [Test]
        public void CopyToTest_02_FailOnNullArray()
        {
            var list = new FlattenableList<object>();

            var ex = Assert.Throws<ArgumentNullException>(() => list.CopyTo(array: null, arrayIndex: 1));
            Assert.AreEqual("array", ex.ParamName);
        }

        [Test]
        public void CopyToTest_03_FailOnNegativeIndex([Random(int.MinValue, max: -1, count: 10)]
            int index)
        {
            var list = new FlattenableList<object>();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(Array.Empty<object>(), index));
            Assert.AreEqual("arrayIndex", ex.ParamName);
        }

        [Test]
        public void CopyToTest_04_FailOnSmallArray(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed,
            [Range(from: 1, to: 3)] int offset)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }


                list.Add(node);
            }

            var array = new INode<int>[count];
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, offset));
        }

        [Test]
        public void FlattenableListTest()
        {
            var list = new FlattenableList<object>();
            Assert.Zero(list.Count);
            Assert.Zero(((IFlattenable<object>) list).FlatCount);
        }

        [Test]
        public void GetEnumeratorTest(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var added = new List<INode<int>>(count);

            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                added.Add(node);
                list.Add(node);
            }

            CollectionAssert.AreEqual(added, list);
        }

        [Test]
        public void IndexerGetterTest(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var controlGroup = new List<INode<int>>();
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                controlGroup.Add(node);
                list.Add(node);
            }

            while (count-- > 0) Assert.AreEqual(controlGroup[count], list[count], $"Difference at index {count}");
        }

        [Test]
        public void IndexerSetterTest(
            [Random(min: 10, max: 100, count: 3)] int count,
            [Random(min: 10, max: 100, count: 3)] int toReplace,
            [Random(count: 10)] int seed)
        {
            var list = new FlattenableList<INode<int>>();
            var rnd = new Random(seed);
            var controlGroup = new List<INode<int>>();
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                controlGroup.Add(node);
                list.Add(node);
            }

            for (var i = 0; i < toReplace; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                int index = rnd.Next(count);
                list[index] = node;
                controlGroup[index] = node;
                CollectionAssert.AreEqual(controlGroup, list);
            }
        }

        [Test]
        public void IndexOfTest(
            [Random(min: 10, max: 1000, count: 30)]
            int count)
        {
            var list = new FlattenableList<INode<int>>();
            Randomizer rnd = TestContext.CurrentContext.Random;
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }


                list.Add(node);
            }

            for (var i = 0; i < count; ++i) Assert.AreEqual(i, list.IndexOf(list[i]));
        }

        [Test]
        public void InsertTest(
            [Random(min: 10, max: 1000, count: 30)]
            int count)
        {
            var list = new FlattenableList<INode<int>>();
            Randomizer rnd = TestContext.CurrentContext.Random;
            var controlGroup = new List<INode<int>>();
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                int insertIndex = rnd.Next(i);
                controlGroup.Insert(insertIndex, node);
                list.Insert(insertIndex, node);
            }

            CollectionAssert.AreEqual(controlGroup, list);
        }


        [Test]
        public void RemoveAtTest(
            [Random(min: 10, max: 1000, count: 30)]
            int count)
        {
            var list = new FlattenableList<INode<int>>();
            Randomizer rnd = TestContext.CurrentContext.Random;
            var controlGroup = new List<INode<int>>();
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                controlGroup.Add(node);
                list.Add(node);
            }

            var iteration = 0;
            while ((count = controlGroup.Count) > 0)
            {
                int index = rnd.Next(count);
                list.RemoveAt(index);
                controlGroup.RemoveAt(index);
                CollectionAssert.AreEqual(controlGroup, list, $"After removing {index} ({++iteration}. iteration)");
                Assert.AreEqual(controlGroup.Count, list.Count);
            }
        }


        [Test]
        public void RemoveTest(
            [Random(min: 10, max: 1000, count: 30)]
            int count)
        {
            var list = new FlattenableList<INode<int>>();
            Randomizer rnd = TestContext.CurrentContext.Random;
            var controlGroup = new List<INode<int>>();
            for (var i = 0; i < count; i++)
            {
                int number = rnd.Next();
                INode<int> node;
                if (rnd.NextDouble() > 0.5)
                {
                    node = new FlattenableLeaf<int>(number);
                }
                else
                {
                    var data = new int[number % 1024];
                    for (var j = 0; j < number % 1024; j++) data[j] = rnd.Next();

                    node = new FlattenableMock<int>(data, number);
                }

                controlGroup.Add(node);
                list.Add(node);
            }

            var iteration = 0;
            var index = 0;
            while ((count = controlGroup.Count) > 0)
            {
                index = (index + 2) % count;
                INode<int> toBeRemoved = list[index];
                Assert.True(list.Remove(toBeRemoved));
                controlGroup.Remove(toBeRemoved);
                CollectionAssert.AreEqual(controlGroup, list, $"After removing {index} ({++iteration}. iteration)");
                Assert.AreEqual(controlGroup.Count, list.Count);
            }
        }
    }
}