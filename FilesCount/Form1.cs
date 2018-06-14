using FastestEnumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesCount
{
    public partial class Form1 : Form
    {
        private List<FileData> files;
        private BindingList<FilesInfo> Bfiles;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPath.Text = @"E:\MP3";

            

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async void btnStart_ClickAsync(object sender, EventArgs e)
        {
            List<FilesInfo> listfi = new List<FilesInfo>();
            await Task.Run(() =>
            {
                files = FastEnumerator.EnumerateFiles(txtPath.Text, "*", System.IO.SearchOption.TopDirectoryOnly, "OnlyDirectory").ToList();
                



                foreach (var item in files)
                {
                    FilesInfo f = new FilesInfo();
                    f.Type = item.Attributes.ToString();
                    f.Name = item.Name;
                    
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(item.Path);
                    f.FilesCount = GetFilesCount(dirInfo);
                    f.DirectoryCount = GetDirectoryCount(dirInfo);

                    listfi.Add(f);
                }
            });

            Bfiles = new BindingList<FilesInfo>(listfi);
            dataGridView1.DataSource = Bfiles;
        }

        public static int GetFilesCount(System.IO.DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            totalFile += dirInfo.GetFiles().Length;
            foreach (System.IO.DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetFilesCount(subdir);
            }
            return totalFile;
        }
        public static int GetDirectoryCount(System.IO.DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            totalFile += dirInfo.GetDirectories().Length;
            foreach (System.IO.DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetDirectoryCount(subdir);
            }
            return totalFile;
        }
    }
}
