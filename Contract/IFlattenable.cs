using System.Collections.Generic;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    ///     Gets a flattened view of the list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IFlattenable<out T> : IReadOnlyList<T>
    {
        /// <summary>
        /// Gets a view of the collection, when its flattened.
        /// </summary>
        /// <remarks>
        /// The view is not a snapshot. The view will update when the collection changes.
        /// </remarks>
        IReadOnlyList<T> FlatView { get; }

        /// <summary>
        /// Gets the numer of elements in the collection, when its flattened.
        /// </summary>
        int FlatCount { get; }
    }
}
