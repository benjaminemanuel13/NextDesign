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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static TileMapForm TileMapForm { get; private set; } = null!;

        PalletteForm palletteForm = new PalletteForm();
        SpriteForm spriteForm = new SpriteForm();
        Tile8x8Form tileForm = new Tile8x8Form();
        Tile16x16Form tile16Form = new Tile16x16Form();
        TileMapForm tileMapForm = new TileMapForm();
        ProjectForm projectForm = new ProjectForm();
        AssistantForm assistantForm = new AssistantForm();

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
            tileForm.colors = Program.Project.Tiles8.First().Pixels.ToArray();
            tileForm.Show();

            tile16Form.MdiParent = this;
            tile16Form.Pallette = Program.Project.Palletts.First();
            tile16Form.Tile8X8Form = tileForm;
            tile16Form.colors = Program.Project.Tiles16.First().Pixels.ToArray();
            tile16Form.Show();

            TileMapForm = tileMapForm;
            tileMapForm.Tile8X8Form = tileForm;

            projectForm.MdiParent = this;
            projectForm.SpriteForm = spriteForm;
            projectForm.Tile8Form = tileForm;
            projectForm.Tile16Form = tile16Form;
            projectForm.TileMapForm = tileMapForm;
            projectForm.Show();

            assistantForm.MdiParent = this;
            assistantForm.Show();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void palletteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            projectForm.Show();
            projectForm.BringToFront();
        }

        private void pallettespritesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void x8TileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void x16TileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tileMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void x16TileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tile16Form.Show();
            tile16Form.BringToFront();
        }

        private void pallettespritesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            palletteForm.Show();
            palletteForm.BringToFront();
        }

        private void x8TileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tileForm.Show();
            tileForm.BringToFront();
        }

        private void tileMapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tileMapForm.Show();
            tileMapForm.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void generateProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var generateProjectForm = new GenerateProjectForm();
            generateProjectForm.ShowDialog(this);
        }

        private void exportBINFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
