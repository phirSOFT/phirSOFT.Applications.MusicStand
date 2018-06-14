using System;
using System.Collections.Specialized;

namespace phirSOFT.Applications.MusicStand.Core
{
    /// <inheritdoc />
    /// <summary>
    /// A collection implementing this interface notifies the subsriber, wheater it can monitor all of its children.
    /// </summary>
    /// <remarks>
    /// If this interface is not present on a collection, it is assumed that <see cref="P:phirSOFT.Applications.MusicStand.Core.IMonitorChildren.CanMonitorAllChildren" /> is allways <see langword="false" />
    /// </remarks>
    public interface IMonitorChildren : INotifyCollectionChanged
    {
        /// <summary>
        /// If true, the collection can monitor all of its children.
        /// </summary>
        bool CanMonitorAllChildren { get; }

        /// <summary>
        /// This event is raised, when <see cref="CanMonitorAllChildren"/> changed.
        /// </summary>
        event EventHandler CanMonitorAllChildrenChanged;
    }
}