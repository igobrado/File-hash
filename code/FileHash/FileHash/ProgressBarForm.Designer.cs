﻿
namespace FileHash
{
    partial class ProgressBarForm
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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressPercentage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 32);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(344, 27);
            this.ProgressBar.Step = 20;
            this.ProgressBar.TabIndex = 0;
            // 
            // label1
            // 
            this.ProgressPercentage.AutoSize = true;
            this.ProgressPercentage.Location = new System.Drawing.Point(171, 9);
            this.ProgressPercentage.Name = "label1";
            this.ProgressPercentage.Size = new System.Drawing.Size(23, 15);
            this.ProgressPercentage.TabIndex = 1;
            this.ProgressPercentage.Text = "0%";
            // 
            // ProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 76);
            this.Controls.Add(this.ProgressPercentage);
            this.Controls.Add(this.ProgressBar);
            this.Name = "ProgressBarForm";
            this.Text = "Hashing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressPercentage;
    }
}