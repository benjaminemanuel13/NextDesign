using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Assistant.Plugin.Models
{
    public enum SelectionMoveDirection
    {
        Undefined,
        Left,
        Right,
        Up,
        Down
    }
    public class SelectionMove
    {
        public SelectionMoveDirection Direction { get; set; }
        public int Amount { get; set; }
    }
}
