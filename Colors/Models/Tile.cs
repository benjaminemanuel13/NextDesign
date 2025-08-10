using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Tile
    {
        public int Id { get; set; }

        public int LevelId { get; set; } = 0;
        public Level Level { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public byte[] Pixels { get; set; } = new byte[64];

        public Tile()
        {
            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = 0xE3;
            }
        }
    }
}
