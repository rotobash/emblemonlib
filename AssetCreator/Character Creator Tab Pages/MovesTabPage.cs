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
using System.IO;

namespace AssetCreator
{
    public partial class CharacterCreator : Form
    {
        List<string> knownMoveStrList;

        void LoadMovesTab()
        {
            knownMoveStrList = new List<string>();
        }


        private void loadMoveLstBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;
            
            loadedMovesList.Items.Clear();

            foreach (string move in Directory.EnumerateFiles(dlg.SelectedPath))
            {
                if (Path.GetExtension(move) == ".xml")
                    loadedMovesList.Items.Add(Path.GetFileNameWithoutExtension(move));
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

        void SerializeMoveList(XmlWriter wr)
        {
            wr.WriteStartElement("MoveList");
            foreach (string move in knownMovesList.Items)
                wr.WriteString(move + " ");
            wr.WriteEndElement();
        }

        void LoadMoveList(XmlNode body)
        {
            string[] moveList = body.InnerText.Split(' ');
            foreach(string move in moveList)
            {
                knownMovesList.Items.Add(move);
            }
        }
    }
}
