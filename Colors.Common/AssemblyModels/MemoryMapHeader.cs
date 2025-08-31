using Colors.Common.AssemblyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.AssemblyModels
{
    public class MemoryMapHeader
    {
        public uint PlayerStartPosition { get; set; }

        public byte CurrentLevel { get; set; } = 1;

        public byte CurrentLives { get; set; } = 5;
        public uint CurrentPosition { get; set; }

        public byte NumberLevels { get; set; } = 2;

        public List<MemoryLevel> Levels { get; set; } = new List<MemoryLevel>();
    }
}
