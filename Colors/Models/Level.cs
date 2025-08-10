using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Level
    {
        public int Id { get; set; } = 0;
        public int GameId { get; set; } = 0;

        public string Name { get; set; } = string.Empty;
        public int Width { get; set; } = 40;
        public int Height { get; set; } = 32;

        public Pallette Pallette { get; set; }
        public List<Sprite> Sprites { get; set; }
        public List<Tile> Tiles { get; set; }

        public Level()
        {
            
        }
    }
}
