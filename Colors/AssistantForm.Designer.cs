namespace Colors
{
    partial class AssistantForm
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
            speakTest = new Button();
            label1 = new Label();
            output = new RichTextBox();
            label2 = new Label();
            input = new TextBox();
            enableRecognition = new CheckBox();
            speakOn = new CheckBox();
            SuspendLayout();
            // 
            // speakTest
            // 
            speakTest.Location = new Point(379, 563);
            speakTest.Name = "speakTest";
            speakTest.Size = new Size(75, 23);
            speakTest.TabIndex = 0;
            speakTest.Text = "Submit";
            speakTest.UseVisualStyleBackColor = true;
            speakTest.Click += speakTest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 1;
            label1.Text = "Messages";
            // 
            // output
            // 
            output.BorderStyle = BorderStyle.FixedSingle;
            output.Location = new Point(12, 27);
            output.Name = "output";
            output.ReadOnly = true;
            output.Size = new Size(442, 399);
            output.TabIndex = 2;
            output.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 444);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 3;
            label2.Text = "Your Message";
            // 
            // input
            // 
            input.BorderStyle = BorderStyle.FixedSingle;
            input.Location = new Point(12, 462);
            input.Multiline = true;
            input.Name = "input";
            input.Size = new Size(442, 95);
            input.TabIndex = 4;
            // 
            // enableRecognition
            // 
            enableRecognition.AutoSize = true;
            enableRecognition.Location = new Point(12, 566);
            enableRecognition.Name = "enableRecognition";
            enableRecognition.Size = new Size(121, 19);
            enableRecognition.TabIndex = 5;
            enableRecognition.Text = "Recognise Speech";
            enableRecognition.UseVisualStyleBackColor = true;
            // 
            // speakOn
            // 
            speakOn.AutoSize = true;
            speakOn.Checked = true;
            speakOn.CheckState = CheckState.Checked;
            speakOn.Location = new Point(157, 567);
            speakOn.Name = "speakOn";
            speakOn.Size = new Size(112, 19);
            speakOn.TabIndex = 6;
            speakOn.Text = "Assistant Speaks";
            speakOn.UseVisualStyleBackColor = true;
            // 
            // AssistantForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 600);
            Controls.Add(speakOn);
            Controls.Add(enableRecognition);
            Controls.Add(input);
            Controls.Add(label2);
            Controls.Add(output);
            Controls.Add(label1);
            Controls.Add(speakTest);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AssistantForm";
            ShowInTaskbar = false;
            Text = "Your Assistant";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button speakTest;
        private Label label1;
        private RichTextBox output;
        private Label label2;
        private TextBox input;
        private CheckBox enableRecognition;
        private CheckBox speakOn;
    }
}