using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using EmblemonLib.Combat;
using EmblemonLib.Utilities;

namespace EmblemonLib.Interfaces
{
	public abstract class Combatable
	{
		Animation characterAnimation;

		CharacterStats stats;
		Move currentMove;

		//pass in through constructor
		LevelingCurve levelCurve;
		Dictionary<LevelingCurve, string> attributeCurves;

		Dictionary<string, Spell> spells;
		Dictionary<string, PhysicalMove> physicalMoves;

		public Combatable(CharacterStats stats, Animation characterAnimation, Dictionary<string, Move> moves) {
			this.stats = stats;
			this.characterAnimation = characterAnimation;
			int moveNumber = 1;
			physicalMoves = new Dictionary<string, PhysicalMove> ();
			spells = new Dictionary<string, Spell> ();

			IsAttacking = false;
			WantsToAttack = false;
			IsSelecting = false;

			string moveString = "move1";

			while(moves.ContainsKey(moveString)) {
				Console.Write (moveString);
				physicalMoves.Add(moveString, (PhysicalMove)moves[moveString]);
				moveNumber++;
				moveString = string.Format ("move{0}", moveNumber);
			}
		}

		//Read-Only Stats
		public int Health { get { return stats.Health; } }
		public int Magic  { get { return stats.Magic; } }

		public int Level { get { return stats.Level; } }

		public int Strength { get { return stats.Strength; } }
		public int Defense { get { return stats.Defense; } }
		public int Power { get { return stats.Power; } }
		public int Fortitude { get { return stats.Fortitude; } }
		public int Speed { get { return stats.Speed; } }

		public Vector2 Location {
			get;
			private set;
		}

		public bool IsAlive  { get { return (stats.Health > 0); } }

		//determines what phase of battle is happening
		internal bool WantsToAttack { get; set; }
		internal bool IsAttacking { get; set; }
		internal bool IsSelecting { get; set; }

		//Determine what move to make
		public Move MakeMoveSelection(string move) {
			if (physicalMoves.ContainsKey(move)) {
				currentMove = physicalMoves[move];
			} else {
				currentMove = spells [move];
			}
			return currentMove;
		}

		public void ChangePhysicalMove (PhysicalMove newMove, int slot) {
			string moveIndex = string.Format ("move{0)", slot);
			if (physicalMoves.ContainsKey (moveIndex)) {
				physicalMoves [moveIndex] = newMove;
			} else {
				physicalMoves.Add(moveIndex, newMove);
			}
		}

		public void ChangeSpell (Spell newSpell, int slot) {
			string spellIndex = string.Format ("spell{0)", slot);
			if (spells.ContainsKey (spellIndex)) {
				spells [spellIndex] = newSpell;
			} else {
				spells.Add(spellIndex, newSpell);
			}
		}

		public void SetCharacterAnimation (Animation newAnim) {
			characterAnimation = newAnim;
		}

		public virtual void BattleUpdate(GameTime gameTime) {
			//wait till the animation is finished before moving on
			characterAnimation.Update(gameTime);
			if (IsAttacking) {
				currentMove.Update(gameTime);
				if (characterAnimation.HasFinished) {
					IsAttacking = false;
					Console.Write ("Not attacking");
				}
			} else {
				characterAnimation.Stop ();
				characterAnimation.Start ();
				characterAnimation.Pause ();
			}
		}

		public virtual void BattleDraw(SpriteBatch spritebatch) {
			if (IsAttacking) {
				currentMove.Draw (spritebatch);
			} else {
				characterAnimation.Draw (spritebatch);
			}
		}

		public int CalculatePhysicalDamage (Move attack) {
			//TODO: base damage on move and other stats
			return Strength;
		}
		public int CalculatePhysicalDefense(Move attack) {
			//TODO: base defense on move and other stats
			return Defense;
		}

		public bool ApplyEffect(Effects effect) {
			bool wasApplied = false;

			switch (effect) {
			case Effects.Confused:
				//do some chance stuff here
				//see if effect is applied
				wasApplied = true;
				break;
			}
			return wasApplied;
		}

		public int CalculateMagicDamage (Spell attack) {
			//TODO: base damage on move and other stats
			//and calculate if you have enough mana
			return Power;
		}
		public int CalculateMagicDefense(Spell attack){
			//TODO: base defense on move and other stats
			return Fortitude;
		}
	}
}
