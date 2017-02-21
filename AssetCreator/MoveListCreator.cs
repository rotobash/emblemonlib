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

        MoveData currMove;
        AnimationData currAnim;
        Image currImg;

        string key = "";

        public MoveListCreator()
        {
            moveData = new Dictionary<string, MoveData>();
            animData = new Dictionary<string, AnimationData>();
            moveAnimSheet = new Dictionary<string, Image>();

            currMove = new MoveData() { name = "" };
            currAnim = new AnimationData() { delay = 0.25f, frameWidth = 4, framHeight = 4 };
            currImg = null;

            moveData.Add("", currMove);
            animData.Add("", currAnim);
            moveAnimSheet.Add("", currImg);
            
            InitializeComponent();
            targetComboBox.SelectedIndex = 0;
            methodComboBox.SelectedIndex = 0;
            effectComboBox.SelectedIndex = 0;
            inflictionComboBox.SelectedIndex = 0;
            moveListBox.Items.Add("");
            moveListBox.SelectedIndex = 0;
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

        void SerializeMove(string path, MoveData move, AnimationData anim)
        {

            //we're just going to disallow empty move names
            if (move.name == "")
                return;

            using (XmlWriter wr = XmlWriter.Create(path + "\\" + move.name + ".xml"))
            {
                wr.WriteStartDocument();
                wr.WriteStartElement("Move");
                wr.WriteElementString("Name", move.name);
                wr.WriteElementString("Power", move.power.ToString());
                wr.WriteElementString("Cost", move.cost.ToString());
                wr.WriteElementString("Target", move.target.ToString());

                wr.WriteStartElement("StatusInfliction");
                wr.WriteAttributeString("inflictChance", move.inflictChance.ToString());
                wr.WriteString(move.infliction.ToString());
                wr.WriteEndElement();

                wr.WriteElementString("Method", move.method.ToString());
                wr.WriteElementString("Effect", move.eff.ToString());

                wr.WriteStartElement("Animation");
                string animPath = "";
                if (anim.path != null)
                {
                    string extension = anim.path.Split('.')[1];
                    animPath = "Move Spritesheets\\" + move.name + "." + extension;
                }
                wr.WriteElementString("Texture", animPath);

                wr.WriteElementString("FrameSize", anim.frameWidth.ToString() + " " + anim.framHeight.ToString());
                wr.WriteElementString("Delay", anim.delay.ToString());
                wr.WriteElementString("OTR", anim.oneTimeRun.ToString());
                wr.WriteEndElement();

                wr.WriteEndElement();
                wr.WriteEndDocument();
                wr.Flush();
            }
        }

        private void loadMoveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create the anim and move data first then add it in to the move list box
            //the events will be called to update the fields

            if (MessageBox.Show("This will delete all data currently open. Continue anyway?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            moveData = new Dictionary<string, MoveData>();
            animData = new Dictionary<string, AnimationData>();
            moveAnimSheet = new Dictionary<string, Image>();
            moveListBox.Items.Clear();

            foreach (string move in Directory.EnumerateFiles(dlg.SelectedPath))
            {
                if (move.Contains(".xml"))
                {
                    MoveData tempMove = new MoveData();
                    AnimationData tempAnim = new AnimationData();
                    Image tempImg = null;

                    XmlDocument doc = new XmlDocument();
                    doc.Load(move);
                    XmlNode body = doc["Move"];
                    tempMove.name = body["Name"].InnerText;
                    tempMove.power = int.Parse(body["Power"].InnerText);
                    tempMove.cost = int.Parse(body["Cost"].InnerText);
                    tempMove.inflictChance = float.Parse(body["StatusInfliction"].Attributes[0].InnerText);
                    tempMove.target = (Target)Enum.Parse(typeof(Target), body["Target"].InnerText);
                    tempMove.infliction = (StatusInfliction)Enum.Parse(typeof(StatusInfliction), body["StatusInfliction"].InnerText);
                    tempMove.method = (Method)Enum.Parse(typeof(Method), body["Method"].InnerText);
                    tempMove.eff = (Effect)Enum.Parse(typeof(Effect), body["Effect"].InnerText);
                    XmlNode animation = body["Animation"];
                    tempAnim.path = animation["Texture"].InnerText;
                    string[] frameSize = animation["FrameSize"].InnerText.Split(' ');
                    tempAnim.frameWidth = int.Parse(frameSize[0]);
                    tempAnim.framHeight = int.Parse(frameSize[1]);
                    tempAnim.delay = float.Parse(animation["Delay"].InnerText);
                    tempAnim.oneTimeRun = bool.Parse(animation["OTR"].InnerText);

                    moveData.Add(tempMove.name, tempMove);
                    animData.Add(tempMove.name, tempAnim);
                    if (tempAnim.path != "")
                        tempImg = Image.FromFile(tempAnim.path);

                    moveAnimSheet.Add(tempMove.name, tempImg);
                    moveListBox.Items.Add(tempMove.name);
                }
            }
        }

        private void generateAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            Directory.SetCurrentDirectory(dlg.SelectedPath);
            Directory.CreateDirectory("Move Spritesheets");

            foreach (string move in moveListBox.Items)
            {
                AnimationData tempAnim = animData[move];
                SerializeMove(Directory.GetCurrentDirectory(), moveData[move], tempAnim);
                if (tempAnim.path != null)
                {
                    string extension = tempAnim.path.Split('.')[1];
                    if(File.Exists("Move Spritesheets\\" + move + "." + extension))
                        File.Delete("Move Spritesheets\\" + move + "." + extension);

                    File.Copy(tempAnim.path, "Move Spritesheets\\" + move + "." + extension);
                }
            }

        }

        private void targetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currMove.target = (Target)targetComboBox.SelectedIndex;
        }

        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currMove.method = (Method)methodComboBox.SelectedIndex;            
        }

        private void inflictionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currMove.infliction = (StatusInfliction)inflictionComboBox.SelectedIndex;
        }

        private void effectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currMove.eff = (Effect)effectComboBox.SelectedIndex;
        }

        private void movePowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            currMove.power = (int)movePowerNumeric.Value;
        }

        private void moveCostNumeric_ValueChanged(object sender, EventArgs e)
        {
            currMove.cost = (int)moveCostNumeric.Value;
        }

        private void inflictionChanceNumeric_ValueChanged(object sender, EventArgs e)
        {
            currMove.inflictChance = (float)inflictionChanceNumeric.Value;
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
            
            currAnim.path = dlg.FileName;

            currImg = Image.FromStream(dlg.OpenFile());
            Bitmap tempBitmap = new Bitmap(currImg.Width, currImg.Height);
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, currImg, currAnim.frameWidth, currAnim.framHeight);
        }

        private void moveAnimDelayNum_ValueChanged(object sender, EventArgs e)
        {
            currAnim.delay = (int)moveAnimDelayNum.Value;
        }

        private void moveAnimFrmWdthNum_ValueChanged(object sender, EventArgs e)
        {
            currAnim.frameWidth = (int)moveAnimFrmWdthNum.Value;
            currImg = (Bitmap)moveAnimSheet[key] ?? new Bitmap(currAnim.frameWidth + 1, currAnim.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(currImg.Width, currImg.Height);
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, currImg, currAnim.frameWidth, currAnim.framHeight);
        }

        private void moveAnimFrmHgtNum_ValueChanged(object sender, EventArgs e)
        {
            currAnim.framHeight = (int)moveAnimFrmHgtNum.Value;
            currImg = (Bitmap)moveAnimSheet[key] ?? new Bitmap(currAnim.frameWidth + 1, currAnim.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(currImg.Width, currImg.Height);
            moveAnimPicBox.Image = tempBitmap;
            DrawFramesOnImg(moveAnimPicBox, currImg, currAnim.frameWidth, currAnim.framHeight);
        }

        private void moveAnimOTRChkBox_CheckedChanged(object sender, EventArgs e)
        {
            currAnim.oneTimeRun = moveAnimOTRChkBox.Checked;
        }

        private void addMoveBtn_Click(object sender, EventArgs e)
        {
            string tempKey = ShowInputDialog("Add A New Move", "Enter the name of the new move to add:");

            if (moveListBox.Items.Contains(tempKey))
                return;

            if(key == null)
                key = tempKey;

            MoveData tempMove = new MoveData() { name = tempKey };
            AnimationData tempAnim = new AnimationData() { delay = 0.25f, frameWidth = 4, framHeight = 4 };
            
            moveData.Add(tempKey, tempMove);
            animData.Add(tempKey, tempAnim);
            moveAnimSheet.Add(tempKey, null);
            moveListBox.Items.Add(tempKey);
        }

        private void deleteMoveBtn_Click(object sender, EventArgs e)
        {
            if(moveListBox.Items.Count <= 1)
            {
                MessageBox.Show("You cannot have an empty moves list.", "Warning!", MessageBoxButtons.OK);
                return;
            }
            string tempKey = key;
            moveData.Remove(key);
            animData.Remove(key);
            moveAnimSheet.Remove(key);

            key = moveData.First().Key;

            currMove = moveData[key];
            currAnim = animData[key];
            currImg = moveAnimSheet[key];

            moveListBox.Items.Remove(moveListBox.SelectedItem);
            moveListBox.SelectedIndex = 0;
        }

        private void moveListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            moveData[key] = currMove;
            animData[key] = currAnim;
            moveAnimSheet[key] = currImg;

            string tempKey = moveListBox.GetItemText(moveListBox.SelectedItem);
            if (tempKey != "")
                key = tempKey;

            //switch
            currMove = moveData[key];
            currAnim = animData[key];

            movePowerNumeric.Value = currMove.power;
            moveCostNumeric.Value = currMove.cost;
            inflictionChanceNumeric.Value = (decimal)currMove.inflictChance;

            targetComboBox.SelectedIndex = (int)currMove.target;
            inflictionComboBox.SelectedIndex = (int)currMove.infliction;
            methodComboBox.SelectedIndex = (int)currMove.method;
            effectComboBox.SelectedIndex = (int)currMove.eff;

            moveAnimPicBox.Image = moveAnimSheet[key];
            moveAnimDelayNum.Value = (decimal)currAnim.delay;
            moveAnimFrmWdthNum.Value = currAnim.frameWidth;
            moveAnimFrmHgtNum.Value = currAnim.framHeight;
            moveAnimOTRChkBox.Checked = currAnim.oneTimeRun;
        }

        private void editNameBtn_Click(object sender, EventArgs e)
        {
            string newName = ShowInputDialog("Edit Move", "Choose a new move name:");
            if (!moveListBox.Items.Contains(newName))
            {
                moveData.Remove(key);
                animData.Remove(key);
                moveAnimSheet.Remove(key);

                currMove.name = newName;
                moveData.Add(newName, currMove);
                animData.Add(newName, currAnim);
                moveAnimSheet.Add(newName, currImg);

                moveListBox.Items.Add(newName);
                moveListBox.Items.Remove(key);
            }
            else
            {
                key = newName;
                currMove = moveData[key];
                currAnim = animData[key];
                currImg = moveAnimSheet[key];
                moveListBox.SelectedIndex = moveListBox.Items.IndexOf(key);
            }
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
