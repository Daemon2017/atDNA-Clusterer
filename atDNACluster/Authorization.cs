using System;
using System.Windows.Forms;

namespace atDNACluster
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        public string KitNumber;
        public string PassWord;

        private void button1_Click(object sender, EventArgs e)
        {
            KitNumber = textBox1.Text;
            PassWord = textBox2.Text;

            Close();
        }
    }
}
