namespace Colors
{
    partial class SpriteForm
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
            textBox1 = new TextBox();
            generate = new Button();
            save = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(335, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(519, 426);
            textBox1.TabIndex = 0;
            // 
            // generate
            // 
            generate.Location = new Point(237, 415);
            generate.Name = "generate";
            generate.Size = new Size(92, 23);
            generate.TabIndex = 1;
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
            save.TabIndex = 4;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // SpriteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 450);
            Controls.Add(save);
            Controls.Add(generate);
            Controls.Add(textBox1);
            Name = "SpriteForm";
            Text = "SpriteForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button generate;
        private Button save;
    }
}