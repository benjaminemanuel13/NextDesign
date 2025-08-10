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
    public partial class MainForm : Form
    {
        PalletteForm palletteForm = new PalletteForm();
        SpriteForm spriteForm = new SpriteForm();
        TileForm tileForm = new TileForm();
        TileMapForm tileMapForm = new TileMapForm();
        ProjectForm projectForm = new ProjectForm();

        public MainForm()
        {
            InitializeComponent();

            palletteForm.MdiParent = this;
            palletteForm.Show();

            spriteForm.MdiParent = this;
            spriteForm.PalletteForm = palletteForm;
            spriteForm.Show();

            tileMapForm.MdiParent = this;
            tileMapForm.Show();

            tileForm.MdiParent = this;
            tileForm.Pallette = Program.Project.Palletts.First();
            tileForm.colors = Program.Project.Tiles.First().Pixels.ToArray();
            tileForm.Show();

            projectForm.MdiParent = this;
            projectForm.SpriteForm = spriteForm;
            projectForm.Show();
        }
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void palletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
