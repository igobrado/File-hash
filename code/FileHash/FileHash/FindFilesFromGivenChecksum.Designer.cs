
using System.Windows.Forms;

namespace FileHash
{
    partial class FindChecksumFromGivenFilesOrFolder
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
            this.FindFilesButton = new System.Windows.Forms.Button();
            this.ChecksumTextbox = new System.Windows.Forms.TextBox();
            this.SearchFolder = new System.Windows.Forms.TextBox();
            this.ChecksumLabel = new System.Windows.Forms.Label();
            this.SearchFolderButton = new System.Windows.Forms.Button();
            this.FoundFiles = new System.Windows.Forms.ListView();
            this.ListboxTitleLabel = new System.Windows.Forms.Label();
            this.ControlName = new System.Windows.Forms.Label();
            this.HashingAlgorithmsGroupbox = new System.Windows.Forms.GroupBox();
            this.MD5Checkbox = new System.Windows.Forms.CheckBox();
            this.SHA1Checkbox = new System.Windows.Forms.CheckBox();
            this.SHA256Checkbox = new System.Windows.Forms.CheckBox();
            this.HashingAlgorithmsGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindFilesButton
            // 
            this.FindFilesButton.Location = new System.Drawing.Point(240, 318);
            this.FindFilesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FindFilesButton.Name = "FindFilesButton";
            this.FindFilesButton.Size = new System.Drawing.Size(195, 25);
            this.FindFilesButton.TabIndex = 0;
            this.FindFilesButton.Text = "Find files";
            this.FindFilesButton.UseVisualStyleBackColor = true;
            this.FindFilesButton.Click += new System.EventHandler(this.FindFilesButton_Click);
            // 
            // ChecksumTextbox
            // 
            this.ChecksumTextbox.Location = new System.Drawing.Point(72, 279);
            this.ChecksumTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ChecksumTextbox.Name = "ChecksumTextbox";
            this.ChecksumTextbox.Size = new System.Drawing.Size(363, 23);
            this.ChecksumTextbox.TabIndex = 1;
            // 
            // SearchFolder
            // 
            this.SearchFolder.Location = new System.Drawing.Point(91, 249);
            this.SearchFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchFolder.Name = "SearchFolder";
            this.SearchFolder.Size = new System.Drawing.Size(344, 23);
            this.SearchFolder.TabIndex = 3;
            // 
            // ChecksumLabel
            // 
            this.ChecksumLabel.AutoSize = true;
            this.ChecksumLabel.Location = new System.Drawing.Point(9, 282);
            this.ChecksumLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ChecksumLabel.Name = "ChecksumLabel";
            this.ChecksumLabel.Size = new System.Drawing.Size(63, 15);
            this.ChecksumLabel.TabIndex = 5;
            this.ChecksumLabel.Text = "Checksum";
            // 
            // SearchFolderButton
            // 
            this.SearchFolderButton.Location = new System.Drawing.Point(1, 249);
            this.SearchFolderButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchFolderButton.Name = "SearchFolderButton";
            this.SearchFolderButton.Size = new System.Drawing.Size(84, 22);
            this.SearchFolderButton.TabIndex = 6;
            this.SearchFolderButton.Text = "Search folder";
            this.SearchFolderButton.UseVisualStyleBackColor = true;
            this.SearchFolderButton.Click += new System.EventHandler(this.SearchFolderButton_Click);
            // 
            // FoundFiles
            // 
            this.FoundFiles.HideSelection = false;
            this.FoundFiles.Location = new System.Drawing.Point(9, 98);
            this.FoundFiles.Name = "FoundFiles";
            this.FoundFiles.Size = new System.Drawing.Size(426, 145);
            this.FoundFiles.TabIndex = 7;
            this.FoundFiles.UseCompatibleStateImageBehavior = false;
            this.FoundFiles.View = View.List;
            // 
            // ListboxTitleLabel
            // 
            this.ListboxTitleLabel.AutoSize = true;
            this.ListboxTitleLabel.Location = new System.Drawing.Point(9, 80);
            this.ListboxTitleLabel.Name = "ListboxTitleLabel";
            this.ListboxTitleLabel.Size = new System.Drawing.Size(207, 15);
            this.ListboxTitleLabel.TabIndex = 8;
            this.ListboxTitleLabel.Text = "Files that are used to create checksum";
            // 
            // ControlName
            // 
            this.ControlName.AutoSize = true;
            this.ControlName.Location = new System.Drawing.Point(9, 19);
            this.ControlName.Name = "ControlName";
            this.ControlName.Size = new System.Drawing.Size(172, 15);
            this.ControlName.TabIndex = 9;
            this.ControlName.Text = "Find files from given checksum";
            // 
            // HashingAlgorithmsGroupbox
            // 
            this.HashingAlgorithmsGroupbox.Controls.Add(this.MD5Checkbox);
            this.HashingAlgorithmsGroupbox.Controls.Add(this.SHA1Checkbox);
            this.HashingAlgorithmsGroupbox.Controls.Add(this.SHA256Checkbox);
            this.HashingAlgorithmsGroupbox.Location = new System.Drawing.Point(486, 98);
            this.HashingAlgorithmsGroupbox.Name = "HashingAlgorithmsGroupbox";
            this.HashingAlgorithmsGroupbox.Size = new System.Drawing.Size(200, 100);
            this.HashingAlgorithmsGroupbox.TabIndex = 10;
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
            // FindChecksumFromGivenFilesOrFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HashingAlgorithmsGroupbox);
            this.Controls.Add(this.ControlName);
            this.Controls.Add(this.ListboxTitleLabel);
            this.Controls.Add(this.FoundFiles);
            this.Controls.Add(this.SearchFolderButton);
            this.Controls.Add(this.ChecksumLabel);
            this.Controls.Add(this.SearchFolder);
            this.Controls.Add(this.ChecksumTextbox);
            this.Controls.Add(this.FindFilesButton);
            this.Name = "FindChecksumFromGivenFilesOrFolder";
            this.Size = new System.Drawing.Size(750, 383);
            this.HashingAlgorithmsGroupbox.ResumeLayout(false);
            this.HashingAlgorithmsGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindFilesButton;
        private System.Windows.Forms.TextBox ChecksumTextbox;
        private System.Windows.Forms.TextBox SearchFolder;
        private System.Windows.Forms.Label ChecksumLabel;
        private System.Windows.Forms.Button SearchFolderButton;
        private System.Windows.Forms.ListView FoundFiles;
        private System.Windows.Forms.Label ListboxTitleLabel;
        private System.Windows.Forms.Label ControlName;
        private System.Windows.Forms.GroupBox HashingAlgorithmsGroupbox;
        private System.Windows.Forms.CheckBox MD5Checkbox;
        private System.Windows.Forms.CheckBox SHA1Checkbox;
        private System.Windows.Forms.CheckBox SHA256Checkbox;
    }
}
