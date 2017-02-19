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
            doc.Load(loadedCharacterPath);
            XmlNode type = doc["Character"].SelectSingleNode("Type");
            InstantiateCharacter(type.InnerText);
            switch (charType)
            {
                case CharacterType.Hybrid:
                    hybridChar = CharacterFactory.GetInstance.BuildHybrid(loadedCharacterPath, game.Content);
                    break;
                case CharacterType.Battle:
                    battleChar = CharacterFactory.GetInstance.BuildBattle(loadedCharacterPath, game.Content);
                    break;
                case CharacterType.Overworld:
                    overChar = CharacterFactory.GetInstance.BuildOverworld(loadedCharacterPath, game.Content);
                    break;
            }
        }

        private void InstantiateCharacter(string type)
        {
            switch (type)
            {
                case "Hybrid":
                    {
                        charType = CharacterType.Hybrid;
                        //Enable everything
                        ToggleOverworldForms(true);
                        ToggleBattleForms(true);
                    }
                    break;

                case "Battle":
                    {
                        charType = CharacterType.Battle;
                        //Enable things relevant to battle characters
                        ToggleBattleForms(true);
                        ToggleOverworldForms(false);
                    }
                    break;

                case "Overworld":
                    {
                        charType = CharacterType.Overworld;
                        //Enable things relevant to overworld characters
                        ToggleOverworldForms(true);
                        ToggleBattleForms(false);
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

        }
    }
}
