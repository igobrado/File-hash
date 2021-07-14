using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace FileHash
{
    enum CurrentState
    {
        FindFilesFromChecksum,
        CalculateChecksumFromFiles,
        MainScreen
    }

    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();

            _currentState = CurrentState.MainScreen;
            FildFilesFromChecksum.Click += FindFilesFromChecksum_Click;
            CalculateChecksumFromFiles.Click += CalculateChecksumFromFiles_Click;
            BackToMainScreen.Click += Back_Click;

            BackToMainScreen.Enabled = false;
            BackToMainScreen.Visible = false;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            ToggleBackToMainScreenButton();
            ToggleMainScreenComponentsVisibility();

            if (_currentState == CurrentState.FindFilesFromChecksum)
            {
                FindChecksumFromGivenFilesOrFolderControl.ResetToInitialState();
            }
            else if (_currentState == CurrentState.CalculateChecksumFromFiles)
            {
                CalculateChecksumFromFilesControl.ResetToInitialState();
            }

            _currentState = CurrentState.MainScreen;
        }

        private void CalculateChecksumFromFiles_Click(object sender, EventArgs e)
        {
            _currentState = CurrentState.CalculateChecksumFromFiles;

            ToggleBackToMainScreenButton();
            ToggleMainScreenComponentsVisibility();

            CalculateChecksumFromFilesControl.Visible = true;
            CalculateChecksumFromFilesControl.Enabled = true;
        }

        private void FindFilesFromChecksum_Click(object sender, EventArgs e)
        {
            _currentState = CurrentState.FindFilesFromChecksum;

            ToggleBackToMainScreenButton();
            ToggleMainScreenComponentsVisibility();

            FindChecksumFromGivenFilesOrFolderControl.Visible = true;
            FindChecksumFromGivenFilesOrFolderControl.Enabled = true;
        }
        
        private void ToggleBackToMainScreenButton()
        {
            BackToMainScreen.Enabled = !BackToMainScreen.Enabled;
            BackToMainScreen.Visible = !BackToMainScreen.Visible;
        }

        private void ToggleMainScreenComponentsVisibility()
        {
            FildFilesFromChecksum.Visible = !FildFilesFromChecksum.Visible;
            FildFilesFromChecksum.Enabled = !FildFilesFromChecksum.Enabled;
            CalculateChecksumFromFiles.Visible = !CalculateChecksumFromFiles.Visible;
            CalculateChecksumFromFiles.Enabled = !CalculateChecksumFromFiles.Enabled;
        }

        private CurrentState _currentState;
    }
}
