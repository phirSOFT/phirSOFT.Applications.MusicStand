using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.Applications.MusicStand.Contract;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal class SessionSetNavigator : ISessionSetNavigator
    {
        public ISessionSet SessionSet { get; set; }
        public ISessionSetItem Current { get; }
        public int Index { get; set; }
        public bool MoveFirst()
        {
            throw new NotImplementedException();
        }

        public bool MoveUp()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public bool MovePrevious()
        {
            throw new NotImplementedException();
        }

        public bool MoveLast()
        {
            throw new NotImplementedException();
        }
    }
}
