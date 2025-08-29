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
        public ushort CurrentLevel { get; set; } = 1;

        public byte NumberLives { get; set; } = 5;

        public ushort NumberLevels { get; set; } = 2;

        public List<MemoryLevel> Levels { get; set; }


        public MemoryMapHeader()
        {
            Levels = new List<MemoryLevel>()
            {
                new MemoryLevel() { LevelNumber = 1, Spi },
                new MemoryLevel() { LevelNumber = 2, TimeLimit = 250, NumberOfColors = 4 }
            };
        }
    }
}
