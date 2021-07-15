
namespace FileHash
{
    partial class CalculateChecksumFromFilesControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = new System.Windows.Forms.Label();
            this.SelectedFiles = new System.Windows.Forms.ListView();
            this.Calculate = new System.Windows.Forms.Button();
            this.ChecksumTextbox = new System.Windows.Forms.TextBox();
            this.Select = new System.Windows.Forms.Button();
            this.HashingAlgorithmsGroupbox = new System.Windows.Forms.GroupBox();
            this.MD5Checkbox = new System.Windows.Forms.CheckBox();
            this.SHA1Checkbox = new System.Windows.Forms.CheckBox();
            this.SHA256Checkbox = new System.Windows.Forms.CheckBox();
            this.HashingAlgorithmsGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Location = new System.Drawing.Point(14, 17);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(166, 15);
            this.Title.TabIndex = 0;
            this.Title.Text = "Calculate checksum from files";
            // 
            // SelectedFiles
            // 
            this.SelectedFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.SelectedFiles.HideSelection = false;
            this.SelectedFiles.Location = new System.Drawing.Point(14, 61);
            this.SelectedFiles.Name = "SelectedFiles";
            this.SelectedFiles.Size = new System.Drawing.Size(322, 271);
            this.SelectedFiles.TabIndex = 1;
            this.SelectedFiles.UseCompatibleStateImageBehavior = false;
            this.SelectedFiles.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Calculate
            // 
            this.Calculate.Location = new System.Drawing.Point(190, 338);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(75, 23);
            this.Calculate.TabIndex = 2;
            this.Calculate.Text = "Calculate checksum";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // ChecksumTextbox
            // 
            this.ChecksumTextbox.Location = new System.Drawing.Point(271, 338);
            this.ChecksumTextbox.Name = "ChecksumTextbox";
            this.ChecksumTextbox.ReadOnly = true;
            this.ChecksumTextbox.Size = new System.Drawing.Size(442, 23);
            this.ChecksumTextbox.TabIndex = 3;
            // 
            // Select
            // 
            this.Select.Location = new System.Drawing.Point(14, 338);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(86, 23);
            this.Select.TabIndex = 4;
            this.Select.Text = "Select folder";
            this.Select.UseVisualStyleBackColor = true;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // HashingAlgorithmsGroupbox
            // 
            this.HashingAlgorithmsGroupbox.Controls.Add(this.MD5Checkbox);
            this.HashingAlgorithmsGroupbox.Controls.Add(this.SHA1Checkbox);
            this.HashingAlgorithmsGroupbox.Controls.Add(this.SHA256Checkbox);
            this.HashingAlgorithmsGroupbox.Location = new System.Drawing.Point(423, 61);
            this.HashingAlgorithmsGroupbox.Name = "HashingAlgorithmsGroupbox";
            this.HashingAlgorithmsGroupbox.Size = new System.Drawing.Size(200, 100);
            this.HashingAlgorithmsGroupbox.TabIndex = 5;
            this.HashingAlgorithmsGroupbox.TabStop = false;
            this.HashingAlgorithmsGroupbox.Text = "Hashing algorithm";
            // 
            // MD5Checkbox
            // 
            this.MD5Checkbox.AutoSize = true;
            this.MD5Checkbox.Location = new System.Drawing.Point(15, 69);
            this.MD5Checkbox.Name = "MD5Checkbox";
            this.MD5Checkbox.Size = new System.Drawing.Size(51, 19);
            this.MD5Checkbox.TabIndex = 2;
            this.MD5Checkbox.Text = "MD5";
            this.MD5Checkbox.UseVisualStyleBackColor = true;
            // 
            // SHA1Checkbox
            // 
            this.SHA1Checkbox.AutoSize = true;
            this.SHA1Checkbox.Location = new System.Drawing.Point(15, 44);
            this.SHA1Checkbox.Name = "SHA1Checkbox";
            this.SHA1Checkbox.Size = new System.Drawing.Size(55, 19);
            this.SHA1Checkbox.TabIndex = 1;
            this.SHA1Checkbox.Text = "SHA1";
            this.SHA1Checkbox.UseVisualStyleBackColor = true;
            // 
            // SHA256Checkbox
            // 
            this.SHA256Checkbox.AutoSize = true;
            this.SHA256Checkbox.Location = new System.Drawing.Point(15, 19);
            this.SHA256Checkbox.Name = "SHA256Checkbox";
            this.SHA256Checkbox.Size = new System.Drawing.Size(67, 19);
            this.SHA256Checkbox.TabIndex = 0;
            this.SHA256Checkbox.Text = "SHA256";
            this.SHA256Checkbox.UseVisualStyleBackColor = true;
            // 
            // CalculateChecksumFromFilesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HashingAlgorithmsGroupbox);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.ChecksumTextbox);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.SelectedFiles);
            this.Controls.Add(this.Title);
            this.Name = "CalculateChecksumFromFilesControl";
            this.Size = new System.Drawing.Size(750, 383);
            this.HashingAlgorithmsGroupbox.ResumeLayout(false);
            this.HashingAlgorithmsGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ListView SelectedFiles;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.TextBox ChecksumTextbox;
        private new System.Windows.Forms.Button Select;
        private System.Windows.Forms.GroupBox HashingAlgorithmsGroupbox;
        private System.Windows.Forms.CheckBox MD5Checkbox;
        private System.Windows.Forms.CheckBox SHA1Checkbox;
        private System.Windows.Forms.CheckBox SHA256Checkbox;
    }
}
