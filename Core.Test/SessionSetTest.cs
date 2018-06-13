using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using phirSOFT.Applications.MusicStand.Contract;
using phirSOFT.Applications.MusicStand.Core.Test.Mocks;

namespace phirSOFT.Applications.MusicStand.Core.Test
{
    [TestFixture]
    public class SessionSetTest
    {
        [Test]
        public void TestFlatten()
        {
            var sessionSet = new SessionSet();

            List<SessionSetItemMock> items = Enumerable.Range(start: 0, count: 3).Select(n => new SessionSetItemMock())
                .ToList();
            var container = new SessionSetItemGroupMock();

            items.ForEach(i => container.Add(i));

            sessionSet.Add(container);

            Assert.AreEqual(expected: 5, actual: sessionSet.GetFlatView().Count);
            IEnumerable<ISessionSetItem> expexted = new ISessionSetItem[] {container}.Concat(items);
            CollectionAssert.AreEqual(expexted, sessionSet.GetFlatView());
        }

        [Test]
        public void TestFlattenReflectChanges_01()
        {
            var sessionSet = new SessionSet();
            IReadOnlyList<ISessionSetItem> flatten = sessionSet.GetFlatView();

            Assert.AreEqual(expected: 0, actual: flatten.Count);

            var item = new SessionSetItemMock();
            sessionSet.Add(item);

            Assert.AreEqual(expected: 1, actual: flatten.Count);
        }

        [Test]
        public void TestFlattenReflectChanges_02()
        {
            var sessionSet = new SessionSet();
            IReadOnlyList<ISessionSetItem> flatten = sessionSet.GetFlatView();

            Assert.AreEqual(expected: 0, actual: flatten.Count);

            var container = new SessionSetItemGroupMock();
            sessionSet.Add(container);

            Assert.AreEqual(expected: 1, actual: flatten.Count);
            container.Add(new SessionSetItemMock());

            Assert.AreEqual(expected: 2, actual: flatten.Count);
        }

        [Test]
        public void TestRootCount()
        {
            var sessionSet = new SessionSet();

            var container = new SessionSetItemGroupMock
            {
                new SessionSetItemMock(),
                new SessionSetItemMock(),
                new SessionSetItemMock()
            };

            sessionSet.Add(container);

            Assert.AreEqual(expected: 1, actual: sessionSet.Count);
        }
    }
}