using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Sprite
    {
        public int Id { get; set; }

        public int LevelId { get; set; } = 0;
        public Level Level { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Width { get; set; } = 16;
        public int Height { get; set; } = 16;
        public byte[] Pixels { get; set; } = new byte[256]; // 16x16 pixels, each pixel is a byte

        public Sprite()
        {
            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = 0xE3;
            }
        }
    }
}
