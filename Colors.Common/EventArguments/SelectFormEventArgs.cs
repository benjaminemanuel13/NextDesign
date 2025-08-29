using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.EventArguments
{
    public class SelectFormEventArgs : EventArgs
    {
        public string FormName { get; set; }

        public SelectFormEventArgs(string formName)
        {
            FormName = formName;
        }
    }
}
