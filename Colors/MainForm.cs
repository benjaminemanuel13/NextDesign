using Colors.Common.EventArguments;
using Colors.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Colors
{
    public partial class MainForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static TileMapForm TileMapForm { get; private set; } = null!;

        PalletteForm palletteForm = new PalletteForm();
        Sprite16x16Form spriteForm = new Sprite16x16Form();
        Tile8x8Form tileForm = new Tile8x8Form();
        Tile16x16Form tile16Form = new Tile16x16Form();
        TileMapForm tileMapForm = new TileMapForm();
        ProjectForm projectForm = new ProjectForm();
        AssistantForm assistantForm = new AssistantForm();

        public MainForm()
        {
            InitializeComponent();

            Orchestrator.ProjectFormToFrontEvent += Orchestrator_ProjectFormToFrontEvent;
            Orchestrator.SelectionMoveEvent += Orchestrator_SelectionMoveEvent;

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

        private void Orchestrator_SelectionMoveEvent(object? sender, SelectionMoveEventArgs e)
        {

        }

        private void Orchestrator_ProjectFormToFrontEvent(object? sender, ProjectFormToFrontEventArgs e)
        {
            string formName = e.FormName.ToLower();
            if (formName.Contains("project"))
            {
                projectForm.Invoke(new Action(() => projectForm.BringToFront()));
            }
            else if (formName.Contains("tile") && (formName.Contains("sixteen") || formName.Contains("16")))
            {
                tile16Form.Invoke(new Action(() => tile16Form.BringToFront()));
            }
            else if (formName.Contains("tile") && (formName.Contains("eight") || formName.Contains("8")))
            {
                tileForm.Invoke(new Action(() => tileForm.BringToFront()));
            }
            else if (e.FormName.ToLower().Contains("pallette"))
            {
                palletteForm.Invoke(new Action(() => palletteForm.BringToFront()));
            }
            else if ((formName.Contains("tile") && formName.Contains("tile")) || formName.Contains("tilemap"))
            {
                tileMapForm.Invoke(new Action(() => tileMapForm.BringToFront()));
            }
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

        private void yourAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var assistantForm = new AssistantForm();
            assistantForm.BringToFront();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            assistantForm.Top = 0;
            assistantForm.Left = this.ClientSize.Width - assistantForm.Width - 20;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            assistantForm.Top = 0;
            assistantForm.Left = this.ClientSize.Width - assistantForm.Width - 20;
        }
    }
}
