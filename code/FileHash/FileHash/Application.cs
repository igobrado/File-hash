﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using FileHashBackend;
using System.Drawing;
using Microsoft.VisualBasic.ApplicationServices;
using System.Threading;
using System.Threading.Tasks;

namespace FileHash
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
            Load += new EventHandler(InitializeModule);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            _previosOperation = Operation.NONE;
            _currentOperation = Operation.NONE;
        }

        private void Form1_Load(object sender, EventArgs e) => _oldSize = base.Size;
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            _oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - _oldSize.Width;
            control.Left += (control.Left * width) / _oldSize.Width;
            control.Width += (control.Width * width) / _oldSize.Width;

            int height = newSize.Height - _oldSize.Height;
            control.Top += (control.Top * height) / _oldSize.Height;
            control.Height += (control.Height * height) / _oldSize.Height;
        }

        private void InitializeModule(object sender, EventArgs e)
        {
            _filePaths = new List<string>();
            _fileDialog = new OpenFileDialog();
            _fileDialog.Multiselect = true;
            _addFileButton.Click += new EventHandler(OnAddFileClicked);
            _removeFileButton.Click += new EventHandler(OnRemoveClicked);
            _moveUpButton.Click += new EventHandler(OnItemMoveUpInTheList);
            _moveDownButton.Click += new EventHandler(OnItemMoveDownInTheList);
            _calculateHashButton.Click += new EventHandler(OnCalculate);
            _findFilesButton.Click += new EventHandler(OnFindFiles);
        }
        
        private void OnFindFiles(object sender, EventArgs e)
        {
            if (_currentOperation != Operation.NONE)
            {
                MessageBox.Show("Current operation in progress, please wait until operation is over!", "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (_selectedFilesCheckbox.Items.Count > 0)
            {
                MessageBox.Show("To enable file search by the checksum, you must clear the file list!", "Find files error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_evaluatedHashTextbox.Text.Length == 0)
            {
                MessageBox.Show("To enable file search by the checksum, you must enter a checksum that files are combining together.", "Find files error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var folders = new List<string>();
            _currentOperation = Operation.FINDING;
            _previosOperation = Operation.FINDING;
            _evaluatedHashTextbox.Enabled = false;
            ToggleCheckboxControl();
            ToggleEnabledSafe(_evaluatedHashTextbox);
            ToggleButtons();

            using (var fbd = new FolderBrowserDialog())
            {
                if (System.Windows.Forms.MessageBox.Show("Please select folder to search the files.", "Select folder", MessageBoxButtons.OK) == DialogResult.Cancel)
                {
                    _currentOperation = Operation.NONE;
                    return;
                }

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    if (fbd.SelectedPath.Length == 0)
                    {
                        return;
                    }

                    folders.Add(fbd.SelectedPath);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }


            var hasherType = GetSelectedHasherType();
            if ( hasherType == HasherType.Invalid)
            {
                MessageBox.Show("You must select which method of checkum calculation you want!", "Find files error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Task.Run(() =>
            {
                // to ensure that no files are entered
                _filePaths.Clear();
                IFinder finder = Creator.Instance.GetFinder(hasherType);
                finder.RegisterEventHandler(new EventHandler<IncreasedPercentage>(OnProgressChanged));

                var findResult = finder.Find(folders, _evaluatedHashTextbox.Text);

                if (findResult.findStatus == FindStatus.FilesFound)
                {
                    _filePaths.AddRange(findResult.files);
                    foreach (var file in findResult.files)
                    {
                        _selectedFilesCheckbox.Items.Add(file);
                    }
                    MessageBox.Show("Done!", "Status information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (findResult.findStatus == FindStatus.FilesNotFound)
                {
                    MessageBox.Show("Found no files that combine matching checksum", "No files found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _currentOperation = Operation.NONE;
                ToggleEnabledSafe(_evaluatedHashTextbox);
                ToggleCheckboxControl();
                ToggleButtons();
            });
        }
        private void OnCalculate(object sender, EventArgs e)
        {
            if (_currentOperation != Operation.NONE)
            {
                MessageBox.Show("Current operation in progress, please wait until operation is over!", "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _currentOperation = Operation.NONE;
                return;
            }

            var hasherType = GetSelectedHasherType();
            if (_selectedFilesCheckbox.CheckedItems.Count == 0)
            {
                _currentOperation = Operation.NONE;

                MessageBox.Show("You must select at least one file!", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ToggleButtons();
            ToggleCheckboxControl();
            _currentOperation = Operation.HASHING;
            _previosOperation = Operation.HASHING;
            _evaluatedHashTextbox.Enabled = false;

            var files = new List<string>();

            var checkedItmes = _selectedFilesCheckbox.CheckedItems;
            foreach(var file in checkedItmes)
            {
                string path = _filePaths.Find(s =>
                {
                    return Path.GetFileName(s) == file.ToString();
                });

                if (path != null)
                {
                    files.Add(path);
                }
            }

            Task.Run(() =>
            {
                using (IHasher hasher = Creator.Instance.GetHasher(hasherType))
                {
                    hasher.RegisterEventHandler(new EventHandler<IncreasedPercentage>(OnProgressChanged));
                    var hashResult = hasher.GetHash(files);
                    if (_evaluatedHashTextbox.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            _evaluatedHashTextbox.Text = hashResult.Item1;
                        }));
                    }
                    else
                    {
                        _evaluatedHashTextbox.Text = hashResult.Item1;
                    }

                    string output = String.Format("Done!\nSize of hashed files:{0} MB", hashResult.Item2);
                    MessageBox.Show(output, "Calculation information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _currentOperation = Operation.NONE;
                ToggleCheckboxControl();
                ToggleEnabledSafe(_evaluatedHashTextbox);
                ToggleButtons();
            });
        }

        public void OnProgressChanged(object sender, IncreasedPercentage args)
        {
            if (args == null)
            {
                return;
            }
            if (this._progressBar.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    OnProgressChanged(null, args);
                }));
            }
            else
            {
                if (args.Percentage <= _progressBar.Maximum)
                {
                    _progressBar.Value = (int)args.Percentage;
                }

                if (_progressBar.Value == _progressBar.Maximum)
                {
                    _progressBar.Value = _progressBar.Minimum;
                }
            }
        }
        private void OnItemMoveUpInTheList(object sender, EventArgs e)
        {
            if (_currentOperation != Operation.NONE)
            {
                MessageBox.Show("Current operation in progress, please wait until operation is over!", "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_selectedFilesCheckbox.SelectedItems.Count == 1)
            {
                // get index of the selected one
                int itemIndex = _selectedFilesCheckbox.SelectedIndex;
                int newIndexOfPreviousItem = itemIndex - 1;

                if (newIndexOfPreviousItem >= 0)
                {
                    string tmpString = _selectedFilesCheckbox.Items[itemIndex].ToString();

                    bool itemCheckedStatePrevious = _selectedFilesCheckbox.GetItemChecked(itemIndex);
                    bool itemCheckedStateNext = _selectedFilesCheckbox.GetItemChecked(newIndexOfPreviousItem);

                    _selectedFilesCheckbox.Items[itemIndex] = _selectedFilesCheckbox.Items[newIndexOfPreviousItem];
                    _selectedFilesCheckbox.Items[newIndexOfPreviousItem] = tmpString;

                    _selectedFilesCheckbox.SetItemCheckState(newIndexOfPreviousItem, itemCheckedStatePrevious == true ? CheckState.Checked : CheckState.Unchecked);
                    _selectedFilesCheckbox.SetItemCheckState(itemIndex, itemCheckedStateNext == true ? CheckState.Checked : CheckState.Unchecked);

                    _selectedFilesCheckbox.SetSelected(newIndexOfPreviousItem, true);
                }
            }
        }
        private void OnItemMoveDownInTheList(object sender, EventArgs e)
        {
            int itemIndex = _selectedFilesCheckbox.SelectedIndex;
            int newIndexOfPreviousItem = itemIndex + 1;

            if (newIndexOfPreviousItem < _selectedFilesCheckbox.Items.Count)
            {
                string tmpString = _selectedFilesCheckbox.Items[itemIndex].ToString();

                _selectedFilesCheckbox.Items[itemIndex] = _selectedFilesCheckbox.Items[newIndexOfPreviousItem];
                _selectedFilesCheckbox.Items[newIndexOfPreviousItem] = tmpString;

                bool itemCheckedStatePrevious = _selectedFilesCheckbox.GetItemChecked(itemIndex);
                bool itemCheckedStateNext = _selectedFilesCheckbox.GetItemChecked(newIndexOfPreviousItem);

                _selectedFilesCheckbox.SetItemCheckState(newIndexOfPreviousItem, itemCheckedStatePrevious == true ? CheckState.Checked : CheckState.Unchecked);
                _selectedFilesCheckbox.SetItemCheckState(itemIndex, itemCheckedStateNext == true ? CheckState.Checked : CheckState.Unchecked);

                _selectedFilesCheckbox.SetSelected(newIndexOfPreviousItem, true);
            }
        }
        private void OnRemoveClicked(object sender, EventArgs e)
        {
            if (_currentOperation != Operation.NONE)
            {
                MessageBox.Show("Current operation in progress, please wait until operation is over!", "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedIndex = _selectedFilesCheckbox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            _selectedFilesCheckbox.Items.RemoveAt(selectedIndex);

            if ((_evaluatedHashTextbox.Text.Length > 0) 
                && (_selectedFilesCheckbox.Items.Count > 0)
                && (_previosOperation != Operation.FINDING))
            {
                 DialogResult dialogResult = MessageBox.Show("Do you want to recalculate hash with selected files?", "Recalculate hash", MessageBoxButtons.YesNo);
                 if (dialogResult == DialogResult.Yes)
                 {
                     OnCalculate(null, EventArgs.Empty);
                 }
                 else if (dialogResult == DialogResult.No)
                 {
                     _evaluatedHashTextbox.Text = "";
                 }
            }
            else if (_evaluatedHashTextbox.Text.Length > 0)
            {
                _evaluatedHashTextbox.Text = "";
            }
        }
        private void OnAddFileClicked(object sender, EventArgs e)
        {
            if (_currentOperation != Operation.NONE)
            {
                MessageBox.Show("Current operation in progress, please wait until operation is over!", "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    InsertItemsIntoCheckboxList();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void InsertItemsIntoCheckboxList()
        {
            foreach(var item in _fileDialog.SafeFileNames)
            {
                InsertItemInList(item, _selectedFilesCheckbox.Items);
            }

            foreach(var item in _fileDialog.FileNames)
            {
                _filePaths.Add(item);
            }

        }
        private void InsertItemInList(string newObjectName, CheckedListBox.ObjectCollection items)
        {
            bool isInList = false;
            foreach (var item in items)
            {
                if (newObjectName == item.ToString())
                {
                    isInList = true;
                    break;
                }
            }

            if (!isInList)
            {
                items.Add(newObjectName);
            }
        }

        private HasherType GetSelectedHasherType()
        {
            if (_shaOneHAlgo.Checked)
            {
                return HasherType.SHA1;
            }
            else if (_sha256HAlgo.Checked)
            {
                return HasherType.SHA256;
            }
            else if (_md5HAlgo.Checked)
            {
                return HasherType.MD5;
            }
            else if (_crc32HAlgo.Checked)
            {
                return HasherType.CRC32;
            }
            else if (_crc64HAlgo.Checked)
            {
                return HasherType.CRC64;
            }

            return HasherType.Invalid;
        }

        void ToggleEnabledSafe(System.Windows.Forms.TextBox textbox)
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new Action(() =>
                {
                    textbox.Enabled = !textbox.Enabled;
                }));
            }
            else
            {
                textbox.Enabled = !textbox.Enabled;
            }
        }

        void ToggleEnabledSafe(System.Windows.Forms.ButtonBase button)
        {
            if (button.InvokeRequired)
            {
                button.Invoke(new Action(() =>
                {
                    button.Enabled = !button.Enabled;
                }));
            }
            else
            {
                button.Enabled = !button.Enabled;
            }
        }

        private void ToggleButtons()
        {
            ToggleEnabledSafe(_addFileButton);
            ToggleEnabledSafe(_removeFileButton);

            ToggleEnabledSafe(_moveUpButton);
            ToggleEnabledSafe(_moveDownButton);
            
            ToggleEnabledSafe(_calculateHashButton);
            ToggleEnabledSafe(_findFilesButton);
        }
        private void ToggleCheckboxControl()
        {
            ToggleEnabledSafe(_shaOneHAlgo);
            ToggleEnabledSafe(_sha256HAlgo);
            ToggleEnabledSafe(_crc32HAlgo);
            ToggleEnabledSafe(_crc64HAlgo);
            ToggleEnabledSafe(_md5HAlgo);
        }
        enum Operation
        {
            NONE,
            HASHING,
            FINDING
        }

        private Operation         _currentOperation;
        private Operation         _previosOperation;
        private OpenFileDialog    _fileDialog;
        private List<string>      _filePaths;
        private Size              _oldSize;
    }
}
