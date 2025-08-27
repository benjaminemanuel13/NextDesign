using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.EventArguments
{
    public class ProjectFormToFrontEventArgs : EventArgs
    {
        public string FormName { get; set; }

        public ProjectFormToFrontEventArgs(string formName)
        {
            FormName = formName;
        }
    }
}
