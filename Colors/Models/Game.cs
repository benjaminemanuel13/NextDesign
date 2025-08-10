using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = "1.0.0";
        public string Author { get; set; } = string.Empty;

        public virtual List<Level> Levels { get; set; } = new List<Level>();
    }
}
