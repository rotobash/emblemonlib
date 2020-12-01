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
    public class CharacterFactory
    {
        public static CharacterFactory GetInstance { get; } = new CharacterFactory();

        public virtual HybridCharacter BuildHybrid(string path, ContentManager cm)
        {
            XmlDocument charDoc = new XmlDocument();
            charDoc.Load(path);
            XmlNode charXml = charDoc["Character"];
            CharacterStats stats;

            LevelingCurve levelCurve;
            Dictionary<string, LevelingCurve> attrCurves;

            Animation overworldAnim;
            Dictionary<string, Animation> battleAnimations;
            Dictionary<string, Move> moves;

            if (charXml["Type"].InnerText != "Hybrid")
            {
                throw new NotSupportedException("This is the wrong factory for this type of character.");
            }

            //Leveling Curves
            levelCurve = LoadCurve(charXml["LevelCurve"]);
            attrCurves = LoadCurves(charXml["AttributeCurves"]);

            //Stats
            stats = LoadStats(charXml["CharacterStats"]);

            //Animations
            battleAnimations = LoadAnimations(charXml["BattleAnimations"], cm);

            overworldAnim = LoadAnimation(charXml["OverworldAnimation"], cm);
            moves = LoadMoves(charXml["MoveList"], cm);

            return new HybridCharacter(stats, overworldAnim, battleAnimations, levelCurve, attrCurves, moves);
        }

        public virtual BattleCharacter BuildBattle(string path, ContentManager cm)
        {
            XmlDocument charDoc = new XmlDocument();
            charDoc.Load(path);
            XmlNode charXml = charDoc["Character"];
            CharacterStats stats;

            LevelingCurve levelCurve;
            Dictionary<string, LevelingCurve> attrCurves;
            
            Dictionary<string, Animation> battleAnimations;
            Dictionary<string, Move> moves;

            if (charXml["Type"].InnerText != "Battle")
            {
                throw new NotSupportedException("This is the wrong factory for this type of character.");
            }

            //Leveling Curves
            levelCurve = LoadCurve(charXml["LevelCurve"]);
            attrCurves = LoadCurves(charXml["AttributeCurves"]);

            //Stats
            stats = LoadStats(charXml["CharacterStats"]);

            //Animations
            battleAnimations = LoadAnimations(charXml["BattleAnimations"], cm);
            
            moves = LoadMoves(charXml["MoveList"], cm);

            return new BattleCharacter(stats, battleAnimations, levelCurve, attrCurves, moves);
        }

        public virtual OverworldCharacter BuildOverworld(string path, ContentManager cm)
        {
            XmlDocument charDoc = new XmlDocument();
            charDoc.Load(path);
            XmlNode charXml = charDoc["Character"];

            Animation overworldAnim;

            if (charXml["Type"].InnerText != "Overworld")
            {
                throw new NotSupportedException("This is the wrong factory for this type of character.");
            }

            overworldAnim = LoadAnimation(charXml["OverworldAnimation"], cm);

            return new OverworldCharacter("", overworldAnim);
        }

        CharacterStats LoadStats(XmlNode node)
        {
            int statParseSucess = 0;

            statParseSucess = int.TryParse(node["Health"].InnerText, out int health) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Magic"].InnerText, out int magic) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Stamina"].InnerText, out int stamina) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Level"].InnerText, out int level) ? ++statParseSucess : statParseSucess;

            statParseSucess = int.TryParse(node["Strength"].InnerText, out int strength) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Defense"].InnerText, out int defense) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Power"].InnerText, out int power) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Fortitude"].InnerText, out int fortitude) ? ++statParseSucess : statParseSucess;
            statParseSucess = int.TryParse(node["Speed"].InnerText, out int speed) ? ++statParseSucess : statParseSucess;

            if (statParseSucess != CharacterStats.STATNUMBER)
                throw new FormatException("All stats were not parsed successfully");

            CharacterStats stats = new CharacterStats()
            {
                Health = health,
                Magic = magic,
                Stamina = stamina,
                Level = level,
                Strength = strength,
                Defense = defense,
                Power = power,
                Fortitude = fortitude,
                Speed = speed
            };

            return stats;
        }

        LevelingCurve LoadCurve(XmlNode node)
        {
            FunctionType type;
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

            return new LevelingCurve()
            {
                Function = type,
                Power = double.Parse(node["Power"].InnerText),
                XSkew = double.Parse(node["xSkew"].InnerText),
                YSkew = double.Parse(node["ySkew"].InnerText),
                XOffset = double.Parse(node["xOffset"].InnerText),
                YOffset = double.Parse(node["yOffset"].InnerText)
            };
        }

        Dictionary<string, LevelingCurve> LoadCurves(XmlNode node)
        {
            Dictionary<string, LevelingCurve>  curves = new Dictionary<string, LevelingCurve>();
            foreach (XmlNode subnode in node.ChildNodes)
               curves.Add(subnode.Attributes[0].InnerText, LoadCurve(subnode));
            return curves;
        }

        Animation LoadAnimation(XmlNode node, ContentManager cm)
        {
            Point tempPoint;
            string[] parsedPoint = node["FrameSize"].InnerText.Split(' ');
            tempPoint = new Point(int.Parse(parsedPoint[0]), int.Parse(parsedPoint[1]));
            return new Animation(cm.Load<Texture2D>(node["Texture"].InnerText), tempPoint);
        }

        Dictionary<string, Animation> LoadAnimations(XmlNode node, ContentManager cm)
        {
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
            foreach (XmlNode subnode in node.ChildNodes)
                animations.Add(subnode.Attributes[0].InnerText, LoadAnimation(subnode, cm));
            return animations;
        }

        private static Dictionary<string, Move> LoadMoves(XmlNode node, ContentManager cm)
        {
            Dictionary<string, Move> moves = new Dictionary<string, Move>();
            string[] moveList = node.InnerText.Split(' ');
            foreach (string move in moveList)
            {
                string movePath = "Content/Moves/" + move + ".xml";
                moves.Add(move, MoveFactory.GetInstance.GetOrLoadMove(movePath, cm));
            }

            return moves;
        }
    }
}
