using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int PathId { get; set; } = 0;
        public Path Path { get; set; }

        public int Speed { get; set; } = 4000;

        public string Name { get; set; } = string.Empty;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        override public string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }
}
