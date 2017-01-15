using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using EmblemonLib.Interfaces;
using EmblemonLib.Combat;
using EmblemonLib.Utilities;

namespace EmblemonLib.Data
{
	public class BattleCharacter : Combatable
	{
        string name;

        public BattleCharacter (CharacterStats stats, Dictionary<string, Animation> battleAnimations, LevelingCurve levelCurve, Dictionary<string, LevelingCurve> attrCurves, Dictionary<string, Move> moves) 
			: base (stats, battleAnimations, levelCurve, attrCurves, moves) {
        }

        public string Name
        {
            get { return name; }
        }

        public override void BattleUpdate(GameTime gameTime)
        {
            base.BattleUpdate(gameTime);
        }

        public Move MakeMoveSelection(Combatable opponent)
        {
            int choice = 1;
            //TODO: make AI decisions based on the battle and update choice
            WantsToAttack = true;
            return base.MakeMoveSelection("move1");
        }
    }
}
