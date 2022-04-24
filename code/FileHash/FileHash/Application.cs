using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using FileHashBackend;

namespace FileHash
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
            Load += new EventHandler(InitializeModule);
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
        }
        
        private void OnCalculate(object sender, EventArgs e)
        {
            var hasherType = GetSelectedHasherType();

            if (_selectedFilesCheckbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one file!");
                return;
            }

            var files = new List<string>();

            foreach (var item in _filePaths)
            {
                int index = _selectedFilesCheckbox.FindString(Path.GetFileName(item));
                if ((index != ListBox.NoMatches) && (_selectedFilesCheckbox.GetItemChecked(index) == true))
                {
                    files.Add(item);
                }
            }

            using (Hasher hasher = new Hasher(hasherType))
            {
                hasher.HashProgress += new EventHandler<IncreasedPercentage>(OnProgressChanged);
                _evaluatedHashTextbox.Text = hasher.GetHash(files).Item1.ToString();

                MessageBox.Show("Done!");
            }
        }

        public void OnProgressChanged(object sender, IncreasedPercentage args)
        {
            if (args == null)
            {
                return;
            }

            if (args.Percentage <= _progressBar.Maximum)
            {
                _progressBar.Value = (int)args.Percentage;
            }
        }
        private void OnItemMoveUpInTheList(object sender, EventArgs e)
        {
            if (_selectedFilesCheckbox.SelectedItems.Count == 1)
            {
                // get index of the selected one
                int itemIndex = _selectedFilesCheckbox.SelectedIndex;
                int newIndexOfPreviousItem = itemIndex - 1;

                if (newIndexOfPreviousItem >= 0)
                {
                    string tmpString = _selectedFilesCheckbox.Items[itemIndex].ToString();

                    _selectedFilesCheckbox.Items[itemIndex] = _selectedFilesCheckbox.Items[newIndexOfPreviousItem];
                    _selectedFilesCheckbox.Items[newIndexOfPreviousItem] = tmpString;
                    
                    // in case that it was checked
                    if (_selectedFilesCheckbox.GetItemChecked(itemIndex))
                    {
                        _selectedFilesCheckbox.SetItemCheckState(itemIndex, CheckState.Unchecked);
                        _selectedFilesCheckbox.SetItemCheckState(newIndexOfPreviousItem, CheckState.Checked);
                    }
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

                // in case that it was checked
                if (_selectedFilesCheckbox.GetItemChecked(itemIndex))
                {
                    _selectedFilesCheckbox.SetItemCheckState(itemIndex, CheckState.Unchecked);
                    _selectedFilesCheckbox.SetItemCheckState(newIndexOfPreviousItem, CheckState.Checked);
                }
            }
        }
        private void OnRemoveClicked(object sender, EventArgs e)
        {
            int selectedIndex = _selectedFilesCheckbox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            _selectedFilesCheckbox.Items.RemoveAt(selectedIndex);

            if (_evaluatedHashTextbox.Text.Length > 0)
            {
                 DialogResult dialogResult = MessageBox.Show("Do you want to recalculate hash with selected files?", "Recalculate hash", MessageBoxButtons.YesNo);
                 if (dialogResult == DialogResult.Yes)
                 {
                     OnCalculate(null, EventArgs.Empty);
                 }else if (dialogResult == DialogResult.No)
                 {
                     _evaluatedHashTextbox.Text = "";
                 }
            }
        }
        private void OnAddFileClicked(object sender, EventArgs e)
        {
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

        private void InsertItemInList(string newObjectName, List<string> items)
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
            else if(_crc32HAlgo.Checked)
            {
                return HasherType.CRC32;
            }

            return HasherType.Invalid;
        }

        private OpenFileDialog _fileDialog;
        private List<string>   _filePaths;
    }
}
