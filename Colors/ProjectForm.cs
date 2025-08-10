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
        public SpriteForm SpriteForm { get; set; } = null!;

        public ProjectForm()
        {
            InitializeComponent();

            LoadGames();
        }

        private void LoadGames()
        {
            var games = Program.Project.Games.Include(x => x.Levels).ThenInclude(y => y.Sprites)
                .Include(x => x.Levels).ThenInclude(y => y.Tiles).ToList();

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
        }
    }
}
