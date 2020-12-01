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
    public partial class CreatorSelect : Form
    {
        public CreatorSelect()
        {
            InitializeComponent();
        }

        private void moveListBtn_Click(object sender, EventArgs e)
        {
            Hide();
            new MoveListCreator().ShowDialog();
            Show();
        }

        private void charCreatorBtn_Click(object sender, EventArgs e)
        {
            Hide();
            new CharacterCreator().ShowDialog();
            Show();
        }

        private void itemCreatorBtn_Click(object sender, EventArgs e)
        {

        }

        private void questCreatorBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
