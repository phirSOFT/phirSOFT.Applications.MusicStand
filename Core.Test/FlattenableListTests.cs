using System;
using System.Collections.Generic;
using NUnit.Framework;
using phirSOFT.Applications.MusicStand.Core.Test.Mocks;

namespace phirSOFT.Applications.MusicStand.Core.Test
{
    [TestFixture()]
    public class FlattenableListTests
    {
        [Test()]
        public void FlattenableListTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetEnumeratorTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void AddTest(
            [Random(min: 10, max: 100, count: 3)]int count,
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
            }
        }

        [Test()]
        public void ClearTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ContainsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CopyToTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void IndexOfTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveAtTest()
        {
            Assert.Fail();
        }
    }
}