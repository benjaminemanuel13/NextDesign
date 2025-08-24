using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class Step
    {
        public int Id { get; set; } = 0;

        public int PathId { get; set; } = 0;
        public Path Path { get; set; }

        public string Name { get; set; } = string.Empty;

        override public string ToString()
        {
            return Name;
        }
    }
}
