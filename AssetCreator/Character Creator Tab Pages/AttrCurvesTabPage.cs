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
using EmblemonLib.Combat;

namespace AssetCreator
{
    /// <summary>
    /// Logic for the Attribute Curves Tab Page
    /// 
    /// TODO:
    /// Save/Load Curves from files
    /// </summary>
    public partial class CharacterCreator : Form
    {
        Dictionary<string, LevelingCurve> attrCurves;
        LevelingCurve levelCurve;
        int previousAttrCurveIndex = -1;

        void LoadAttrCurveTab()
        {
            attrCurves = new Dictionary<string, LevelingCurve>()
            {
                { "Attribute Curve", new LevelingCurve(FunctionType.Linear, 1, 1, 1, 0, 0) }
            };
            levelCurve = new LevelingCurve(FunctionType.Linear, 1, 1, 1, 0, 0);

            currentAttrCurveDropDown.SelectedText = "Attribute Curve";
            currentAttrCurveDropDown.SelectedIndex = 0;
            attrCurveFnComboBox.SelectedIndex = 0;
            lvlCurveFnComboBox.SelectedIndex = 0;
            singleCurveRadioBtn.Checked = true;

            FillInFields("Level Curve", false);
            FillInFields("Attribute Curve", true);
        }

        void LoadAttrCurveTabFromFile(string path)
        {
            attrCurves = new Dictionary<string, LevelingCurve>()
            {
                { "Attribute ", new LevelingCurve(FunctionType.Linear, 1, 1, 1, 0, 0) }
            };
            levelCurve = new LevelingCurve(FunctionType.Linear, 1, 1, 1, 0, 0);
        }

        void FillInFields(string curveName, bool isAttrCurve)
        {
            string type;
            LevelingCurve curve;

            if (!attrCurves.ContainsKey(curveName) || levelCurve == null)
            {
                curve = new LevelingCurve(FunctionType.Linear, 1, 1, 1, 0, 0);
                if (isAttrCurve)
                    attrCurves.Add(curveName, curve);
                else
                    levelCurve = curve;
            }
            else
                curve = (isAttrCurve ? attrCurves[curveName] : levelCurve);

            switch (curve.Function)
            {
                case FunctionType.Polynomial:
                    type = "Polynomial";
                    break;
                case FunctionType.Exponential:
                    type = "Exponential";
                    break;
                case FunctionType.Logarithmic:
                    type = "Logarithmic";
                    break;
                default:
                    type =  "Linear";
                    break;
            }
            if (isAttrCurve)
            {
                attrCurveFnComboBox.SelectedText = "";
                attrCurveFnComboBox.SelectedText = type;
                attrCurvePwrNumBox.Value = (decimal)curve.Power;
                attrCurveXSkewNumBox.Value = (decimal)curve.XSkew;
                attrCurveYSkewNumBox.Value = (decimal)curve.YSkew;
                attrCurveXOffNumBox.Value = (decimal)curve.XOffset;
                attrCurveYOffNumBox.Value = (decimal)curve.YOffset;
            }
            else
            {
                attrCurveFnComboBox.SelectedText = "";
                lvlCurveFnComboBox.SelectedText = type;
                lvlCurvePwrNumBox.Value = (decimal)curve.Power;
                lvlCurveXSkewNumBox.Value = (decimal)curve.XSkew;
                lvlCurveYSkewNumBox.Value = (decimal)curve.YSkew;
                lvlCurveXOffNumBox.Value = (decimal)curve.XOffset;
                lvlCurveYOffNumBox.Value = (decimal)curve.YOffset;
            }
        }

