using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileHashBackend;
using System.Diagnostics;

namespace FileHash
{
    public partial class CalculateChecksumFromFilesControl : UserControl
    {
        public CalculateChecksumFromFilesControl()
        {
            InitializeComponent();
            Load += LoadOpenFileDialog;
            Load += InitializeSelectedFilesListbox;
        }

        public void ResetToInitialState()
        {
            Visible = false;
            Enabled = false;

            for (int i = 0; i < SelectedFiles.Items.Count; ++i)
            {
                SelectedFiles.Items[i].Text = "";
            }

            SHA256Checkbox.Checked = false;
            SHA1Checkbox.Checked = false;
            MD5Checkbox.Checked = false;
            ChecksumTextbox.Text = "";
        }

        private void LoadOpenFileDialog(object sender, EventArgs e)
        {
            _openFileDialog = new OpenFileDialog { 
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
                Multiselect = true
             };
        }

        private void Select_Click(object sender, EventArgs e)
        {
            DialogResult result = _openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (var item in _openFileDialog.FileNames)
                {
                    SelectedFiles.Items.Add(item);
                }
            }
        }

        private void InitializeSelectedFilesListbox(object sender, EventArgs e)
        {
            SelectedFiles.View = View.List;
            SelectedFiles.MultiSelect = true;
            SelectedFiles.KeyDown += new KeyEventHandler(OnKeyPressed);
            SelectedFiles.MouseClick += new MouseEventHandler(OnMouseKeyPressed);

            ContextMenuStrip m = new ContextMenuStrip();
            // Add menu items to the MenuItems collection.
            m.Items.Add(new ToolStripMenuItem("Delete"));

            SelectedFiles.ContextMenuStrip = m;
            m.Click += OnMouseKeyPressedContextStripMenu;
        }

        protected virtual void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedFilesFromList(SelectedFiles.SelectedItems);
            }
        }

        protected virtual void OnMouseKeyPressed(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedFiles.ContextMenuStrip.Show();
            }
        }

        protected virtual void OnMouseKeyPressedContextStripMenu(object sender, EventArgs e)
        {
            var trueEvent = (MouseEventArgs)e;
            if (trueEvent == null)
            {
                return;
            }

            if (trueEvent.Button == MouseButtons.Left)
            {
                RemoveSelectedFilesFromList(SelectedFiles.SelectedItems);
            }
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            var files = new List<string>();

            for (int i = 0; i < SelectedFiles.Items.Count; ++i)
            {
                files.Add(SelectedFiles.Items[i].Text);
            }

            var hasherType = GetSelectedHasherType();
            if (hasherType == HasherType.Invalid)
            {
                MessageBox.Show("You need to select hashing algorithm type.", "Invalid settings",  MessageBoxButtons.OK);
                return;
            }

            using (var hasher = new Hasher(hasherType))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var hashSizeHashTuple = hasher.GetHash(files);
                ChecksumTextbox.Text =hashSizeHashTuple.Item1;
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                String message = String.Format("For files which are having size: {0}MB hashing lasted for {1}", hashSizeHashTuple.Item2.ToString(), elapsedTime);
                MessageBox.Show(message, "DEBUG", MessageBoxButtons.OK);
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

        private void RemoveSelectedFilesFromList(ListView.SelectedListViewItemCollection selectedList)
        {
            foreach (ListViewItem eachItem in selectedList)
            {
                SelectedFiles.Items.Remove(eachItem);
            }

            ChecksumTextbox.Text = "";
        }

        private OpenFileDialog _openFileDialog;
    }
}
