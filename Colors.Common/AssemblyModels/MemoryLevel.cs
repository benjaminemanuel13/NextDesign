using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.AssemblyModels
{
    public class MemoryLevel
    {
        public string LevelName { get; set; }

        public int LevelNumber { get; set; } = 1;

        public uint PlayerStartPosition { get; set; } = 0;

        public byte NumberEnemies { get; set; }
        
        public List<MemoryEnemy> Enemies { get; set; }

        public int NumberPaths { get; set; }
        
        public List<MemoryPath> Paths { get; set; }
    }
}
