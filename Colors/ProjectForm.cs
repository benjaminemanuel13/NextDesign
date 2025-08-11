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

        public ProjectForm()
        {
            InitializeComponent();

            LoadGames();
        }

        private void LoadGames()
        {
            var games = Program.Project.Games.Include(x => x.Levels).ThenInclude(y => y.Sprites)
                .Include(x => x.Levels).ThenInclude(y => y.Tiles).Include(x => x.Levels)
                .ThenInclude(y => y.Tiles16).ToList();

            foreach (var game in games)
            {
                var node = project.Nodes.Add(game.Id.ToString(), game.Name);

                foreach (var level in game.Levels)
                {
                    var levelNode = node.Nodes.Add(level.Id.ToString(), level.Name);

                    var spritesNode = levelNode.Nodes.Add("Sprites", "Sprites");
                    foreach (var sprite in level.Sprites)
                    {
                        var newSprite = spritesNode.Nodes.Add(sprite.Id.ToString(), sprite.Name);
                        newSprite.Tag = sprite;
                    }

                    var tile8Node = levelNode.Nodes.Add("8x8Tiles", "8x8 Tiles");
                    foreach (var tile in level.Tiles)
                    {
                        var newTile = tile8Node.Nodes.Add(tile.Id.ToString(), tile.Name);
                        newTile.Tag = tile;
                    }

                    var tile16Node = levelNode.Nodes.Add("16x16Tiles", "16x16 Tiles");
                    foreach (var tile in level.Tiles16)
                    {
                        var newTile = tile16Node.Nodes.Add(tile.Id.ToString(), tile.Name);
                        newTile.Tag = tile;
                    }
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
    }
}
