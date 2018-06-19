using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal class SessionSetGroup : FlattenableList<ISessionSetItem>, ISessionSetItemGroup
    {
    }

    internal class SessionSet : FlattenableList<ISessionSetItem>, ISessionSet
    {
    }
}
