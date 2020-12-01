using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Data;
using EmblemonLib.Utilities;

namespace AssetCreator
{
    public struct AnimationData
    {
        public double delay;
        public int frameWidth;
        public int framHeight;
        public bool oneTimeRun;
        public string path;
    }

    public partial class CharacterCreator : Form
    {
        Dictionary<string, Image> battleAnims;
        Dictionary<string, Image> overworldAnims;
        Dictionary<string, AnimationData> battleAnimData;
        Dictionary<string, AnimationData> overworldAnimData;

        void LoadAnimationTab()
        {
            battleAnims = new Dictionary<string, Image>();
            battleAnimData = new Dictionary<string, AnimationData>();
            overworldAnims = new Dictionary<string, Image>();
            overworldAnimData = new Dictionary<string, AnimationData>();
            AnimationData data;

            foreach(object item in currBttlAnimComboBox.Items)
            {
                string key = currBttlAnimComboBox.GetItemText(item);
                data = new AnimationData() { delay = 0.25f, frameWidth = 4, framHeight = 4 };
                battleAnims.Add(key, null);
                battleAnimData.Add(key, data);
            }
            foreach (object item in currOvrwrldAnimComboBox.Items)
            {
                string key = currOvrwrldAnimComboBox.GetItemText(item);
                data = new AnimationData() { delay = 0.25f, frameWidth = 4, framHeight = 4 };
                overworldAnims.Add(key, null);
                overworldAnimData.Add(key, data);
            }

            currBttlAnimComboBox.SelectedIndex = 0;
            currOvrwrldAnimComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Given a PictureBox, an image and the frame size, 
        /// </summary>
        /// <param name="target">Target PictureBox to draw to.</param>
        /// <param name="originalImg">The spritesheet to draw over</param>
        /// <param name="frameWidth">The width of one frame</param>
        /// <param name="frameHeight">The height of one frame</param>
        void DrawFramesOnImg(PictureBox target, int frameWidth, int frameHeight)
        {
            if (target.Image == null)
            {
                Bitmap bmp = new Bitmap(target.Width, target.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                }
                target.Image = bmp;
            }
            using (Graphics g = Graphics.FromImage(target.Image))
            {
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

        private void addBattleSheetBtn_Click(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            if (battleSpriteSheetPicBox.Image != null &&
                MessageBox.Show("This will get rid of the current spritesheet for this animation. Are you sure you want to add this spritesheet?",
                "Caution!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            AnimationData data = battleAnimData[key];
            data.path = dlg.FileName;
            battleAnimData[key] = data;
            Image img = Image.FromStream(dlg.OpenFile());
            battleAnims[key] = img;

            DrawFramesOnImg(battleSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void currOvrwrldAnimComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            overworldSpriteSheetPicBox.Image = overworldAnims[key];
            AnimationData data = overworldAnimData[key];
            ovrwrldDelayNum.Value = data.delay > 0 ? (decimal)data.delay : 0.25m;
            ovrwrldFrmWdthNum.Value = data.frameWidth > 0 ? data.frameWidth : 1;
            ovrwrldFrmHgtNum.Value = data.framHeight > 0 ? data.framHeight : 1;
            checkBox1.Checked = data.oneTimeRun;
            if (overworldSpriteSheetPicBox.Image != null)
                DrawFramesOnImg(overworldSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void ovrwrldFrmWdthNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            AnimationData data = overworldAnimData[key];
            data.frameWidth = (int)ovrwrldFrmWdthNum.Value;
            overworldAnimData[key] = data;
            Bitmap img = (Bitmap)overworldAnims[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            overworldSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(overworldSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void ovrwrldFrmHgtNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            AnimationData data = overworldAnimData[key];
            data.framHeight = (int)ovrwrldFrmHgtNum.Value;
            overworldAnimData[key] = data;
            Bitmap img = (Bitmap)overworldAnims[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            overworldSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(overworldSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void ovrwrldDelayNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            AnimationData data = overworldAnimData[key];
            data.delay = (double)ovrwrldDelayNum.Value;
            overworldAnimData[key] = data;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            AnimationData data = overworldAnimData[key];
            data.oneTimeRun = checkBox1.Checked;
            overworldAnimData[key] = data;
        }

        private void currBttlAnimComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            battleSpriteSheetPicBox.Image = battleAnims[key];
            AnimationData data = battleAnimData[key];
            bttlDelayNum.Value = data.delay > 0 ? (decimal)data.delay : 0.25m;
            bttlFrmWdthNum.Value = data.frameWidth > 0 ? data.frameWidth : 1;
            bttlFrmHgtNum.Value = data.framHeight > 0 ? data.framHeight : 1;
            bttlOTRChkBox.Checked = data.oneTimeRun;
            if (battleSpriteSheetPicBox.Image != null)
                DrawFramesOnImg(battleSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void addOvrwrldSheetBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            if (overworldSpriteSheetPicBox.Image != null &&
                   MessageBox.Show("This will get rid of the current spritesheet for this animation. Ae you sure you want to add this spritesheet?",
                   "Caution!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string key = currOvrwrldAnimComboBox.GetItemText(currOvrwrldAnimComboBox.SelectedItem);
            AnimationData data = overworldAnimData[key];
            data.path = dlg.FileName;
            overworldAnimData[key] = data;
            
            Image img = Image.FromStream(dlg.OpenFile());
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(tempBitmap))
                g.DrawImage(img, 0, 0);

            overworldSpriteSheetPicBox.Image = tempBitmap;
            overworldAnims[key] = tempBitmap;

            DrawFramesOnImg(overworldSpriteSheetPicBox,  data.frameWidth, data.framHeight);
        }

        private void bttlFrmWdthNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            AnimationData data = battleAnimData[key];
            data.frameWidth = (int)bttlFrmWdthNum.Value;
            battleAnimData[key] = data;

            Bitmap img = (Bitmap)battleAnims[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            battleSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(battleSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void bttlFrmHgtNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            AnimationData data = battleAnimData[key];
            data.framHeight = (int)bttlFrmHgtNum.Value;
            battleAnimData[key] = data;
            Image img = battleAnims[key];
            battleSpriteSheetPicBox.Image = img;
            DrawFramesOnImg(battleSpriteSheetPicBox, data.frameWidth, data.framHeight);
        }

        private void bttlDelayNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            AnimationData data = battleAnimData[key];
            data.delay = (double)bttlDelayNum.Value;
            battleAnimData[key] = data;
        }

        private void bttlOTRChkBox_CheckedChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            AnimationData data = battleAnimData[key];
            data.oneTimeRun = checkBox1.Checked;
            battleAnimData[key] = data;
        }

        void SerializeAnimations(XmlWriter wr)
        {
            AnimationData temp;
            string textureFilePath = "";
            System.IO.Directory.CreateDirectory(data.name + "'s Animations");
            wr.WriteStartElement("Animations");

            if (charType == CharacterType.Overworld || charType == CharacterType.Hybrid)
            {
                wr.WriteStartElement("OverworldAnimations");
                foreach (string overworldAnim in overworldAnimData.Keys)
                {
                    temp = overworldAnimData[overworldAnim];
                    wr.WriteStartElement("Item");
                    wr.WriteAttributeString("key", overworldAnim);

                    if (temp.path != null)
                    {
                        string extension = temp.path.Split('.')[1];
                        textureFilePath = data.name + "'s Animations\\" + overworldAnim + "." + extension;
                        if (System.IO.File.Exists(textureFilePath))
                            System.IO.File.Delete(textureFilePath);
                        System.IO.File.Copy(temp.path, textureFilePath);
                    }

                    wr.WriteElementString("Texture", textureFilePath);
                    wr.WriteElementString("FrameSize", temp.frameWidth.ToString() + " " + temp.framHeight.ToString());
                    wr.WriteElementString("Delay", temp.delay.ToString());
                    wr.WriteElementString("OTR", temp.oneTimeRun.ToString());
                    wr.WriteEndElement();
                }
                wr.WriteEndElement();
            }

            if (charType == CharacterType.Battle || charType == CharacterType.Hybrid)
            {
                wr.WriteStartElement("BattleAnimations");
                foreach (string battleAnim in battleAnimData.Keys)
                {
                    temp = battleAnimData[battleAnim];
                    wr.WriteStartElement("Item");
                    wr.WriteAttributeString("key", battleAnim);

                    if (temp.path != null)
                    {
                        string extension = temp.path.Split('.')[1];
                        textureFilePath = data.name + "'s Animations\\" + battleAnim + "." + extension;
                        if (System.IO.File.Exists(textureFilePath))
                            System.IO.File.Delete(textureFilePath);
                        System.IO.File.Copy(temp.path, textureFilePath);
                    }

                    wr.WriteElementString("Texture", textureFilePath);
                    wr.WriteElementString("FrameSize", temp.frameWidth.ToString() + " " + temp.framHeight.ToString());
                    wr.WriteElementString("Delay", temp.delay.ToString());
                    wr.WriteElementString("OTR", temp.oneTimeRun.ToString());
                    wr.WriteEndElement();
                }
                wr.WriteEndElement();
                wr.WriteEndElement();
            }
        }

        void LoadAnims(XmlNode body)
        {
            AnimationData tempAnim;
            Image tempImg;


            if(charType == CharacterType.Battle || charType == CharacterType.Hybrid)
            {
                battleAnims.Clear();
                battleAnimData.Clear();
                foreach(XmlElement battleAnimNode in body["BattleAnimations"].ChildNodes)
                {
                    string key = battleAnimNode.GetAttribute("key");
                    tempAnim = new AnimationData();
                    tempAnim.path = battleAnimNode["Texture"].InnerText;
                    tempImg = null;
                    if (tempAnim.path != "")
                    {
                        //indexed pixel formats (BMP Files)
                        tempImg = Image.FromFile(tempAnim.path);
                        Bitmap tempBitmap = new Bitmap(tempImg.Width, tempImg.Height);
                        using (Graphics g = Graphics.FromImage(tempBitmap))
                            g.DrawImage(tempImg, 0, 0);

                        battleSpriteSheetPicBox.Image = tempBitmap;
                        tempImg = tempBitmap;
                    }
                    string[] frameSize = battleAnimNode["FrameSize"].InnerText.Split(' ');
                    tempAnim.frameWidth = int.Parse(frameSize[0]);
                    tempAnim.framHeight = int.Parse(frameSize[1]);
                    tempAnim.delay = float.Parse(battleAnimNode["Delay"].InnerText);
                    tempAnim.oneTimeRun = bool.Parse(battleAnimNode["OTR"].InnerText);

                    battleAnimData.Add(key, tempAnim);
                    battleAnims.Add(key, tempImg);
                }
                currBttlAnimComboBox.SelectedIndex = 0;
            }

            if (charType == CharacterType.Overworld || charType == CharacterType.Hybrid)
            {
                overworldAnimData.Clear();
                overworldAnims.Clear();
                foreach (XmlElement overworldAnimNode in body["OverworldAnimations"].ChildNodes)
                {
                    string key = overworldAnimNode.GetAttribute("key");
                    tempAnim = new AnimationData();
                    tempAnim.path = overworldAnimNode["Texture"].InnerText;
                    tempImg = null;
                    if (tempAnim.path != "")
                    {
                        tempImg = Image.FromFile(tempAnim.path);
                        Bitmap tempBitmap = new Bitmap(tempImg.Width, tempImg.Height);
                        using (Graphics g = Graphics.FromImage(tempBitmap))
                            g.DrawImage(tempImg, 0, 0);
                        overworldSpriteSheetPicBox.Image = tempBitmap;
                        tempImg = tempBitmap;
                    }

                    string[] frameSize = overworldAnimNode["FrameSize"].InnerText.Split(' ');
                    tempAnim.frameWidth = int.Parse(frameSize[0]);
                    tempAnim.framHeight = int.Parse(frameSize[1]);
                    tempAnim.delay = float.Parse(overworldAnimNode["Delay"].InnerText);
                    tempAnim.oneTimeRun = bool.Parse(overworldAnimNode["OTR"].InnerText);
                    overworldAnimData.Add(key, tempAnim);
                    overworldAnims.Add(key, tempImg);
                }
                currOvrwrldAnimComboBox.SelectedIndex = 0;
            }
        }
    }
}
