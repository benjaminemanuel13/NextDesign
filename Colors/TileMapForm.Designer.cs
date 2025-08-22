namespace Colors
{
    partial class TileMapForm
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
            save = new Button();
            button1 = new Button();
            mode = new ComboBox();
            SuspendLayout();
            // 
            // save
            // 
            save.Location = new Point(12, 518);
            save.Name = "save";
            save.Size = new Size(75, 23);
            save.TabIndex = 0;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // button1
            // 
            button1.Location = new Point(93, 518);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Export...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // mode
            // 
            mode.DropDownStyle = ComboBoxStyle.DropDownList;
            mode.FormattingEnabled = true;
            mode.Items.AddRange(new object[] { "Tile Map", "Paths" });
            mode.Location = new Point(174, 519);
            mode.Name = "mode";
            mode.Size = new Size(121, 23);
            mode.TabIndex = 2;
            mode.SelectedIndexChanged += mode_SelectedIndexChanged;
            mode.Click += mode_Click;
            // 
            // TileMapForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 553);
            Controls.Add(mode);
            Controls.Add(button1);
            Controls.Add(save);
            Name = "TileMapForm";
            Text = "TileMapForm";
            Load += TileMapForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button save;
        private Button button1;
        private ComboBox mode;
    }
}