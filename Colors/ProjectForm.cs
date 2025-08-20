using Colors.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colors
{
    public partial class ProjectForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile8x8Form Tile8Form { get; set; } = null!;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile16x16Form Tile16Form { get; set; } = null!;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteForm SpriteForm { get; set; } = null!;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TileMapForm TileMapForm { get; set; } = null!;

        private ContextMenuStrip contextMenu = new ContextMenuStrip();

        public ProjectForm()
        {
            InitializeComponent();

            LoadGames();

            project.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var node = project.GetNodeAt(e.Location);
                    if (node != null)
                    {
                        project.SelectedNode = node;

                        SetupMenu(node);

                        contextMenu.Show(project, e.Location);
                    }
                }
            };
        }

        private void SetupMenu(TreeNode node)
        {
            contextMenu.Items.Clear();
            if (node.Tag is List<Sprite>)
            {
                var item = contextMenu.Items.Add("Add Sprite", null, (s, e) => { AddSprite(node); });
            }
            else if (node.Tag is List<Tile8x8>)
            {
                contextMenu.Items.Add("Add 8x8 Tile", null, (s, e) => { AddTile(node); });
            }
            else if (node.Tag is List<Tile16x16>)
            {
                contextMenu.Items.Add("Add 16x16 Tile", null, (s, e) => { AddLargeTile(node); });
            }
            else if (node.Tag is Sprite)
            {
                contextMenu.Items.Add("Rename Sprite", null, (s, e) => { /* Rename logic */ });
                contextMenu.Items.Add("Delete Sprite", null, (s, e) => { /* Delete logic */ });
            }
            else if (node.Tag is Tile8x8)
            {
                contextMenu.Items.Add("Rename Tile", null, (s, e) => { /* Rename logic */ });
                contextMenu.Items.Add("Delete Tile", null, (s, e) => { /* Delete logic */ });
            }
            else if (node.Tag is Tile16x16)
            {
                contextMenu.Items.Add("Rename Tile", null, (s, e) => { /* Rename logic */ });
                contextMenu.Items.Add("Delete Tile", null, (s, e) => { /* Delete logic */ });
            }
            else
            {
                return;
            }
        }

        void AddSprite(TreeNode node)
        {
            var levelTag = node.Parent.Tag as Level;

            var level = Program.Project.Levels.Include(x => x.Sprites).Where(x => x.Id == levelTag.Id).FirstOrDefault();

            var newSprite = new Sprite
            {
                Name = "New Sprite",
                LevelId = level.Id,
                Pixels = new byte[256],
                Height = 16,
                Width = 16,
            };

            for(int i = 0; i < newSprite.Pixels.Length; i++)
            {
                newSprite.Pixels[i] = 0xE3;
            }

            Program.Project.Sprites.Add(newSprite);
            Program.Project.SaveChanges();

            node.Nodes.Add(newSprite.Id.ToString(), "New Sprite");

            LoadGames();
        }

        void AddTile(TreeNode node)
        {
            var levelTag = node.Parent.Tag as Level;

            var level = Program.Project.Levels.Include(x => x.Tiles).FirstOrDefault(x => x.Id == levelTag.Id);

            var newTile = new Tile8x8
            {
                Name = "New Tile",
                LevelId = level.Id,
                Pixels = new byte[64],
            };

            for (int i = 0; i < newTile.Pixels.Length; i++)
            {
                newTile.Pixels[i] = 0;
            }

            Program.Project.Tiles8.Add(newTile);
            Program.Project.SaveChanges();

            node.Nodes.Add(newTile.Id.ToString(), "New Tile");

            LoadGames();
        }

        void AddLargeTile(TreeNode node)
        {
            var levelTag = node.Parent.Tag as Level;

            var level = Program.Project.Levels.Include(x => x.Tiles16).FirstOrDefault(x => x.Id == levelTag.Id);

            var newTile = new Tile16x16
            {
                Name = "New Tile",
                LevelId = level.Id,
                Pixels = new byte[256],
            };

            for (int i = 0; i < newTile.Pixels.Length; i++)
            {
                newTile.Pixels[i] = 0;
            }

            Program.Project.Tiles16.Add(newTile);
            Program.Project.SaveChanges();

            node.Nodes.Add(newTile.Id.ToString(), "New Tile");

            LoadGames();
        }

        private void LoadGames()
        {
            project.Nodes.Clear();

            var games = Program.Project.Games.Include(x => x.Levels).ThenInclude(y => y.Sprites)
                .Include(x => x.Levels).ThenInclude(y => y.Tiles).Include(x => x.Levels)
                .ThenInclude(y => y.Tiles16).ToList();

            foreach (var game in games)
            {
                var node = project.Nodes.Add(game.Id.ToString(), game.Name);
                node.Tag = game;

                foreach (var level in game.Levels)
                {
                    var levelNode = node.Nodes.Add(level.Id.ToString(), level.Name);
                    levelNode.Tag = level;

                    var spritesNode = levelNode.Nodes.Add("Sprites", "Sprites");
                    spritesNode.Tag = level.Sprites;
                    foreach (var sprite in level.Sprites)
                    {
                        var newSprite = spritesNode.Nodes.Add(sprite.Id.ToString(), sprite.Name);
                        newSprite.Tag = sprite;
                    }

                    var tile8Node = levelNode.Nodes.Add("8x8Tiles", "8x8 Tiles");
                    tile8Node.Tag = level.Tiles;
                    foreach (var tile in level.Tiles)
                    {
                        var newTile = tile8Node.Nodes.Add(tile.Id.ToString(), tile.Name);
                        newTile.Tag = tile;
                    }

                    var tile16Node = levelNode.Nodes.Add("16x16Tiles", "16x16 Tiles");
                    tile16Node.Tag = level.Tiles16;
                    foreach (var tile in level.Tiles16)
                    {
                        var newTile = tile16Node.Nodes.Add(tile.Id.ToString(), tile.Name);
                        newTile.Tag = tile;
                    }

                    var tileMapNode = levelNode.Nodes.Add("TileMap", "Tile Map");
                }
            }
        }

        private void project_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            if (node.Tag is Sprite)
            {
                SpriteForm.SetSprite(node.Tag as Sprite);
            }
            else if (node.Tag is Tile8x8)
            {
                Tile8Form.SetTile(node.Tag as Tile8x8);
            }
            else if (node.Tag is Tile16x16)
            {
                Tile16Form.SetTile(node.Tag as Tile16x16);
            }

        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {

        }

        private void project_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
