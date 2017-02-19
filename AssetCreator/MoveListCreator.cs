using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using EmblemonLib.Combat;
using System.Xml;

namespace AssetCreator
{
    public struct MoveData
    {
        public Target target;
        public Method method;
        public StatusInfliction infliction;
        public Effect eff;

        public string name;
        public int power;
        public int cost;
        public float inflictChance;
    }

    public partial class MoveListCreator : Form
    {
        Dictionary<string, MoveData> moveData;
        Dictionary<string, AnimationData> animData;
        Dictionary<string, Image> moveAnimSheet;

        string key;

        public MoveListCreator()
        {
            moveData = new Dictionary<string, MoveData>();
            animData = new Dictionary<string, AnimationData>();
            moveAnimSheet = new Dictionary<string, Image>();

            InitializeComponent();
        }
        /// <summary>
        /// Given a PictureBox, an image and the frame size, 
        /// </summary>
        /// <param name="target">Target PictureBox to draw to.</param>
        /// <param name="originalImg">The spritesheet to draw over</param>
        /// <param name="frameWidth">The width of one frame</param>
        /// <param name="frameHeight">The height of one frame</param>
        void DrawFramesOnImg(PictureBox target, Image originalImg, int frameWidth, int frameHeight)
        {
            using (Graphics g = Graphics.FromImage(target.Image))
            {
                g.DrawImage(originalImg, 0, 0);
                for (int y = 0; y < target.Image.Height; y += frameHeight)
                {
                    for (int x = 0; x < target.Image.Width; x += frameWidth)
                    {
                        g.DrawRectangle(Pens.Red, new Rectangle(x, y, frameWidth, frameHeight));
                    }
                }
            }
            target.Invalidate();
        }

        void SerializeMove(string path, string moveName)
        {
            XmlDocument wr = new XmlDocument();
            using (FileStream f = File.Create(path + moveName + ".xml"))
            {
                f.Flush();
            }
            wr.Load(path + moveName + ".xml");
            XmlNode body = wr.CreateElement("Move");

        }

        private void loadMoveListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generateMoveXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;


        }

        private void generateMoveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;
            
