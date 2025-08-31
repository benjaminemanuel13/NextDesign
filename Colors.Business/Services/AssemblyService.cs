using Colors.AssemblyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Business.Services
{
    public class AssemblyService
    {
        public void GenerateMemoryMap()
        {
            MemoryMap map = new MemoryMap();

            FileStream stream = new FileStream("", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(map.Header.CurrentLevel);
            writer.Write(map.Header.CurrentLives);
            writer.Write(map.Header.CurrentPosition);

            writer.Write(map.Header.NumberLevels);
            foreach (var level in map.Header.Levels)
            {
                writer.Write(level.PlayerStartPosition);

                writer.Write(level.NumberEnemies);
                foreach (var enemy in level.Enemies)
                {
                    writer.Write(enemy.CurrentPosition);
                    writer.Write(enemy.Sprite);
                    writer.Write(enemy.Path);
                    writer.Write(enemy.CurrentStep);
                }

                writer.Write(level.NumberPaths);
                foreach (var path in level.Paths)
                {
                    writer.Write(path.NumberSteps);
                    foreach (var step in path.Steps)
                    {
                        writer.Write(step.Speed);
                        writer.Write(step.X);
                        writer.Write(step.Y);
                    }
                }
            }
        }
    }
}
