using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileHashBackend;
using System.Diagnostics;
using System.Threading.Tasks;

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
            FoundFiles.Items.Clear();

            ChecksumTextbox.Text = "";
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
                ProgressBarForm progressBar = new ProgressBarForm();

                Stopwatch sw = new Stopwatch();

                sw.Start();
                Task.Run(() => progressBar.ShowDialog());
                var findResult = finder.Find(folderPaths, ChecksumTextbox.Text, hasher);
                sw.Stop();
                progressBar.Stop();

                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00}s",
                    ts.Hours, ts.Minutes, ts.Seconds);

                if (findResult.findStatus == FindStatus.FilesFound)
                {
                    int i = 1;
                    foreach (var file in findResult.files)
                    {
                        String line = String.Format("{0} {1}", i, file);
                        FoundFiles.Items.Add(line);
                        ++i;
                    }

                    MessageBox.Show(String.Format("Finding of files has been successfull! Time elapsed: {0}", elapsedTime), "Find filče status", MessageBoxButtons.OK);
                }
                else if (findResult.findStatus == FindStatus.InvalidArguments)
                { // shall not happen
                    MessageBox.Show("Invalid arguments, you need to provide checksum and folders to search", "Invalid argument error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String message = String.Format("No files found which are mathcing to checksum {0} Time elapsed:{1}.", ChecksumLabel.Text, elapsedTime);
                    MessageBox.Show(message, "No files found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                progressBar.Close();
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
