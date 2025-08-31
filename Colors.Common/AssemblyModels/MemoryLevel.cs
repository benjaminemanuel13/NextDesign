using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.AssemblyModels
{
    public class MemoryLevel
    {
        public string LevelName { get; set; } = "Default Level";

        public int LevelNumber { get; set; } = 1;

        public int EnemyCount
        {
            get { 
                return Enemies.Count;
            }
        }

        public List<MemoryEnemy> Enemies { get; set; }


        public MemoryLevel() { 
            Enemies = new List<MemoryEnemy>()
            {
                new MemoryEnemy() { Sprite = 1, Path = 0 },
                new MemoryEnemy() { Sprite = 1, Path = 1 },
                new MemoryEnemy() { Sprite = 1, Path = 2 },
            };
        }
    }
}
