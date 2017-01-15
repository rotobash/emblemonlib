using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using EmblemonLib.Combat;
using EmblemonLib.Interfaces;
using EmblemonLib.Utilities;

namespace EmblemonLib.Data
{
	public class Enemy : Combatable
	{
		public Enemy(CharacterStats stats, Animation characterAnim, Dictionary<string, Move> moves) 
			: base (stats, characterAnim, moves) {
		}

		public override void BattleUpdate(GameTime gameTime) {
			base.BattleUpdate (gameTime);
		}

		public Move MakeMoveSelection (Combatable opponent) {
			int choice = 1;
			//TODO: make AI decisions based on the battle and update choice
			WantsToAttack = true;
			return base.MakeMoveSelection("move1");
		}
	}
}

