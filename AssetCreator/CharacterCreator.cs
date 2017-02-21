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
    public enum CharacterType { Hybrid, Battle, Overworld }
    /// <summary>
    /// This form is mostly a tab sheet with each sheet having different controls in it.
    /// For that reason I have split this class up by tab page (you can do that with partial classes)
    /// </summary>
    public partial class CharacterCreator : Form
    {
        string loadedCharacterPath;
        Game1 game;

        BattleCharacter battleChar;
        HybridCharacter hybridChar;
        OverworldCharacter overChar;

        CharacterType charType;

        public CharacterCreator()
        {
            InitializeComponent();
            LoadStatsTab();
            LoadAttrCurveTab();
            LoadAnimationTab();
        }

        public string ShowInputDialog(string title, string message)
        {
            InputDialog testDialog = new InputDialog(title, message);
            string res = "";

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                 res = testDialog.Result;
            }
            testDialog.Dispose();
            return res;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            XmlDocument doc = new XmlDocument();
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            loadedCharacterPath = dlg.FileName;
            Directory.SetCurrentDirectory(Path.GetDirectoryName(dlg.FileName));
            try
            {
                doc.Load(loadedCharacterPath);
            }
            catch (XmlException ex)
            {
                MessageBox.Show("Could not load character, file may be corrupt.");
                return;
            }
            XmlNode body = doc["Character"];
            InstantiateCharacter(body["Type"].InnerText);
            nameTextBox.Text = body["Name"].InnerText;

            if(charType == CharacterType.Battle || charType == CharacterType.Hybrid)
            {
                LoadAttrCurve(body["Curves"]);
                LoadMoveList(body["MoveList"]);
                LoadStats(body["CharacterStats"]);
            }

            LoadAnims(body["Animations"]);
            
        }

        private void InstantiateCharacter(string type)
        {
            switch (type)
            {
                case "Hybrid":
                    {
                        charType = CharacterType.Hybrid;
                        ToggleOverworldForms(true);
                        ToggleBattleForms(true);
                    }
                    break;

                case "Battle":
                    {
                        charType = CharacterType.Battle;
                        ToggleBattleForms(true);
                        ToggleOverworldForms(false);
                    }
                    break;

                case "Overworld":
                    {
                        charType = CharacterType.Overworld;
                        ToggleBattleForms(false);
                        ToggleOverworldForms(true);
                    }
                    break;
            }
        }

        private void ToggleBattleForms(bool enabled)
        {
            attrCurvesTab.Enabled = enabled;
            movesTab.Enabled = enabled;
            charHealthNumeric.Enabled = enabled;
            charMagicNumeric.Enabled = enabled;
            charStaminaNumeric.Enabled = enabled;
            charStrengthNumeric.Enabled = enabled;
            charDefenseNumeric.Enabled = enabled;
            charPowerNumeric.Enabled = enabled;
            charFortitudeNumeric.Enabled = enabled;
            charSpeedNum.Enabled = enabled;
            charLevelNum.Enabled = enabled;

            battleSpriteSheetPicBox.Enabled = enabled;
            currBttlAnimComboBox.Enabled = enabled;
            addBattleSheetBtn.Enabled = enabled;
            bttlDelayNum.Enabled = enabled;
            bttlFrmWdthNum.Enabled = enabled;
            bttlFrmHgtNum.Enabled = enabled;
            bttlOTRChkBox.Enabled = enabled;
        }

        private void ToggleOverworldForms(bool enabled)
        {
            overworldSpriteSheetPicBox.Enabled = enabled;
            currOvrwrldAnimComboBox.Enabled = enabled;
            addOvrwrldSheetBtn.Enabled = enabled;
            ovrwrldDelayNum.Enabled = enabled;
            ovrwrldFrmWdthNum.Enabled = enabled;
            ovrwrldFrmHgtNum.Enabled = enabled;
            checkBox1.Enabled = enabled;
        }

        private void generateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML Files(*.xml;)|*.xml;";
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;
            
            using (XmlWriter wr = XmlWriter.Create(dlg.FileName))
            {
                wr.WriteStartDocument();
                wr.WriteStartElement("Character");
                wr.WriteElementString("Type", charType.ToString());
                wr.WriteElementString("Name", nameTextBox.Text);

                System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(dlg.FileName));

                if (charType == CharacterType.Battle || charType == CharacterType.Hybrid)
                {
                    SerializeLevelCurves(wr);
                    SerializeAttributes(wr);
                    SerializeMoveList(wr);
                }
                SerializeAnimations(wr);

                wr.WriteEndElement();
                wr.WriteEndDocument();
                wr.Flush();
            }
        }
    }
}
