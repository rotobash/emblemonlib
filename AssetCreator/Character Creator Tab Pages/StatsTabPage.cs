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

using EmblemonLib.Combat;
using EmblemonLib.Data;

namespace AssetCreator
{
    public struct CharacterAttributes
    {
        public string name;

        public int level;
        public int health;
        public int magic;
        public int stamina;
        public int strength;
        public int defense;
        public int power;
        public int fortitude;
        public int speed;
    }

    public partial class CharacterCreator : Form
    {
        CharacterAttributes data;

        void LoadStatsTab()
        {
            characterTypeComboBox.SelectedIndex = 2;
            data = new CharacterAttributes();
            data.health = 1;
            data.magic = 1;
            data.stamina = 1;
            data.strength = 1;
            data.defense = 1;
            data.power = 1;
            data.fortitude = 1;
            data.speed = 1;
            data.level = 1;
        }

        private void characterTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstantiateCharacter(characterTypeComboBox.GetItemText(characterTypeComboBox.SelectedItem));
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            data.name = nameTextBox.Text;
        }

        private void charHealthNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.health = (int)charHealthNumeric.Value;
        }

        private void charMagicNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.magic = (int)charMagicNumeric.Value;
        }

        private void charStaminaNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.stamina = (int)charStaminaNumeric.Value;
        }

        private void charStrengthNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.strength = (int)charStrengthNumeric.Value;
        }

        private void charDefenseNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.defense = (int)charDefenseNumeric.Value;
        }

        private void charPowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.power = (int)charPowerNumeric.Value;
        }

        private void charFortitudeNumeric_ValueChanged(object sender, EventArgs e)
        {
            data.fortitude = (int)charFortitudeNumeric.Value;
        }

        private void charLevelNum_ValueChanged(object sender, EventArgs e)
        {
            data.level = (int)charLevelNum.Value;
        }

        private void charSpeedNum_ValueChanged(object sender, EventArgs e)
        {
            data.speed = (int)charSpeedNum.Value;
        }

        void SerializeAttributes(XmlWriter wr)
        {
            wr.WriteStartElement("CharacterStats");
            wr.WriteElementString("Health", data.health.ToString());
            wr.WriteElementString("Magic", data.magic.ToString());
            wr.WriteElementString("Stamina", data.stamina.ToString());
            wr.WriteElementString("Level", data.level.ToString());
            wr.WriteElementString("Strength", data.strength.ToString());
            wr.WriteElementString("Defense", data.defense.ToString());
            wr.WriteElementString("Power", data.power.ToString());
            wr.WriteElementString("Fortitude", data.fortitude.ToString());
            wr.WriteElementString("Speed", data.speed.ToString());
            wr.WriteEndElement();
        }

        void LoadStats(XmlNode body)
        {
            charHealthNumeric.Value = int.Parse(body["Health"].InnerText);
            charMagicNumeric.Value = int.Parse(body["Magic"].InnerText);
            charStaminaNumeric.Value = int.Parse(body["Stamina"].InnerText);
            charLevelNum.Value = int.Parse(body["Level"].InnerText);
            charStrengthNumeric.Value = int.Parse(body["Strength"].InnerText);
            charDefenseNumeric.Value = int.Parse(body["Defense"].InnerText);
            charPowerNumeric.Value = int.Parse(body["Power"].InnerText);
            charFortitudeNumeric.Value = int.Parse(body["Fortitude"].InnerText);
            charSpeedNum.Value = int.Parse(body["Speed"].InnerText);
        }
    }
}
