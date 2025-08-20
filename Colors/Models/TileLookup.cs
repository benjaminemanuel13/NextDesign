using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public enum TileType
    {
        Tile8x8,
        Tile16x16
    }

    public class TileLookup
    {
        public int Id { get; set; }
        
        public TileType Type { get; set; }

        public int TileId { get; set; }
    }
}
