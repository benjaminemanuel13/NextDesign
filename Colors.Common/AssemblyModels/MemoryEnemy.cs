using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Common.AssemblyModels
{
    public class MemoryEnemy
    {
        public byte Sprite { get; set; } = 0;

        public byte Path { get; set; } = 0;

        public ushort CurrentPosition { get; set; } = 0;

        public byte CurrentStep { get; set; } = 0;
    }
}