        void UpdateLevelCurve(string curveName, bool isAttrCurve)
        {
            string selectedType;
            FunctionType type;
            double functionPower, xSkew, ySkew, xOffset, yOffset;
            selectedType = curveName == "Level Curve" ? lvlCurveFnComboBox.GetItemText(lvlCurveFnComboBox.SelectedItem) 
                : attrCurveFnComboBox.GetItemText(attrCurveFnComboBox.SelectedItem);

            switch (selectedType)
            {
                case "Polynomial":
                    type = FunctionType.Polynomial;
                    break;
                case "Exponential":
                    type = FunctionType.Exponential;
                    break;
                case "Logarithmic":
                    type = FunctionType.Logarithmic;
                    break;
                default:
                    type = FunctionType.Linear;
                    break;
            }

            if (!isAttrCurve)
            {
                functionPower = (double)lvlCurvePwrNumBox.Value;
                xSkew = (double)lvlCurveXSkewNumBox.Value;
                ySkew = (double)lvlCurveYSkewNumBox.Value;
                xOffset = (double)lvlCurveXOffNumBox.Value;
                yOffset = (double)lvlCurveYOffNumBox.Value;
                levelCurve = new LevelingCurve(type, functionPower, xSkew, ySkew, xOffset, yOffset);
                DrawCurve(lvlCurvePicBox, levelCurve);
            }
            else
            {
                functionPower = (double)attrCurvePwrNumBox.Value;
                xSkew = (double)attrCurveXSkewNumBox.Value;
                ySkew = (double)attrCurveYSkewNumBox.Value;
                xOffset = (double)attrCurveXOffNumBox.Value;
                yOffset = (double)attrCurveYOffNumBox.Value;
                LevelingCurve attrCurve = new LevelingCurve(type, functionPower, xSkew, ySkew, xOffset, yOffset);

                if (attrCurves.ContainsKey(curveName))
                    attrCurves[curveName] = attrCurve;
                else
                    attrCurves.Add(curveName, attrCurve);

                DrawCurve(attrCurvePicBox, attrCurve);
            }
        }

        void DrawCurve(PictureBox target, LevelingCurve curve, int originOffset=30, int xScale=3, int yScale=2)
        {
            DrawAxis(target, originOffset, xScale, yScale);

            Point currPoint = new Point(originOffset + (int)curve.XOffset, 
                target.Height - (int)curve.GetExperienceForNextLevel(1) - originOffset - (int)curve.YOffset);
            Point nextPoint;

            for (int i = 2; i <= 100; i++)
            {
                int yCoord = target.Height - ((int)curve.GetExperienceForNextLevel(i) * yScale) - originOffset;
                int xCoord = i * xScale + originOffset;

                if (currPoint.Y < 0 || currPoint.X > target.Width)
                    break;

                nextPoint = new Point(xCoord, yCoord);
                DrawLine(target, currPoint, nextPoint);
                currPoint = nextPoint;
            }
        }

        void DrawAxis(PictureBox targetPictureBox, int originOffset, int xScale, int yScale)
        {
            Point origin = new Point(originOffset, targetPictureBox.Height - originOffset);
            targetPictureBox.Image = null;
            targetPictureBox.Invalidate();

            DrawLine(targetPictureBox, origin, new Point(origin.X, targetPictureBox.Height - (100 * yScale) - originOffset));
            DrawLine(targetPictureBox, origin, new Point((100 * xScale) + originOffset, origin.Y));

            using (Graphics g = Graphics.FromImage(targetPictureBox.Image))
            {
                g.DrawString("0", Font, Brushes.Black, new Point(originOffset - 5, targetPictureBox.Height - originOffset));
                for (int i = 10; i <= 100; i += 10)
                {
                    g.DrawString(i.ToString(), Font, Brushes.Black, new Point((i * xScale) + originOffset, targetPictureBox.Height - originOffset));
                    g.DrawString(i.ToString(), Font, Brushes.Black, new Point(originOffset - 20, targetPictureBox.Height - (i * yScale) - originOffset));
                }
            }
            targetPictureBox.Invalidate();
        }

        void DrawLine(PictureBox targetPictureBox, Point firstDrawPoint, Point secondDrawPoint)
        {
            if (targetPictureBox.Image == null)
            {
                Bitmap bmp = new Bitmap(targetPictureBox.Width, targetPictureBox.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                }
                targetPictureBox.Image = bmp;
            }
            using (Graphics g = Graphics.FromImage(targetPictureBox.Image))
            {
                g.DrawLine(Pens.Black, firstDrawPoint, secondDrawPoint);
            }
            targetPictureBox.Invalidate();
        }

