using Colors.AssemblyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Business.Services
{
    public class AssemblyClass
    {
        public void GenerateMemoryMap()
        {
            MemoryMap map = new MemoryMap();

            FileStream stream = new FileStream("", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(map.Header.CurrentLevel);
        }
}
