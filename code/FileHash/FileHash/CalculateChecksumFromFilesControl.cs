using FileHashBackend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileHash
{
    public partial class CalculateChecksumFromFilesControl : UserControl
    {
        public CalculateChecksumFromFilesControl()
        {
            InitializeComponent();
            Load += LoadOpenFileDialog;
            Load += InitializeSelectedFilesListbox;
            SelectedFiles.KeyDown += on_keypress;
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

        private void on_keypress(object sender, EventArgs e)
        {
            KeyEventArgs arg = (KeyEventArgs)e;

            if (arg == null)
            {
                return;
            }

            int indexOffset = 0;

            if (SelectedFiles.SelectedItems.Count == 1)
            {
                var itemIndex = SelectedFiles.SelectedItems[0].Index;

                if (arg.KeyCode == Keys.Up)
                {
                    indexOffset = -1;
                }
                else if (arg.KeyCode == Keys.Down)
                {
                    indexOffset = 1;
                }

                if (indexOffset != 0)
                {
                    var tmpString = SelectedFiles.Items[itemIndex].Text;

                    SelectedFiles.Items[itemIndex].Text = SelectedFiles.Items[itemIndex + indexOffset].Text;
                    SelectedFiles.Items[itemIndex + indexOffset].Text = tmpString;
                }
            }
        }

        private void LoadOpenFileDialog(object sender, EventArgs e)
        {
            _selectFolderDialog = new FolderBrowserDialog { };
        }

        private void Select_Click(object sender, EventArgs e)
        {
            DialogResult result = _selectFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var finder = new Finder();
                var files = finder.GetAllFilesInDirectory(new List<string> { _selectFolderDialog.SelectedPath });

                foreach (var file in files)
                {
                    SelectedFiles.Items.Add(file);
                }
            }
        }

        private void InitializeSelectedFilesListbox(object sender, EventArgs e)
        {
            SelectedFiles.View = View.SmallIcon;
            SelectedFiles.MultiSelect = true;
            SelectedFiles.KeyDown += new KeyEventHandler(OnListboxKeyDown);
            SelectedFiles.MouseClick += new MouseEventHandler(OnListboxMouseClick);

            ContextMenuStrip m = new ContextMenuStrip();
            // Add menu items to the MenuItems collection.
            m.Items.Add(new ToolStripMenuItem("Delete"));

            SelectedFiles.ContextMenuStrip = m;
            m.Click += OnMouseKeyPressedContextStripMenu;
        }

        protected void OnListboxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedFilesFromList(SelectedFiles.SelectedItems);
            }
        }

        protected void OnListboxMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedFiles.ContextMenuStrip.Show();
            }
        }

        protected void OnMouseKeyPressedContextStripMenu(object sender, EventArgs e)
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
                ProgressBarForm progressBarForm = new ProgressBarForm();
                
                Task.Run(() => progressBarForm.ShowDialog());
                Stopwatch sw = new Stopwatch();

                sw.Start();
                var hashSizeHashTuple = hasher.GetHash(files);
                sw.Stop();

                progressBarForm.Stop();

                ChecksumTextbox.Text = hashSizeHashTuple.Item1;
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00}s",
                    ts.Hours, ts.Minutes, ts.Seconds);

                MessageBox.Show(String.Format("For files which are having size: {0}MB hashing lasted for {1}", hashSizeHashTuple.Item2.ToString("#.##"), elapsedTime), "Operation success", MessageBoxButtons.OK);

                progressBarForm.Close();
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

        private FolderBrowserDialog _selectFolderDialog;
    }
}
