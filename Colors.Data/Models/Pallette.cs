using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Pallette
    {
        public int Id { get; set; } = 0;

        public int LevelId { get; set; } = 0;
        public Level Level { get; set; }

        public string Name { get; set; } = string.Empty;
        public List<byte> Colors { get; set; } = new List<byte>();

        public Pallette()
        {
            for (int i = 0; i < 16; i++)
            {
                Colors.Add((byte)(i * 16));
            }
        }
    }
}
