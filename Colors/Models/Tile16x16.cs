using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Tile16x16
    {
        public int Id { get; set; }

        public int LevelId { get; set; } = 0;
        public Level Level { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public byte[] Pixels { get; set; } = new byte[16 * 16];

        public Tile16x16()
        {
            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = 0xE3;
            }
        }
    }
}
