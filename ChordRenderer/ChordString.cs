using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordRenderer
{
    /// <summary>
    /// Represents a string with chord symbols above
    /// </summary>
    public sealed class ChordString
    {
        private readonly string _baseString;
        private readonly Dictionary<int, object> _chords;


        public IReadOnlyList<ChordLine> Lines { get; set; }
    }

    public struct ChordLine
    {
        public string Lyrics { get; }
        public IReadOnlyDictionary<int, object> Chords { get; }
    }
}
