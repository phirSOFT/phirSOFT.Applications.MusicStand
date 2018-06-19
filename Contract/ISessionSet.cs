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
    public interface ISessionSet : IList<ISessionSetItem>, IFlattenable<ISessionSetItem>
    {

    }
}
