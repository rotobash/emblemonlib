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
    public struct CurveData
    {
        public FunctionType type;
        public double power, xSkew, ySkew, xOffset, yOffset;
    }

    public partial class CharacterCreator : Form
    {
        Dictionary<string, CurveData> attrCurves;
        CurveData curve;
        string key;

        string[] curves = { "", "Level Curve", "Health Curve", "Magic Curve", "Stamina Curve", "Speed Curve",
            "Strength Curve", "Defense Curve" , "Power Curve", "Fortitude Curve" };

        void LoadAttrCurveTab()
        {
            attrCurves = new Dictionary<string, CurveData>() {
                { "Attribute Curve", new CurveData() { power = 1, xSkew = 1, ySkew = 1, type = FunctionType.Linear } }
            };
            
            foreach(string curveStr in curves)
                attrCurves.Add(curveStr, new CurveData() { power = 1, xSkew = 1, ySkew = 1, type = FunctionType.Linear });
            
            key = "Attribute Curve";
            curve = attrCurves[key];
            singleCurveRadioBtn.Checked = true;
            currentAttrCurveDropDown.SelectedIndex = 0;
        }

        void FillInFields()
        {
            attrCurveFnComboBox.SelectedIndex = attrCurveFnComboBox.Items.IndexOf(curve.type.ToString());
            attrCurvePwrNumBox.Value = (decimal)curve.power;
            attrCurveXSkewNumBox.Value = (decimal)curve.xSkew;
            attrCurveYSkewNumBox.Value = (decimal)curve.ySkew;
            attrCurveXOffNumBox.Value = (decimal)curve.xOffset;
            attrCurveYOffNumBox.Value = (decimal)curve.yOffset;
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

        private void currentAttrCurveDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            attrCurves[key] = curve;
            key = currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem);
            curve = attrCurves[key];
            FillInFields();
        }

        private void attrCurveFnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = attrCurveFnComboBox.GetItemText(attrCurveFnComboBox.SelectedItem);
            curve.type = (FunctionType)Enum.Parse(typeof(FunctionType), selectedType);
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private void attrCurvePwrNumBox_ValueChanged(object sender, EventArgs e)
        {
            curve.power = (double)attrCurvePwrNumBox.Value;
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private void attrCurveXSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            curve.xSkew = (double)attrCurveXSkewNumBox.Value;
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private void attrCurveYSkewNumBox_ValueChanged(object sender, EventArgs e)
        {
            curve.ySkew = (double)attrCurveYSkewNumBox.Value;
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private void attrCurveXOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            curve.xOffset = (double)attrCurveXOffNumBox.Value;
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private void attrCurveYOffNumBox_ValueChanged(object sender, EventArgs e)
        {
            curve.yOffset = (double)attrCurveYOffNumBox.Value;
            DrawCurve(attrCurvePicBox, GetLevelingCurve());
        }

        private LevelingCurve GetLevelingCurve()
        {
            return new LevelingCurve()
            {
                Function = curve.type,
                Power = curve.power,
                XSkew = curve.xSkew,
                YSkew = curve.ySkew,
                XOffset = curve.xOffset,
                YOffset = curve.yOffset
            };
        }


        private void singleCurveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            currentAttrCurveDropDown.Items.Clear();
            currentAttrCurveDropDown.Items.Add("Attribute Curve");
            currentAttrCurveDropDown.SelectedIndex = 0;
        }

        private void individualCurvesRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            currentAttrCurveDropDown.Items.Clear();
            currentAttrCurveDropDown.Items.Add("Level Curve");
            currentAttrCurveDropDown.Items.Add("Health Curve");
            currentAttrCurveDropDown.Items.Add("Magic Curve");
            currentAttrCurveDropDown.Items.Add("Stamina Curve");
            currentAttrCurveDropDown.Items.Add("Strength Curve");
            currentAttrCurveDropDown.Items.Add("Defense Curve");
            currentAttrCurveDropDown.Items.Add("Power Curve");
            currentAttrCurveDropDown.Items.Add("Fortitude Curve");
            currentAttrCurveDropDown.Items.Add("Speed Curve");
            currentAttrCurveDropDown.SelectedIndex = 0;
        }

        void SerializeLevelCurves(XmlWriter wr)
        {
            wr.WriteStartElement("Curves");

            CurveData tempCurve;
            foreach (string curve in curves)
            {
                wr.WriteStartElement("Item");
                if (singleCurveRadioBtn.Checked)
                    tempCurve = attrCurves["Attribute Curve"];
                else
                    tempCurve = attrCurves[curve];

                wr.WriteAttributeString("key", curve);
                wr.WriteElementString("Type", tempCurve.type.ToString());
                wr.WriteElementString("Power", tempCurve.power.ToString());
                wr.WriteElementString("xSkew", tempCurve.xSkew.ToString());
                wr.WriteElementString("ySkew", tempCurve.ySkew.ToString());
                wr.WriteElementString("xOffset", tempCurve.xOffset.ToString());
                wr.WriteElementString("yOffset", tempCurve.yOffset.ToString());

                wr.WriteEndElement();
            }
            wr.WriteEndElement();
        }

        void LoadAttrCurve(XmlNode body)
        {
            attrCurves.Clear();
            key = "";
            attrCurves.Add("Attribute Curve", new CurveData() { power = 1, xSkew = 1, ySkew = 1, type = FunctionType.Linear });
            foreach (XmlElement item in body.ChildNodes)
            {
                string key = item.Attributes[0].InnerText;
                if (key == "")
                    return;
                
                FunctionType type = (FunctionType)Enum.Parse(typeof(FunctionType), item["Type"].InnerText);
                double power = double.Parse(item["Power"].InnerText);
                double xskew = double.Parse(item["xSkew"].InnerText);
                double yskew = double.Parse(item["ySkew"].InnerText);
                double xoffset = double.Parse(item["xOffset"].InnerText);
                double yoffset = double.Parse(item["yOffset"].InnerText);

                curve = new CurveData() { type=type, power=power, xSkew=xskew, ySkew=yskew, xOffset=xoffset, yOffset=yoffset };
                attrCurves.Add(key, curve);
            }
            //ensure our fields are updated
            individualCurvesRadioBtn.Checked = true;
            attrCurves[key] = curve;
            key = currentAttrCurveDropDown.GetItemText(currentAttrCurveDropDown.SelectedItem);
            curve = attrCurves[key];
            FillInFields();
        }
    }
}
