using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Models
{
    public class TileBase
    {
        protected int startSlot = 0;
        public virtual int StartSlot { get; set; }


        protected int endSlot = 0;
        public virtual int EndSlot { get; set; }
    }
}
