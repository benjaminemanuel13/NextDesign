namespace Colors
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fIleToolStripMenuItem = new ToolStripMenuItem();
            newProjectToolStripMenuItem = new ToolStripMenuItem();
            openProjectToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem1 = new ToolStripMenuItem();
            generateProjectToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            exportBINFilesToolStripMenuItem = new ToolStripMenuItem();
            windowToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem = new ToolStripMenuItem();
            pallettespritesToolStripMenuItem = new ToolStripMenuItem();
            x8TileToolStripMenuItem = new ToolStripMenuItem();
            x16TileToolStripMenuItem = new ToolStripMenuItem();
            tileMapToolStripMenuItem = new ToolStripMenuItem();
            yourAssistantToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fIleToolStripMenuItem, projectToolStripMenuItem1, exportToolStripMenuItem, windowToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(680, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            fIleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newProjectToolStripMenuItem, openProjectToolStripMenuItem, toolStripMenuItem1, exitToolStripMenuItem });
            fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            fIleToolStripMenuItem.Size = new Size(37, 20);
            fIleToolStripMenuItem.Text = "FIle";
            // 
            // newProjectToolStripMenuItem
            // 
            newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            newProjectToolStripMenuItem.Size = new Size(143, 22);
            newProjectToolStripMenuItem.Text = "New Project";
            newProjectToolStripMenuItem.Click += newProjectToolStripMenuItem_Click;
            // 
            // openProjectToolStripMenuItem
            // 
            openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            openProjectToolStripMenuItem.Size = new Size(143, 22);
            openProjectToolStripMenuItem.Text = "Open Project";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(143, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // projectToolStripMenuItem1
            // 
            projectToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { generateProjectToolStripMenuItem });
            projectToolStripMenuItem1.Name = "projectToolStripMenuItem1";
            projectToolStripMenuItem1.Size = new Size(56, 20);
            projectToolStripMenuItem1.Text = "Project";
            // 
            // generateProjectToolStripMenuItem
            // 
            generateProjectToolStripMenuItem.Name = "generateProjectToolStripMenuItem";
            generateProjectToolStripMenuItem.Size = new Size(161, 22);
            generateProjectToolStripMenuItem.Text = "Generate Project";
            generateProjectToolStripMenuItem.Click += generateProjectToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportBINFilesToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(52, 20);
            exportToolStripMenuItem.Text = "Export";
            // 
            // exportBINFilesToolStripMenuItem
            // 
            exportBINFilesToolStripMenuItem.Name = "exportBINFilesToolStripMenuItem";
            exportBINFilesToolStripMenuItem.Size = new Size(156, 22);
            exportBINFilesToolStripMenuItem.Text = "Export .BIN files";
            exportBINFilesToolStripMenuItem.Click += exportBINFilesToolStripMenuItem_Click;
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { projectToolStripMenuItem, pallettespritesToolStripMenuItem, x8TileToolStripMenuItem, x16TileToolStripMenuItem, tileMapToolStripMenuItem, yourAssistantToolStripMenuItem });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new Size(63, 20);
            windowToolStripMenuItem.Text = "W&indow";
            // 
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new Size(180, 22);
            projectToolStripMenuItem.Text = "Project";
            projectToolStripMenuItem.Click += projectToolStripMenuItem_Click;
            // 
            // pallettespritesToolStripMenuItem
            // 
            pallettespritesToolStripMenuItem.Name = "pallettespritesToolStripMenuItem";
            pallettespritesToolStripMenuItem.Size = new Size(180, 22);
            pallettespritesToolStripMenuItem.Text = "256 Pallette (sprites)";
            pallettespritesToolStripMenuItem.Click += pallettespritesToolStripMenuItem_Click_1;
            // 
            // x8TileToolStripMenuItem
            // 
            x8TileToolStripMenuItem.Name = "x8TileToolStripMenuItem";
            x8TileToolStripMenuItem.Size = new Size(180, 22);
            x8TileToolStripMenuItem.Text = "8x8 Tile";
            x8TileToolStripMenuItem.Click += x8TileToolStripMenuItem_Click_1;
            // 
            // x16TileToolStripMenuItem
            // 
            x16TileToolStripMenuItem.Name = "x16TileToolStripMenuItem";
            x16TileToolStripMenuItem.Size = new Size(180, 22);
            x16TileToolStripMenuItem.Text = "16x16 Tile";
            x16TileToolStripMenuItem.Click += x16TileToolStripMenuItem_Click_1;
            // 
            // tileMapToolStripMenuItem
            // 
            tileMapToolStripMenuItem.Name = "tileMapToolStripMenuItem";
            tileMapToolStripMenuItem.Size = new Size(180, 22);
            tileMapToolStripMenuItem.Text = "Tile Map";
            tileMapToolStripMenuItem.Click += tileMapToolStripMenuItem_Click_1;
            // 
            // yourAssistantToolStripMenuItem
            // 
            yourAssistantToolStripMenuItem.Name = "yourAssistantToolStripMenuItem";
            yourAssistantToolStripMenuItem.Size = new Size(180, 22);
            yourAssistantToolStripMenuItem.Text = "Your Assistant";
            yourAssistantToolStripMenuItem.Click += yourAssistantToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 296);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Smile Game Editor";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fIleToolStripMenuItem;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripMenuItem openProjectToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem pallettespritesToolStripMenuItem;
        private ToolStripMenuItem x8TileToolStripMenuItem;
        private ToolStripMenuItem x16TileToolStripMenuItem;
        private ToolStripMenuItem tileMapToolStripMenuItem;
        private ToolStripMenuItem projectToolStripMenuItem1;
        private ToolStripMenuItem generateProjectToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem exportBINFilesToolStripMenuItem;
        private ToolStripMenuItem yourAssistantToolStripMenuItem;
    }
}