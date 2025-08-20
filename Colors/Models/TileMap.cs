using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class TileMap
    {
        public int Id { get; set; } = 0;

        public int LevelId { get; set; } = 0;
        public Level Level { get; set; }
        public string Name { get; set; } = string.Empty;
        public int[] LookupIds { get; set; } = new int[1280];
    }
}
