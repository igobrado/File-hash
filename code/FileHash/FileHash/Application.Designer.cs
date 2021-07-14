
namespace FileHash
{
    partial class Application
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CalculateChecksumFromFiles = new System.Windows.Forms.Button();
            this.FildFilesFromChecksum = new System.Windows.Forms.Button();
            this.BackToMainScreen = new System.Windows.Forms.Button();
            this.CalculateChecksumFromFilesControl = new FileHash.CalculateChecksumFromFilesControl();
            this.FindChecksumFromGivenFilesOrFolderControl = new FileHash.FindChecksumFromGivenFilesOrFolder();
            this.SuspendLayout();
            // 
            // CalculateChecksumFromFiles
            // 
            this.CalculateChecksumFromFiles.Location = new System.Drawing.Point(229, 181);
            this.CalculateChecksumFromFiles.Name = "CalculateChecksumFromFiles";
            this.CalculateChecksumFromFiles.Size = new System.Drawing.Size(205, 23);
            this.CalculateChecksumFromFiles.TabIndex = 0;
            this.CalculateChecksumFromFiles.Text = "Calculate checksum from files";
            this.CalculateChecksumFromFiles.UseVisualStyleBackColor = true;
            // 
            // FildFilesFromChecksum
            // 
            this.FildFilesFromChecksum.Location = new System.Drawing.Point(229, 210);
            this.FildFilesFromChecksum.Name = "FildFilesFromChecksum";
            this.FildFilesFromChecksum.Size = new System.Drawing.Size(205, 23);
            this.FildFilesFromChecksum.TabIndex = 1;
            this.FildFilesFromChecksum.Text = "Find files from given checksum";
            this.FildFilesFromChecksum.UseVisualStyleBackColor = true;
            // 
            // BackToMainScreen
            // 
            this.BackToMainScreen.Location = new System.Drawing.Point(652, 376);
            this.BackToMainScreen.Name = "BackToMainScreen";
            this.BackToMainScreen.Size = new System.Drawing.Size(75, 23);
            this.BackToMainScreen.TabIndex = 2;
            this.BackToMainScreen.Text = "Back";
            this.BackToMainScreen.UseVisualStyleBackColor = true;
            // 
            // CalculateChecksumFromFilesControl
            // 
            this.CalculateChecksumFromFilesControl.Location = new System.Drawing.Point(6, 3);
            this.CalculateChecksumFromFilesControl.Name = "CalculateChecksumFromFilesControl";
            this.CalculateChecksumFromFilesControl.Size = new System.Drawing.Size(721, 383);
            this.CalculateChecksumFromFilesControl.TabIndex = 3;
            this.CalculateChecksumFromFilesControl.Visible = false;
            // 
            // FindChecksumFromGivenFilesOrFolderControl
            // 
            this.FindChecksumFromGivenFilesOrFolderControl.Location = new System.Drawing.Point(0, 3);
            this.FindChecksumFromGivenFilesOrFolderControl.Name = "FindChecksumFromGivenFilesOrFolderControl";
            this.FindChecksumFromGivenFilesOrFolderControl.Size = new System.Drawing.Size(752, 383);
            this.FindChecksumFromGivenFilesOrFolderControl.TabIndex = 4;
            this.FindChecksumFromGivenFilesOrFolderControl.Visible = false;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 405);
            this.Controls.Add(this.BackToMainScreen);
            this.Controls.Add(this.FildFilesFromChecksum);
            this.Controls.Add(this.CalculateChecksumFromFiles);
            this.Controls.Add(this.CalculateChecksumFromFilesControl);
            this.Controls.Add(this.FindChecksumFromGivenFilesOrFolderControl);
            this.Name = "Application";
            this.Text = "Applicaiton";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CalculateChecksumFromFiles;
        private System.Windows.Forms.Button FildFilesFromChecksum;
        private System.Windows.Forms.Button BackToMainScreen;
        private CalculateChecksumFromFilesControl CalculateChecksumFromFilesControl;
        private FindChecksumFromGivenFilesOrFolder FindChecksumFromGivenFilesOrFolderControl;
    }
}

