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
//#if Desktop
        string BasePath = @"C:\Users\benja\source\Next\Colors\CoreGame\Game\";
//#else
//        string BasePath = @"C:\Users\benja\source\repos\benjaminemanuel13\NextDesign\CoreGame\Game\";
//#endif
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
                            X = (ushort)(step.X * 16),
                            Y = (ushort)(step.Y * 16),
                        });
                    }
                    memLevel.Paths.Add(memPath);
                }

                int sprite = 0;
                memLevel.NumberEnemies = (byte)level.Enemies.Count;
                foreach (var enemy in level.Enemies)
                {
                    memLevel.Enemies.Add(new MemoryEnemy()
                    {
                        X = (ushort)(enemy.Path.Steps[0].X * 16),
                        Y = (ushort)(enemy.Path.Steps[0].Y * 16),
                        Path = (byte)enemy.Path.Id,
                        Sprite = (byte)enemy.Sprite.Pos,
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

                    writer.Write((byte)2);  // This should be ignored.
                    var p = _context.Paths.Include(x => x.Steps).First(x => x.Id == enemy.Path);
                    writer.Write((byte)(p.Steps.Count * 4));

                    var a = 0;

                    foreach (var step in p.Steps)
                    {
                        bool isX = false;
                        bool isPositive = false;

                        if (a < p.Steps.Count - 1)
                        {
                            if (p.Steps[a + 1].X > p.Steps[a].X) // Moving right
                            {
                                isX = true;
                                isPositive = true;
                            }
                            else if (p.Steps[a + 1].Y > p.Steps[a].Y) // Moving down
                            {
                                isX = false;
                                isPositive = true;
                            }
                            else if (p.Steps[a].X > p.Steps[a + 1].X)  // Moving left
                            {
                                isX = true;
                                isPositive = false;
                            }
                            else if (p.Steps[a].Y > p.Steps[a + 1].Y) // Moving up
                            {
                                isX = false;
                                isPositive = false;
                            }
                        }
                        else
                        {
                            if (p.Steps[0].X > p.Steps[a].X) // Moving right
                            {
                                isX = true;
                                isPositive = true;
                            }
                            else if (p.Steps[0].Y > p.Steps[a].Y) // Moving down
                            {
                                isX = false;
                                isPositive = true;
                            }
                            else if (p.Steps[a].X > p.Steps[0].X)  // Moving left
                            {
                                isX = true;
                                isPositive = false;
                            }
                            else if (p.Steps[a].Y > p.Steps[0].Y) // Moving up
                            {
                                isX = false;
                                isPositive = false;
                            }
                        }
                        a++;

                        if (!isPositive)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                writer.Write((ushort)step.Speed);

                                if (isX)
                                {
                                    writer.Write((ushort)((step.X * 16) - (i * 4)));
                                    writer.Write((ushort)(step.Y * 16));
                                }
                                else
                                {
                                    writer.Write((ushort)(step.X * 16));
                                    writer.Write((ushort)((step.Y * 16) - (i * 4)));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                writer.Write((ushort)step.Speed);

                                if (isX)
                                {
                                    writer.Write((ushort)((step.X * 16) + (i * 4)));
                                    writer.Write((ushort)(step.Y * 16));
                                }
                                else
                                {
                                    writer.Write((ushort)(step.X * 16));
                                    writer.Write((ushort)((step.Y * 16) + (i * 4)));
                                }
                            }
                        }
                    }
                }
            }

            writer.Close();
            stream.Close();
        }
    }
}
