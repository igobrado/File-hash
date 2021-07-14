using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileHashBackend;
using System.Diagnostics;

namespace FileHash
{
    public partial class FindChecksumFromGivenFilesOrFolder : UserControl
    {
        public FindChecksumFromGivenFilesOrFolder()
        {
            InitializeComponent();
            Load += InitializeFolderBrowserDialog;
        }

        private void InitializeFolderBrowserDialog(object sender, EventArgs e)
        {
            _folderBrowserDialog = new FolderBrowserDialog();
        }

        public void ResetToInitialState()
        {
            for(int i = 0; i < FoundFiles.SelectedItems.Count; ++i)
            {
                FoundFiles.SelectedItems[i].Text = "";
            }

            SHA1Checkbox.Checked = false;
            SHA256Checkbox.Checked = false;
            MD5Checkbox.Checked = false;
            SearchFolder.Text = "";

            Visible = false;
            Enabled = false;
        }

        private void SearchFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = _folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                SearchFolder.Text = _folderBrowserDialog.SelectedPath;
            }
        }

        private void FindFilesButton_Click(object sender, EventArgs e)
        {
            // TODO: add listbox to have possibility of multiple folders
            List<string> folderPaths = new List<string>();
            folderPaths.Add(SearchFolder.Text);

            var hasherType = GetSelectedHasherType();
            if (hasherType == HasherType.Invalid)
            {
                MessageBox.Show("You need to select hashing algorithm type.", "Invalid settings", MessageBoxButtons.OK);
                return;
            }

            using (var hasher = new Hasher(hasherType))
            {
                Finder finder = new Finder();
#if DEBUG
                Stopwatch sw = new Stopwatch();
                sw.Start();
#endif
                var files = finder.Find(folderPaths, ChecksumTextbox.Text, hasher);
#if DEBUG
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
#endif
                if (files.Item1 == FindStatus.FilesFound)
                {
                    int i = 1;
                    foreach (var file in files.Item2)
                    {
                        String line = String.Format("{0} {1}", i, file);
                        FoundFiles.Items.Add(line);
                        ++i;
                    }
#if DEBUG
                    String message = String.Format("Finding of files has been successfull! Time elapsed: {0}", elapsedTime);
                    MessageBox.Show(message, "", MessageBoxButtons.OK);
#endif
                }
                else if (files.Item1 == FindStatus.InvalidArguments)
                { // shall not happen
                    MessageBox.Show("Invalid arguments, you need to provide checksum and folders to search", "Invalid argument error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String message = String.Format("No files found which are mathcing to checksum {0}.", ChecksumLabel.Text);
                    MessageBox.Show(message, "No files found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private HasherType GetSelectedHasherType()
        {
            if (SHA1Checkbox.Checked)
            {
                return HasherType.SHA1;
            }
            else if (SHA256Checkbox.Checked)
            {
                return HasherType.SHA256;
            }
            else if (MD5Checkbox.Checked)
            {
                return HasherType.MD5;
            }

            return HasherType.Invalid;
        }

        private FolderBrowserDialog _folderBrowserDialog;
    }
}
