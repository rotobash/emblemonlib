using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using EmblemonLib.Combat;
using EmblemonLib.Interfaces;
using EmblemonLib.Utilities; 

namespace EmblemonLib.Data
{
	public class PlayerCharacter : Combatable, Character
	{
		bool isMoving;
		Animation overWorldAnimation;

		public PlayerCharacter(CharacterStats stats, Animation overworldAnim, Animation battleAnim, Dictionary<string, Move> moves) 
			: base (stats, battleAnim, moves) {
			overWorldAnimation = overworldAnim;
		}

		public bool IsPlayable {
			get { return true; }
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
			bool isMoving = true;
			overWorldAnimation.UpdateLocation (newPosition);
		}


	}
}
