using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Content;

using EmblemonLib.Data;
using EmblemonLib.Utilities;

namespace AssetCreator
{
    public partial class CharacterCreator : Form
    {
        string moveListPath;
        string[] loadedMoveStrArr;
        int moveIndex;
        List<string> knownMoveStrList;

        void LoadMovesTab()
        {
            knownMoveStrList = new List<string>();
            moveIndex = 0;
        }


        private void loadMoveLstBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Move List Files (*.txt;)|*.txt;";
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            moveListPath = dlg.FileName;
            loadedMoveStrArr = System.IO.File.ReadAllText(moveListPath).Split('\n');
            loadedMovesList.Items.Clear();
            foreach (string move in loadedMoveStrArr)
            {
                loadedMovesList.Items.Add(move);
            }
        }

        private void loadedMovesList_DoubleClick(object sender, EventArgs e)
        {
            if (loadedMovesList.Items.Count == 0 || knownMovesList.Items.Contains(loadedMovesList.SelectedItem))
                return;
            knownMovesList.Items.Add(loadedMovesList.SelectedItem);
        }

        private void knownMovesList_DoubleClick(object sender, EventArgs e)
        {
            if (knownMovesList.Items.Count > 0)
                knownMovesList.Items.Remove(knownMovesList.SelectedItem);
        }

        private void addCustomMoveBtn_Click(object sender, EventArgs e)
        {
            string res = ShowInputDialog("Add A Custom Move", "Enter the name of the move you want to add:");
            if (res == "")
                return;
            knownMovesList.Items.Add(res);
        }
    }
}
