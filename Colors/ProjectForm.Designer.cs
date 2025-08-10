namespace Colors
{
    partial class ProjectForm
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
            project = new TreeView();
            label1 = new Label();
            SuspendLayout();
            // 
            // project
            // 
            project.Location = new Point(12, 33);
            project.Name = "project";
            project.Size = new Size(257, 252);
            project.TabIndex = 0;
            project.AfterSelect += project_AfterSelect;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 1;
            label1.Text = "Project";
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(283, 450);
            Controls.Add(label1);
            Controls.Add(project);
            Name = "ProjectForm";
            Text = "ProjectForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView project;
        private Label label1;
    }
}