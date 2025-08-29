namespace Colors
{
    partial class Tile8x8Form
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
            generate = new Button();
            text = new TextBox();
            save = new Button();
            select = new Button();
            not = new Label();
            SuspendLayout();
            // 
            // generate
            // 
            generate.Location = new Point(106, 288);
            generate.Name = "generate";
            generate.Size = new Size(93, 23);
            generate.TabIndex = 0;
            generate.Text = "Generate >>";
            generate.UseVisualStyleBackColor = true;
            generate.Click += generate_Click;
            // 
            // text
            // 
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            text.Location = new Point(205, 12);
            text.Multiline = true;
            text.Name = "text";
            text.Size = new Size(366, 299);
            text.TabIndex = 1;
            // 
            // save
            // 
            save.Enabled = false;
            save.Location = new Point(12, 288);
            save.Name = "save";
            save.Size = new Size(92, 23);
            save.TabIndex = 4;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // select
            // 
            select.Enabled = false;
            select.Location = new Point(384, 317);
            select.Name = "select";
            select.Size = new Size(187, 23);
            select.TabIndex = 5;
            select.Text = "Select for tilemap";
            select.UseVisualStyleBackColor = true;
            select.Click += select_Click;
            // 
            // not
            // 
            not.AutoSize = true;
            not.ForeColor = Color.IndianRed;
            not.Location = new Point(12, 270);
            not.Name = "not";
            not.Size = new Size(87, 15);
            not.TabIndex = 6;
            not.Text = "No Tile Loaded";
            // 
            // Tile8x8Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(588, 348);
            Controls.Add(not);
            Controls.Add(select);
            Controls.Add(save);
            Controls.Add(text);
            Controls.Add(generate);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Tile8x8Form";
            Text = "Tile (8x8)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generate;
        private TextBox text;
        private Button save;
        private Button select;
        private Label not;
    }
}