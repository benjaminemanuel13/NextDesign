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
            // TileForm8x8
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(588, 409);
            Controls.Add(text);
            Controls.Add(generate);
            Name = "TileForm8x8";
            Text = "Tile (8x8)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generate;
        private TextBox text;
    }
}