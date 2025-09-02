namespace Colors
{
    partial class Sprite16x16Form
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
            not = new Label();
            animationIndex = new ComboBox();
            animation = new CheckBox();
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
            // not
            // 
            not.AutoSize = true;
            not.ForeColor = Color.IndianRed;
            not.Location = new Point(35, 415);
            not.Name = "not";
            not.Size = new Size(98, 15);
            not.TabIndex = 7;
            not.Text = "No Sprite Loaded";
            // 
            // animationIndex
            // 
            animationIndex.Enabled = false;
            animationIndex.FormattingEnabled = true;
            animationIndex.Items.AddRange(new object[] { "1" });
            animationIndex.Location = new Point(788, 444);
            animationIndex.Name = "animationIndex";
            animationIndex.Size = new Size(66, 23);
            animationIndex.TabIndex = 9;
            // 
            // animation
            // 
            animation.AutoSize = true;
            animation.Location = new Point(700, 444);
            animation.Name = "animation";
            animation.Size = new Size(82, 19);
            animation.TabIndex = 10;
            animation.Text = "Animation";
            animation.UseVisualStyleBackColor = true;
            animation.CheckedChanged += animation_CheckedChanged;
            // 
            // Sprite16x16Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 476);
            Controls.Add(animation);
            Controls.Add(animationIndex);
            Controls.Add(not);
            Controls.Add(save);
            Controls.Add(generate);
            Controls.Add(textBox1);
            Name = "Sprite16x16Form";
            Text = "Sprite";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button generate;
        private Button save;
        private Label not;
        private ComboBox animationIndex;
        private CheckBox animation;
    }
}