            StreamWriter wr = File.CreateText(dlg.FileName);
            foreach(string move in moveListBox.Items)
                wr.WriteLine(move);
            wr.Flush();
            wr.Dispose();
        }

        private void generateAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateMoveListToolStripMenuItem_Click(sender, e);
            generateMoveXmlToolStripMenuItem_Click(sender, e);

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];
            data.name = nameTextBox.Text;
            moveData[key] = data;
        }

        private void targetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];

            switch (effectComboBox.GetItemText(effectComboBox.SelectedItem))
            {
                case "Self":
                    data.target = Target.Self;
                    break;
                case "Party Member":
                    data.target = Target.PartyMember;
                    break;
                case "Enemy":
                    data.target = Target.Enemy;
                    break;
                case "Enemy Party":
                    data.target = Target.EnemyParty;
                    break;
                case "Player Party":
                    data.target = Target.PlayerParty;
                    break;
                case "Everyone":
                    data.target = Target.Everyone;
                    break;
                case "Any":
                    data.target = Target.Any;
                    break;
                default:
                    data.target = Target.None;
                    break;
            }

            moveData[key] = data;
        }

        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];

            switch (effectComboBox.GetItemText(effectComboBox.SelectedItem))
            {
                case "Physical":
                    data.method = Method.Physical;
                    break;
                case "Spell":
                    data.method = Method.Spell;
                    break;
                default:
                    data.method = Method.Physical;
                    break;
            }

            moveData[key] = data;
        }

        private void inflictionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];

            switch (effectComboBox.GetItemText(effectComboBox.SelectedItem))
            {
                case "Poison":
                    data.infliction = StatusInfliction.Poison;
                    break;
                case "Stun":
                    data.infliction = StatusInfliction.Stun;
                    break;
                case "Confusion":
                    data.infliction = StatusInfliction.Confusion;
                    break;
                case "Burn":
                    data.infliction = StatusInfliction.Burn;
                    break;
                case "Freeze":
                    data.infliction = StatusInfliction.Freeze;
                    break;
                default:
                    data.infliction = StatusInfliction.None;
                    break;
            }

            moveData[key] = data;
        }

        private void effectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];

            switch(effectComboBox.GetItemText(effectComboBox.SelectedItem))
            {
                case "Damaging":
                    data.eff = Effect.Damaging;
                    break;
                case "Curative":
                    data.eff = Effect.Curative;
                    break;
                default:
                    data.eff = Effect.None;
                    break;
            }

            moveData[key] = data;
        }

        private void movePowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];
            data.power = (int)movePowerNumeric.Value;
            moveData[key] = data;
        }

        private void moveCostNumeric_ValueChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];
            data.cost = (int)moveCostNumeric.Value;
            moveData[key] = data;
        }

        private void inflictionChanceNumeric_ValueChanged(object sender, EventArgs e)
        {
            MoveData data = moveData[key];
            data.inflictChance = (float)inflictionChanceNumeric.Value;
            moveData[key] = data;
        }

        private void addMoveAnimBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            if (moveAnimPicBox.Image != null &&
                MessageBox.Show("This will get rid of the current spritesheet for this move. Are you sure you want to add this spritesheet?",
                "Caution!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            AnimationData data = animData[key];
            data.path = dlg.FileName;
            animData[key] = data;

            Image img = Image.FromStream(dlg.OpenFile());
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            moveAnimSheet[key] = img;
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, img, data.frameWidth, data.framHeight);
        }

        private void moveAnimDelayNum_ValueChanged(object sender, EventArgs e)
        {
            AnimationData data = animData[key];
            data.delay = (int)moveAnimDelayNum.Value;
            animData[key] = data;
        }

        private void moveAnimFrmWdthNum_ValueChanged(object sender, EventArgs e)
        {
            AnimationData data = animData[key];
            data.frameWidth = (int)moveAnimFrmWdthNum.Value;
            animData[key] = data;
            Bitmap img = (Bitmap)moveAnimSheet[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            moveAnimSheet[key] = img;
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, img, data.frameWidth, data.framHeight);
        }

        private void moveAnimFrmHgtNum_ValueChanged(object sender, EventArgs e)
        {
            AnimationData data = animData[key];
            data.framHeight = (int)moveAnimFrmHgtNum.Value;
            animData[key] = data;
            Bitmap img = (Bitmap)moveAnimSheet[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            moveAnimSheet[key] = img;
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, img, data.frameWidth, data.framHeight);
        }

        private void moveAnimOTRChkBox_CheckedChanged(object sender, EventArgs e)
        {
            AnimationData data = animData[key];
            data.oneTimeRun = moveAnimOTRChkBox.Checked;
            animData[key] = data;
        }

        private void addMoveBtn_Click(object sender, EventArgs e)
        {
            string tempKey = ShowInputDialog("Add A New Move", "Enter the name of the new move to add:");
            if (tempKey == "")
                return;

            MoveData tempMove = new MoveData();
            AnimationData tempAnim = new AnimationData();

            tempAnim.frameWidth = 4;
            tempAnim.framHeight = 4;

            moveData.Add(tempKey, tempMove);
            animData.Add(tempKey, tempAnim);
            moveAnimSheet.Add(tempKey, null);
            moveListBox.Items.Add(tempKey);
        }

        private void deleteMoveBtn_Click(object sender, EventArgs e)
        {
            moveData.Remove(key);
            animData.Remove(key);
            moveAnimSheet.Remove(key);

            moveListBox.Items.Remove(moveListBox.SelectedItem);
        }

        private void moveListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            key = moveListBox.GetItemText(moveListBox.SelectedItem);
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
    }
}
