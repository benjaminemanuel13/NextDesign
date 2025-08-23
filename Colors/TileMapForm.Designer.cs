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
            pathPanel = new Panel();
            moveDown = new Button();
            moveUp = new Button();
            removeStep = new Button();
            addStep = new Button();
            remove = new Button();
            steps = new ListBox();
            label3 = new Label();
            messages = new TextBox();
            label2 = new Label();
            addPath = new Button();
            paths = new ListBox();
            label1 = new Label();
            pathPanel.SuspendLayout();
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
            // pathPanel
            // 
            pathPanel.Controls.Add(moveDown);
            pathPanel.Controls.Add(moveUp);
            pathPanel.Controls.Add(removeStep);
            pathPanel.Controls.Add(addStep);
            pathPanel.Controls.Add(remove);
            pathPanel.Controls.Add(steps);
            pathPanel.Controls.Add(label3);
            pathPanel.Controls.Add(messages);
            pathPanel.Controls.Add(label2);
            pathPanel.Controls.Add(addPath);
            pathPanel.Controls.Add(paths);
            pathPanel.Controls.Add(label1);
            pathPanel.Location = new Point(669, 13);
            pathPanel.Name = "pathPanel";
            pathPanel.Size = new Size(221, 529);
            pathPanel.TabIndex = 8;
            pathPanel.Visible = false;
            // 
            // moveDown
            // 
            moveDown.Location = new Point(129, 479);
            moveDown.Name = "moveDown";
            moveDown.Size = new Size(75, 23);
            moveDown.TabIndex = 18;
            moveDown.Text = "Move Down";
            moveDown.UseVisualStyleBackColor = true;
            // 
            // moveUp
            // 
            moveUp.Location = new Point(129, 450);
            moveUp.Name = "moveUp";
            moveUp.Size = new Size(75, 23);
            moveUp.TabIndex = 17;
            moveUp.Text = "Move Up";
            moveUp.UseVisualStyleBackColor = true;
            // 
            // removeStep
            // 
            removeStep.Location = new Point(129, 317);
            removeStep.Name = "removeStep";
            removeStep.Size = new Size(75, 23);
            removeStep.TabIndex = 9;
            removeStep.Text = "Remove";
            removeStep.UseVisualStyleBackColor = true;
            // 
            // addStep
            // 
            addStep.Location = new Point(129, 288);
            addStep.Name = "addStep";
            addStep.Size = new Size(75, 23);
            addStep.TabIndex = 16;
            addStep.Text = "Add";
            addStep.UseVisualStyleBackColor = true;
            // 
            // remove
            // 
            remove.Location = new Point(129, 48);
            remove.Name = "remove";
            remove.Size = new Size(75, 23);
            remove.TabIndex = 15;
            remove.Text = "Remove";
            remove.UseVisualStyleBackColor = true;
            // 
            // steps
            // 
            steps.FormattingEnabled = true;
            steps.Location = new Point(3, 288);
            steps.Name = "steps";
            steps.Size = new Size(120, 214);
            steps.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 270);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 13;
            label3.Text = "Steps";
            // 
            // messages
            // 
            messages.BorderStyle = BorderStyle.FixedSingle;
            messages.Location = new Point(3, 190);
            messages.Multiline = true;
            messages.Name = "messages";
            messages.ReadOnly = true;
            messages.Size = new Size(201, 60);
            messages.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 172);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 11;
            label2.Text = "Messages";
            // 
            // addPath
            // 
            addPath.Location = new Point(129, 19);
            addPath.Name = "addPath";
            addPath.Size = new Size(75, 23);
            addPath.TabIndex = 10;
            addPath.Text = "Add";
            addPath.UseVisualStyleBackColor = true;
            // 
            // paths
            // 
            paths.FormattingEnabled = true;
            paths.Location = new Point(3, 19);
            paths.Name = "paths";
            paths.Size = new Size(120, 139);
            paths.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 1);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 8;
            label1.Text = "Paths";
            // 
            // TileMapForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 553);
            Controls.Add(pathPanel);
            Controls.Add(mode);
            Controls.Add(button1);
            Controls.Add(save);
            Name = "TileMapForm";
            Text = "Tile Map / Path Editor";
            Load += TileMapForm_Load;
            pathPanel.ResumeLayout(false);
            pathPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button save;
        private Button button1;
        private ComboBox mode;
        private Panel pathPanel;
        private TextBox messages;
        private Label label2;
        private Button addPath;
        private ListBox paths;
        private Label label1;
        private Button moveDown;
        private Button moveUp;
        private Button removeStep;
        private Button addStep;
        private Button remove;
        private ListBox steps;
        private Label label3;
    }
}