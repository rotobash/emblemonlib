using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetCreator
{
    public partial class InputDialog : Form
    {
        public string Result = "";

        public InputDialog(string title, string message)
        {
            InitializeComponent();
            Text = title;
            label1.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Result = textBox1.Text;
        }
    }
}
