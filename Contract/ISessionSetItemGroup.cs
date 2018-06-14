using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    /// Provies a logical container to organize <see cref="T:phirSOFT.Applications.MusicStand.Contract.ISessionSetItem" />s in a hirachial way.
    /// </summary>
    public interface ISessionSetItemGroup : ISessionSetItem, IList<ISessionSetItem>, IFlattenable<ISessionSetItem>
    {

    }
}
