
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
            this.components = new System.ComponentModel.Container();
            this._hashingAlgorithmBox = new System.Windows.Forms.GroupBox();
            this._crc32HAlgo = new System.Windows.Forms.RadioButton();
            this._md5HAlgo = new System.Windows.Forms.RadioButton();
            this._sha256HAlgo = new System.Windows.Forms.RadioButton();
            this._shaOneHAlgo = new System.Windows.Forms.RadioButton();
            this._calculateHashButton = new System.Windows.Forms.Button();
            this._findFilesButton = new System.Windows.Forms.Button();
            this._filesGroupBox = new System.Windows.Forms.GroupBox();
            this._selectedFilesCheckbox = new System.Windows.Forms.CheckedListBox();
            this._moveDownButton = new System.Windows.Forms.Button();
            this._moveUpButton = new System.Windows.Forms.Button();
            this._removeFileButton = new System.Windows.Forms.Button();
            this._addFileButton = new System.Windows.Forms.Button();
            this._evaluatedHashTextbox = new System.Windows.Forms.TextBox();
            this._evaluatedHashLabel = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this._crc64HAlgo = new System.Windows.Forms.RadioButton();
            this._hashingAlgorithmBox.SuspendLayout();
            this._filesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _hashingAlgorithmBox
            // 
            this._hashingAlgorithmBox.Controls.Add(this._crc64HAlgo);
            this._hashingAlgorithmBox.Controls.Add(this._crc32HAlgo);
            this._hashingAlgorithmBox.Controls.Add(this._md5HAlgo);
            this._hashingAlgorithmBox.Controls.Add(this._sha256HAlgo);
            this._hashingAlgorithmBox.Controls.Add(this._shaOneHAlgo);
            this._hashingAlgorithmBox.Location = new System.Drawing.Point(408, 34);
            this._hashingAlgorithmBox.Name = "_hashingAlgorithmBox";
            this._hashingAlgorithmBox.Size = new System.Drawing.Size(238, 150);
            this._hashingAlgorithmBox.TabIndex = 0;
            this._hashingAlgorithmBox.TabStop = false;
            this._hashingAlgorithmBox.Text = "Hashing algorithm";
            // 
            // _crc32HAlgo
            // 
            this._crc32HAlgo.AutoSize = true;
            this._crc32HAlgo.Location = new System.Drawing.Point(15, 94);
            this._crc32HAlgo.Name = "_crc32HAlgo";
            this._crc32HAlgo.Size = new System.Drawing.Size(60, 19);
            this._crc32HAlgo.TabIndex = 3;
            this._crc32HAlgo.TabStop = true;
            this._crc32HAlgo.Text = "CRC32";
            this._crc32HAlgo.UseVisualStyleBackColor = true;
            // 
            // _md5HAlgo
            // 
            this._md5HAlgo.AutoSize = true;
            this._md5HAlgo.Location = new System.Drawing.Point(15, 69);
            this._md5HAlgo.Name = "_md5HAlgo";
            this._md5HAlgo.Size = new System.Drawing.Size(50, 19);
            this._md5HAlgo.TabIndex = 2;
            this._md5HAlgo.TabStop = true;
            this._md5HAlgo.Text = "MD5";
            this._md5HAlgo.UseVisualStyleBackColor = true;
            // 
            // _sha256HAlgo
            // 
            this._sha256HAlgo.AutoSize = true;
            this._sha256HAlgo.Location = new System.Drawing.Point(15, 44);
            this._sha256HAlgo.Name = "_sha256HAlgo";
            this._sha256HAlgo.Size = new System.Drawing.Size(66, 19);
            this._sha256HAlgo.TabIndex = 1;
            this._sha256HAlgo.TabStop = true;
            this._sha256HAlgo.Text = "SHA256";
            this._sha256HAlgo.UseVisualStyleBackColor = true;
            // 
            // _shaOneHAlgo
            // 
            this._shaOneHAlgo.AutoSize = true;
            this._shaOneHAlgo.Location = new System.Drawing.Point(15, 19);
            this._shaOneHAlgo.Name = "_shaOneHAlgo";
            this._shaOneHAlgo.Size = new System.Drawing.Size(54, 19);
            this._shaOneHAlgo.TabIndex = 0;
            this._shaOneHAlgo.TabStop = true;
            this._shaOneHAlgo.Text = "SHA1";
            this._shaOneHAlgo.UseVisualStyleBackColor = true;
            // 
            // _calculateHashButton
            // 
            this._calculateHashButton.Location = new System.Drawing.Point(489, 190);
            this._calculateHashButton.Name = "_calculateHashButton";
            this._calculateHashButton.Size = new System.Drawing.Size(157, 23);
            this._calculateHashButton.TabIndex = 1;
            this._calculateHashButton.Text = "Calculate hash";
            this._calculateHashButton.UseVisualStyleBackColor = true;
            // 
            // _findFilesButton
            // 
            this._findFilesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._findFilesButton.Location = new System.Drawing.Point(489, 219);
            this._findFilesButton.Name = "_findFilesButton";
            this._findFilesButton.Size = new System.Drawing.Size(157, 23);
            this._findFilesButton.TabIndex = 2;
            this._findFilesButton.Text = "Find files";
            this._findFilesButton.UseVisualStyleBackColor = true;
            // 
            // _filesGroupBox
            // 
            this._filesGroupBox.Controls.Add(this._selectedFilesCheckbox);
            this._filesGroupBox.Controls.Add(this._moveDownButton);
            this._filesGroupBox.Controls.Add(this._moveUpButton);
            this._filesGroupBox.Controls.Add(this._removeFileButton);
            this._filesGroupBox.Controls.Add(this._addFileButton);
            this._filesGroupBox.Location = new System.Drawing.Point(28, 12);
            this._filesGroupBox.Name = "_filesGroupBox";
            this._filesGroupBox.Size = new System.Drawing.Size(363, 263);
            this._filesGroupBox.TabIndex = 3;
            this._filesGroupBox.TabStop = false;
            this._filesGroupBox.Text = "Files";
            // 
            // _selectedFilesCheckbox
            // 
            this._selectedFilesCheckbox.FormattingEnabled = true;
            this._selectedFilesCheckbox.Location = new System.Drawing.Point(6, 22);
            this._selectedFilesCheckbox.Name = "_selectedFilesCheckbox";
            this._selectedFilesCheckbox.Size = new System.Drawing.Size(252, 220);
            this._selectedFilesCheckbox.TabIndex = 4;
            // 
            // _moveDownButton
            // 
            this._moveDownButton.Location = new System.Drawing.Point(264, 207);
            this._moveDownButton.Name = "_moveDownButton";
            this._moveDownButton.Size = new System.Drawing.Size(85, 23);
            this._moveDownButton.TabIndex = 3;
            this._moveDownButton.Text = "Move Down";
            this._moveDownButton.UseVisualStyleBackColor = true;
            // 
            // _moveUpButton
            // 
            this._moveUpButton.Location = new System.Drawing.Point(264, 178);
            this._moveUpButton.Name = "_moveUpButton";
            this._moveUpButton.Size = new System.Drawing.Size(85, 23);
            this._moveUpButton.TabIndex = 2;
            this._moveUpButton.Text = "Move Up";
            this._moveUpButton.UseVisualStyleBackColor = false;
            this._moveUpButton.UseWaitCursor = true;
            // 
            // _removeFileButton
            // 
            this._removeFileButton.Location = new System.Drawing.Point(264, 51);
            this._removeFileButton.Name = "_removeFileButton";
            this._removeFileButton.Size = new System.Drawing.Size(85, 23);
            this._removeFileButton.TabIndex = 1;
            this._removeFileButton.Text = "Remove";
            this._removeFileButton.UseVisualStyleBackColor = true;
            // 
            // _addFileButton
            // 
            this._addFileButton.Location = new System.Drawing.Point(264, 22);
            this._addFileButton.Name = "_addFileButton";
            this._addFileButton.Size = new System.Drawing.Size(85, 23);
            this._addFileButton.TabIndex = 0;
            this._addFileButton.Text = "Add";
            this._addFileButton.UseVisualStyleBackColor = true;
            // 
            // _evaluatedHashTextbox
            // 
            this._evaluatedHashTextbox.Location = new System.Drawing.Point(115, 281);
            this._evaluatedHashTextbox.Name = "_evaluatedHashTextbox";
            this._evaluatedHashTextbox.Size = new System.Drawing.Size(610, 23);
            this._evaluatedHashTextbox.TabIndex = 4;
            // 
            // _evaluatedHashLabel
            // 
            this._evaluatedHashLabel.AutoSize = true;
            this._evaluatedHashLabel.Location = new System.Drawing.Point(20, 284);
            this._evaluatedHashLabel.Name = "_evaluatedHashLabel";
            this._evaluatedHashLabel.Size = new System.Drawing.Size(66, 15);
            this._evaluatedHashLabel.TabIndex = 5;
            this._evaluatedHashLabel.Text = "Checksum:";
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(28, 325);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(697, 12);
            this._progressBar.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // _crc64HAlgo
            // 
            this._crc64HAlgo.AutoSize = true;
            this._crc64HAlgo.Location = new System.Drawing.Point(15, 119);
            this._crc64HAlgo.Name = "_crc64HAlgo";
            this._crc64HAlgo.Size = new System.Drawing.Size(60, 19);
            this._crc64HAlgo.TabIndex = 4;
            this._crc64HAlgo.TabStop = true;
            this._crc64HAlgo.Text = "CRC64";
            this._crc64HAlgo.UseVisualStyleBackColor = true;
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 348);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._evaluatedHashLabel);
            this.Controls.Add(this._evaluatedHashTextbox);
            this.Controls.Add(this._filesGroupBox);
            this.Controls.Add(this._findFilesButton);
            this.Controls.Add(this._calculateHashButton);
            this.Controls.Add(this._hashingAlgorithmBox);
            this.MaximumSize = new System.Drawing.Size(754, 387);
            this.MinimumSize = new System.Drawing.Size(754, 387);
            this.Name = "Application";
            this.Text = "Applicaiton";
            this._hashingAlgorithmBox.ResumeLayout(false);
            this._hashingAlgorithmBox.PerformLayout();
            this._filesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox _hashingAlgorithmBox;
        private System.Windows.Forms.RadioButton _shaOneHAlgo;
        private System.Windows.Forms.RadioButton _md5HAlgo;
        private System.Windows.Forms.RadioButton _sha256HAlgo;
        private System.Windows.Forms.RadioButton _crc32HAlgo;
        private System.Windows.Forms.Button _calculateHashButton;
        private System.Windows.Forms.Button _findFilesButton;
        private System.Windows.Forms.GroupBox _filesGroupBox;
        private System.Windows.Forms.CheckedListBox _selectedFilesCheckbox;
        private System.Windows.Forms.Button _moveDownButton;
        private System.Windows.Forms.Button _moveUpButton;
        private System.Windows.Forms.Button _removeFileButton;
        private System.Windows.Forms.Button _addFileButton;
        private System.Windows.Forms.TextBox _evaluatedHashTextbox;
        private System.Windows.Forms.Label _evaluatedHashLabel;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton _crc64HAlgo;
    }
}