        LevelingCurve LoadCurve(XmlNode node)
        {
            FunctionType type;
            double functionPower, xSkew, ySkew, xOffset, yOffset;

            switch (node["Type"].InnerText)
            {
                case "Polynomial":
                    type = FunctionType.Polynomial;
                    break;
                case "Exponential":
                    type = FunctionType.Exponential;
                    break;
                case "Logarithmic":
                    type = FunctionType.Logarithmic;
                    break;
                default:
                    type = FunctionType.Linear;
                    break;
            }
            functionPower = double.Parse(node["Power"].InnerText);
            xSkew = double.Parse(node["xSkew"].InnerText);
            ySkew = double.Parse(node["ySkew"].InnerText);
            xOffset = double.Parse(node["xOffset"].InnerText);
            yOffset = double.Parse(node["yOffset"].InnerText);
            return new LevelingCurve(type, functionPower, xSkew, ySkew, xOffset, yOffset);
        }

        private void saveAllBtn_Click(object sender, EventArgs e)
        {

        }

        private void saveLvlCurveBtn_Click(object sender, EventArgs e)
        {

        }

        private void saveAttrCurve_Click(object sender, EventArgs e)
        {

        }

        private void loadAllFromFileBtn_Click(object sender, EventArgs e)
        {

        }

        private void loadLvlCurveBttn_Click(object sender, EventArgs e)
        {

        }

        private void loadAttrCurveBttn_Click(object sender, EventArgs e)
        {

        }

        private void lvlCurveFnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void lvlCurvePwrNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void lvlCurveXSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void lvlCurveYSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void lvlCurveXOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void lvlCurveYOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve("Level Curve", false);
        }

        private void currentAttrCurveDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(previousAttrCurveIndex > -1)
            {
                object prevObj = currentAttrCurveDropDown.Items[previousAttrCurveIndex];
                UpdateLevelCurve(currentAttrCurveDropDown.GetItemText(prevObj), true);
            }
            FillInFields(currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem), true);
            previousAttrCurveIndex = currentAttrCurveDropDown.SelectedIndex;
        }

        private void attrCurveFnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }

        private void attrCurvePwrNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }

        private void attrCurveXSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }

        private void attrCurveYSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }

        private void attrCurveXOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }

        private void attrCurveYOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateLevelCurve(currentAttrCurveDropDown.SelectedText, true);
        }


        private void singleCurveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            currentAttrCurveDropDown.Items.Clear();
            currentAttrCurveDropDown.Items.Add("Attribute Curve");
            currentAttrCurveDropDown.SelectedText = "Attribute Curve";
            currentAttrCurveDropDown.SelectedIndex = 0;
            previousAttrCurveIndex = -1;
            FillInFields(currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem), true);
        }

        private void individualCurvesRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            currentAttrCurveDropDown.Items.Clear();
            currentAttrCurveDropDown.Items.Add("Health Curve");
            currentAttrCurveDropDown.Items.Add("Magic Curve");
            currentAttrCurveDropDown.Items.Add("Stamina Curve");
            currentAttrCurveDropDown.Items.Add("Strength Curve");
            currentAttrCurveDropDown.Items.Add("Defense Curve");
            currentAttrCurveDropDown.Items.Add("Power Curve");
            currentAttrCurveDropDown.Items.Add("Fortitude Curve");
            currentAttrCurveDropDown.SelectedText = "Health Curve";
            currentAttrCurveDropDown.SelectedIndex = 0;
            previousAttrCurveIndex = -1;
            FillInFields(currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem), true);
        }

        private void threeTwoTwoRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            currentAttrCurveDropDown.Items.Clear();
            currentAttrCurveDropDown.Items.Add("Resource Curve");
            currentAttrCurveDropDown.Items.Add("Physical Curve");
            currentAttrCurveDropDown.Items.Add("Magical Curve");
            currentAttrCurveDropDown.SelectedText = "Resource Curve";
            currentAttrCurveDropDown.SelectedIndex = 0;
            previousAttrCurveIndex = -1;
            FillInFields(currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem), true);
        }
    }
}
