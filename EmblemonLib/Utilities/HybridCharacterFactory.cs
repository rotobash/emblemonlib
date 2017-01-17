using System;
using System.Collections.Generic;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using EmblemonLib.Combat;
using EmblemonLib.Data;

namespace EmblemonLib.Utilities
{
    public static class HybridCharacterFactory
    {
        public static HybridCharacter Build(string path, ContentManager cm)
        {
            XmlDocument charDoc = new XmlDocument();
            charDoc.Load(path);
            XmlNode charXml = charDoc["Character"];
            CharacterStats stats = new CharacterStats();
            Animation overworldAnim;
            string[] parsedPoint;
            Point tempPoint;
            XmlNode node;
            LevelingCurve levelCurve;
            Dictionary<string, LevelingCurve> attrCurves = new Dictionary<string, LevelingCurve>();

            Dictionary<string, Animation> battleAnimations = new Dictionary<string, Animation>();

            int health, magic, stamina, level, strength, defense, power, fortitude, speed;
            int statParseSucess = 0;


            if (charXml["Type"].InnerText != "Hybrid")
            {
                throw new NotSupportedException("This is the wrong factory for this type of character.");
            }

            //LevelingCurves
            LevelingCurve curve;
            FunctionType type;
            double functionPower;
            double xSkew;
            double ySkew;
            double xOffset;
            double yOffset;

            node = charXml["LevelCurve"];
            switch(node["Type"].InnerText)
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
            levelCurve = new LevelingCurve(type, functionPower, xSkew, ySkew, xOffset, yOffset);

            node = charXml["AttributeCurves"];
            foreach (XmlNode subnode in node.ChildNodes)
            {
                switch (subnode["Type"].InnerText)
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
                functionPower = double.Parse(subnode["Power"].InnerText);
                xSkew = double.Parse(subnode["xSkew"].InnerText);
                ySkew = double.Parse(subnode["ySkew"].InnerText);
                xOffset = double.Parse(subnode["xOffset"].InnerText);
                yOffset = double.Parse(subnode["yOffset"].InnerText);
                curve = new LevelingCurve(type, functionPower, xSkew, ySkew, xOffset, yOffset);
                attrCurves.Add(subnode.Attributes[0].InnerText, curve);
            }

            //STATS
            node = charXml["CharacterStats"];
            statParseSucess = int.TryParse(node["Health"].InnerText, out health) ? ++statParseSucess : statParseSucess;
			statParseSucess = int.TryParse(node["Magic"].InnerText, out magic) ? ++statParseSucess : statParseSucess;
			statParseSucess = int.TryParse(node["Magic"].InnerText, out stamina) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Level"].InnerText, out level) ? ++statParseSucess : statParseSucess;

            statParseSucess = int.TryParse(node["Strength"].InnerText, out strength) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Defense"].InnerText, out defense) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Power"].InnerText, out power) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Fortitude"].InnerText, out fortitude) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Speed"].InnerText, out speed) ? ++statParseSucess : statParseSucess;
            //statParseSucess = int.TryParse(node["Health"].InnerText, out health) ? ++statParseSucess : statParseSucess;

            if (statParseSucess != CharacterStats.STATNUMBER)
                throw new FormatException("All stats were not parsed successfully");

			stats.LoadStats (health, magic, stamina, level, strength, defense, power, fortitude, speed);

            //Animations
            node = charXml["BattleAnimations"];
            foreach(XmlNode subnode in node.ChildNodes)
            {
                parsedPoint = subnode["FrameSize"].InnerText.Split(' ');
                tempPoint = new Point(int.Parse(parsedPoint[0]), int.Parse(parsedPoint[1]));
                Animation anim = new Animation(cm.Load<Texture2D>(subnode["Texture"].InnerText), tempPoint);
                battleAnimations.Add(subnode.Attributes[0].InnerText, anim);
            }

            node = charXml["OverworldAnimation"];
            parsedPoint = node["FrameSize"].InnerText.Split(' ');
            tempPoint = new Point(int.Parse(parsedPoint[0]), int.Parse(parsedPoint[1]));
            overworldAnim = new Animation(cm.Load<Texture2D>(node["Texture"].InnerText), tempPoint);

            Dictionary<string, Move> moves = new Dictionary<string, Move>();
            string[] moveList = charXml["MoveList"].InnerText.Split(' ');
            foreach(string move in moveList)
            {
                string movePath = "Content/Moves/" + move + ".xml";
                moves.Add(move, MoveFactory.Build(movePath, cm));
            }

            return new HybridCharacter(stats, overworldAnim, battleAnimations, levelCurve, attrCurves, moves);
        }

        public static void Save(HybridCharacter hybridChar, string savePath)
        {
            //now go the opposite way
        }
    }
}
