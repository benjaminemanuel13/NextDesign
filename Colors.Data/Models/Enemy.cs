using Colors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Data.Models
{
    public class Enemy
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        
        public int PathId { get; set; }
        public Colors.Models.Path Path { get; set; }

        public int SpriteId { get; set; }
        public Sprite16x16 Sprite { get; set; }

        override public string ToString()
        {
            return Name;
        }
    }
}
