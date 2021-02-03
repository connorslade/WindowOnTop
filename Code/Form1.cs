using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OnTop
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd,
            int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        public Form1()
        {
            InitializeComponent();
            LoadProcesses();
        }
        private void LoadProcesses()
        {
            listBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            this.listBox1.DisplayMember = "MainWindowTitle";
            this.listBox1.ValueMember = "MainWindowHandle";

            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Length >= 1)
                {
                    listBox1.Items.Add(process);
                }
            }
        }
        private void btnOntopSet_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                Process process = this.listBox1.SelectedItem as Process;
                SetWindowPos(process.MainWindowHandle,
                    HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
        }

        private void btnRefresh_click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void btnOntopSetDefault_click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                Process process = this.listBox1.SelectedItem as Process;
                SetWindowPos(process.MainWindowHandle,
                    HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
        }
        private void allOff_click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Length >= 1)
                {
                    SetWindowPos(process.MainWindowHandle,
                    HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Basicprogrammer10/WindowOnTop");
        }
    }
}
