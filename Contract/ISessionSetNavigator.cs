using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    /// Navigates through a sesssion set
    /// </summary>
    public interface ISessionSetNavigator
    {
        /// <summary>
        ///     The session set to navigate through.
        /// </summary>
        ISessionSet SessionSet { get; set; }

        /// <summary>
        /// Gets the currently active sessionset item.
        /// </summary>
        ISessionSetItem Current { get; }

        /// <summary>
        /// Gets or sets the index of the current item.
        /// </summary>
        /// <remarks>
        ///     The index is the index of the FlatView retrieved by <see cref="ISessionSet.GetFlatView()"/>.
        /// </remarks>
        int Index { get; set; }

        /// <summary>
        /// Moves to the first item in the session set.
        /// </summary>
        /// <returns>Returns true, when the <see cref="Current"/> changed.</returns>
        bool MoveFirst();

        /// <summary>
        /// Moves to the parent item of the current item. If the current item has no parent. This call is equivalent to <see cref="MoveFirst"/> 
        /// </summary>
        /// <returns>Returns true, when the <see cref="Current"/> changed.</returns>
        bool MoveUp();

        /// <summary>
        /// Moves to the next item in the session set, if there is any.
        /// </summary>
        /// <returns>Returns true, when the <see cref="Current"/> changed.</returns>
        bool MoveNext();

        /// <summary>
        /// Moves to the previous item in the session set, if there is any.
        /// </summary>
        /// <returns>Returns true, when the <see cref="Current"/> changed.</returns>
        bool MovePrevious();

        /// <summary>
        /// Moves to the last item in the session set.
        /// </summary>
        /// <returns>Returns true, when the <see cref="Current"/> changed.</returns>
        bool MoveLast();
    }
}
