using Colors.AssemblyModels;
using Colors.Common.AssemblyModels;
using Microsoft.EntityFrameworkCore;
using SKcode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colors.Business.Services
{
    public class AssemblyService
    {
        string BasePath = @"C:\Users\benja\source\Next\Colors\CoreGame\Game\";
        private ProjectDBContext _context;

        public AssemblyService()
        {
            _context = ProjectDBContext.Project;
        }

        public void GenerateMemoryMap()
        {
            MemoryMap map = new MemoryMap() { 
                Header = new MemoryMapHeader() { 
                    CurrentLevel = 1,
                    CurrentLives = 5,
                    CurrentPosition = 0x000A000A,  // 10 x 10
                }
            };

            var levels = _context.Levels.Include(x => x.Enemies).Include(x => x.Paths).ThenInclude(x => x.Steps).ToList();
            map.Header.NumberLevels = (byte)levels.Count;

            foreach(var level in levels)
            {
                MemoryLevel memLevel = new MemoryLevel()
                {
                    LevelNumber = (byte)level.Id,
                    PlayerStartPosition = 0x000A000A,
                    Paths = new List<MemoryPath>(),
                    Enemies = new List<MemoryEnemy>()
                };

                memLevel.NumberPaths = (byte)level.Paths.Count;
                foreach (var path in level.Paths)
                {
                    MemoryPath memPath = new MemoryPath()
                    {
                        NumberSteps = (byte)path.Steps.Count,
                        CurrentStep = 0,
                        Steps = new List<MemoryStep>()
                    };

                    memPath.NumberSteps = (byte)path.Steps.Count;
                    foreach (var step in path.Steps)
                    {
                        memPath.Steps.Add(new MemoryStep()
                        {
                            Speed = (ushort)step.Speed,
                            X = (ushort)(step.X),
                            Y = (ushort)(step.Y),
                        });
                    }
                    memLevel.Paths.Add(memPath);
                }

                memLevel.NumberEnemies = (byte)level.Enemies.Count;
                foreach (var enemy in level.Enemies)
                {
                    memLevel.Enemies.Add(new MemoryEnemy()
                    {
                        X = 19,
                        Y = 8,
                        Path = (byte)0,
                        Sprite = (byte)0,
                        CurrentStep = 0,
                    });
                }

                map.Header.Levels.Add(memLevel);
            }

            if (File.Exists(BasePath + "memory.map"))
            { 
                File.Delete(BasePath + "memory.map");
            }

            FileStream stream = new FileStream(BasePath + "memory.map", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            
            writer.Write(map.Header.CurrentLives);
            writer.Write(map.Header.CurrentLevel);
            writer.Write(map.Header.CurrentPosition);

            writer.Write(map.Header.NumberLevels);
            foreach (var level in map.Header.Levels)
            {
                writer.Write(level.PlayerStartPosition);

                writer.Write(level.NumberEnemies);
                foreach (var enemy in level.Enemies)
                {
                    writer.Write(enemy.X);
                    writer.Write(enemy.Y);
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

            writer.Close();
            stream.Close();
        }
    }
}
