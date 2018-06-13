using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    ///     Provides a set of elements (like songs) played during a session.
    /// </summary>
    /// <remarks>
    ///     The implementing interface only represents the direct children.
    ///     To retrieve all items use the <see cref="GetFlatView"/> Method.
    /// </remarks>
    public interface ISessionSet : IList<ISessionSetItem>
    {
        /// <summary>
        /// Returns a flatten view of the session set.
        /// </summary>
        /// <returns>A readonly collection that maps to the flatten view.</returns>
        IReadOnlyList<ISessionSetItem> GetFlatView();
    }
}
