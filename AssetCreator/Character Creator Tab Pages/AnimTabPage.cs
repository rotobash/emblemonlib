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
                data = new AnimationData();
                data.frameWidth = 4;
                data.framHeight = 4;
                battleAnims.Add(key, null);
                battleAnimData.Add(key, data);
            }
            foreach (object item in currOvrwrldAnimComboBox.Items)
            {
                string key = currOvrwrldAnimComboBox.GetItemText(item);
                data = new AnimationData();
                data.frameWidth = 4;
                data.framHeight = 4;
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
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            battleAnims[key] = img;
            battleSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(battleSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
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
            DrawFramesOnImg(overworldSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
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
            DrawFramesOnImg(overworldSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
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
            overworldAnims[key] = img;
            overworldSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(overworldSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
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
            DrawFramesOnImg(battleSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
        }

        private void bttlFrmHgtNum_ValueChanged(object sender, EventArgs e)
        {
            string key = currBttlAnimComboBox.GetItemText(currBttlAnimComboBox.SelectedItem);
            AnimationData data = battleAnimData[key];
            data.framHeight = (int)bttlFrmHgtNum.Value;
            battleAnimData[key] = data;
            Bitmap img = (Bitmap)battleAnims[key] ?? new Bitmap(data.frameWidth + 1, data.framHeight + 1);
            Bitmap tempBitmap = new Bitmap(img.Width, img.Height);
            battleSpriteSheetPicBox.Image = tempBitmap;
            DrawFramesOnImg(battleSpriteSheetPicBox, img, data.frameWidth, data.framHeight);
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
    }
}
