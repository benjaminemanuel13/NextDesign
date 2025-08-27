using Colors.Assistant.Plugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.EventArguments
{
    public class SelectionMoveEventArgs
    {
        public SelectionMove Move { get; set; }

        public SelectionMoveEventArgs(SelectionMove move)
        {
            Move = move;
        }
    }
}
