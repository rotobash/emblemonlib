using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using EmblemonLib.Combat;
using EmblemonLib.Interfaces;
using EmblemonLib.Utilities; 

namespace EmblemonLib.Data
{
	public class HybridCharacter : Combatable, Character
	{
        string name;
		bool isMoving;
		Animation overWorldAnimation;

		public HybridCharacter(CharacterStats stats, Animation overworldAnim, Dictionary<string, Animation> battleAnimations, LevelingCurve levelCurve, Dictionary<string, LevelingCurve> attrCurves, Dictionary<string, Move> moves) 
			: base (stats, battleAnimations, levelCurve, attrCurves, moves) {
			overWorldAnimation = overworldAnim;
		}

        public string Name
        {
            get { return name; }
        }

		public void OverworldUpdate(GameTime gameTime) {
			if (!isMoving) {
				overWorldAnimation.Pause ();
			} else {
				isMoving = false;
			}
			overWorldAnimation.Update (gameTime);
		}

		public override void BattleUpdate(GameTime gameTime) {
			
			base.BattleUpdate(gameTime);
		}

		public void OverworldDraw(SpriteBatch spritebatch) {
			overWorldAnimation.Draw (spritebatch);
		}

		public void MoveTo (Vector2 newPosition) {
			isMoving = true;
			overWorldAnimation.UpdateLocation (newPosition);
		}


	}
}
