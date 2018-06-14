using System;
using System.Collections;
using System.Collections.Generic;

namespace phirSOFT.Applications.MusicStand.Core
{
    internal partial class FlattenableList<T>
    {
        private class FlattenedListView : IReadOnlyList<T>
        {
            public FlattenedListView(FlattenableList<T> parent)
            {
                
            }
        }
    }
}