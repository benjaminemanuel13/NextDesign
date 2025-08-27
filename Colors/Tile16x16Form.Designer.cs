namespace Colors
{
    partial class Tile16x16Form
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
            text = new TextBox();
            generate = new Button();
            save = new Button();
            select = new Button();
            not = new Label();
            import = new Button();
            SuspendLayout();
            // 
            // text
            // 
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            text.Location = new Point(335, 12);
            text.Multiline = true;
            text.Name = "text";
            text.ScrollBars = ScrollBars.Vertical;
            text.Size = new Size(519, 454);
            text.TabIndex = 1;
            // 
            // generate
            // 
            generate.Location = new Point(237, 415);
            generate.Name = "generate";
            generate.Size = new Size(92, 23);
            generate.TabIndex = 2;
            generate.Text = "Generate >>";
            generate.UseVisualStyleBackColor = true;
            generate.Click += generate_Click;
            // 
            // save
            // 
            save.Enabled = false;
            save.Location = new Point(139, 415);
            save.Name = "save";
            save.Size = new Size(92, 23);
            save.TabIndex = 3;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // select
            // 
            select.Enabled = false;
            select.Location = new Point(12, 415);
            select.Name = "select";
            select.Size = new Size(121, 23);
            select.TabIndex = 6;
            select.Text = "Select for tilemap";
            select.UseVisualStyleBackColor = true;
            select.Click += select_Click;
            // 
            // not
            // 
            not.AutoSize = true;
            not.ForeColor = Color.IndianRed;
            not.Location = new Point(12, 397);
            not.Name = "not";
            not.Size = new Size(87, 15);
            not.TabIndex = 7;
            not.Text = "No Tile Loaded";
            // 
            // import
            // 
            import.Location = new Point(237, 444);
            import.Name = "import";
            import.Size = new Size(92, 23);
            import.TabIndex = 8;
            import.Text = "Import";
            import.UseVisualStyleBackColor = true;
            // 
            // Tile16x16Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 478);
            Controls.Add(import);
            Controls.Add(not);
            Controls.Add(select);
            Controls.Add(save);
            Controls.Add(generate);
            Controls.Add(text);
            Name = "Tile16x16Form";
            Text = "Tile (16x16)";
            Load += Tile16x16Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox text;
        private Button generate;
        private Button save;
        private Button select;
        private Label not;
        private Button import;
    }
}