using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.AssemblyModels
{
    public class MemoryPath
    {
        public byte CurrentStep { get; set; } = 0;

        public byte NumberSteps
        {
            get
            {
                return (byte)Steps.Count;
            }
        }

        public List<MemoryStep> Steps { get; set; } = new List<MemoryStep>();
    }
}
