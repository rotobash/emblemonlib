using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using EmblemonLib.Combat;
using Microsoft.Xna.Framework;

namespace EmblemonLib.Utilities
{
    public class MoveFactory
    {
        static MoveFactory instance = new MoveFactory();
        //keep memory of moves we've loaded
        Dictionary<string, Move> movesLoaded = new Dictionary<string, Move>();

        public Dictionary<string, Move> GetAllMovesLoaded
        {
            get
            {
                return movesLoaded;
            }
        }

        public static MoveFactory GetInstance
        {
            get
            {
                return instance;
            }
        }

        public Move GetOrLoadMove(string path, ContentManager cm)
        {
            XmlDocument moveDoc = new XmlDocument();
            moveDoc.Load(path);
            XmlNode moveXml = moveDoc["Move"];

            string name = moveXml["Name"].InnerText;

            if (movesLoaded.ContainsKey(name))
                return movesLoaded[name];

            return BuildMove(moveXml, name, cm);
        }


        public Move BuildMove(XmlNode moveXml, string name, ContentManager cm)
        {
			int power = int.Parse(moveXml["Power"].InnerText);
			int cost = int.Parse(moveXml["Cost"].InnerText);
            float inflictChance = float.Parse(moveXml["StatusInfliction"].Attributes[0].InnerText);

            Target target;
            Method method;
            StatusInfliction infliction;
            Combat.Effect effect;

            Animation overlay;

            switch (moveXml["Target"].InnerText)
            {
                case "Self":
                    target = Target.Self;
                    break;
                case "PartyMember":
                    target = Target.PartyMember;
                    break;
                case "EnemyParty":
                    target = Target.EnemyParty;
                    break;
                case "PlayerParty":
                    target = Target.PlayerParty;
                    break;
                case "Enemy":
                    target = Target.Enemy;
                    break;
                case "Everyone":
                    target = Target.Everyone;
                    break;
                case "Any":
                    target = Target.Any;
                    break;
                default:
                    target = Target.None;
                    break;
            }

            switch (moveXml["Method"].InnerText)
            {
                case "Spell":
                    method = Method.Spell;
                    break;
                default:
                    method = Method.Physical;
                    break;
            }

            switch (moveXml["StatusInfliction"].InnerText)
            {
                case "Burn":
                    infliction = StatusInfliction.Burn;
                    break;
                case "Confusion":
                    infliction = StatusInfliction.Confusion;
                    break;
                case "Stun":
                    infliction = StatusInfliction.Stun;
                    break;
                case "Freeze":
                    infliction = StatusInfliction.Freeze;
                    break;
                case "Poison":
                    infliction = StatusInfliction.Poison;
                    break;
                default:
                    infliction = StatusInfliction.None;
                    break;
            }

            switch (moveXml["Effect"].InnerText)
            {
                case "Damaging":
                    effect = Combat.Effect.Damaging;
                    break;
                case "Curative":
                    effect = Combat.Effect.Curative;
                    break;
                default:
                    effect = Combat.Effect.None;
                    break;
            }

            XmlNode node = moveXml["Animation"];
            string[] parsedPoint = node["FrameSize"].InnerText.Split(' ');
            Point tempPoint = new Point(int.Parse(parsedPoint[0]), int.Parse(parsedPoint[1]));
            overlay = new Animation(cm.Load<Texture2D>(node["Texture"].InnerText), tempPoint, 0.25f, true);

			return new Move(name, power, cost, inflictChance, target, method, infliction, effect, overlay);
        }
    }
}
