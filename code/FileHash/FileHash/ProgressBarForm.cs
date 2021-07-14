using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FileHash
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm()
        {
            InitializeComponent();

            _shouldStop = false;
            _worker = new Thread(Work);
            _worker.Start();
        }

        public void Stop()
        {
            _shouldStop = true;
            _worker.Join();
        }

        public new void Close()
        {
            if (ProgressBar.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate () {
                    base.Close();
                });
            }
            else
            {
                base.Close();
            }
    }

        public void Work()
        {
            while (true)
            {
                if (ProgressBar.Value == 100)
                {
                    break;
                }

                int updateValue = 1;
                if (_shouldStop == true)
                {
                    updateValue = ProgressBar.Maximum - ProgressBar.Value;
                }
                
                if ((_shouldStop == false) && (ProgressBar.Value == ProgressBar.Value -1))
                {
                    continue;
                }

                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate () {
                        OnProgress(updateValue);
                    });
                }
                else
                {
                    OnProgress(updateValue);
                }

                Thread.Sleep(1000);
            }
        }

        protected void OnProgress(int value)
        {
            if (ProgressBar.Value + value <= 100)
            {
                ProgressBar.Value += value;
                label1.Text = ProgressBar.Value.ToString() + "%";
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool _shouldStop;
        Thread _worker;
    }
}
