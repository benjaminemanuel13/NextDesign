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
            SuspendLayout();
            // 
            // text
            // 
            text.BorderStyle = BorderStyle.FixedSingle;
            text.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            text.Location = new Point(335, 12);
            text.Multiline = true;
            text.Name = "text";
            text.Size = new Size(519, 426);
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
            // Tile16x16Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 450);
            Controls.Add(save);
            Controls.Add(generate);
            Controls.Add(text);
            Name = "Tile16x16Form";
            Text = "Tile (16x16)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox text;
        private Button generate;
        private Button save;
    }
}