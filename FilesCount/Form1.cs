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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<FileData> files = FastEnumerator.EnumerateFiles(@"F:\Work\test", "*", System.IO.SearchOption.AllDirectories, "OnlyDirectory").ToList();
        }
    }
}
