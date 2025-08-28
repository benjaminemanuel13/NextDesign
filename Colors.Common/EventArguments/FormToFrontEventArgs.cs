using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.EventArguments
{
    public class FormToFrontEventArgs : EventArgs
    {
        public string FormName { get; set; }

        public FormToFrontEventArgs(string formName)
        {
            FormName = formName;
        }
    }
}
