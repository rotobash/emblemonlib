using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using EmblemonLib.Interfaces;

namespace EmblemonLib.Combat
{
	public enum BattlePhase { MoveSelection, Attacking, BattleWon, BattleLost }

	public class TurnBasedBattle
	{
		BattlePhase phase = BattlePhase.MoveSelection;
		Party playerParty;
		Party enemyParty;

		int playerMoveSelection = 0;

		Combatable playerOnField;
		Combatable enemyOnField;


		public TurnBasedBattle (Party playerParty, Party enemyParty)
		{
			this.playerParty = playerParty;
			this.enemyParty = enemyParty;

			phase = BattlePhase.MoveSelection;

			playerOnField = playerParty[0];
			enemyOnField = enemyParty [0];
		}

		/// <summary>
		/// Updates the battle based on what battle phase we are in.
		/// </summary>
		/// <param name="gametime">Gametime.</param>
		public void Update (GameTime gametime) {
			switch (phase) {
				case BattlePhase.Attacking:
					AttackPhase (gametime);
					break;
				case BattlePhase.MoveSelection:
					SelectionPhase ();
					break;
			}
		}

		public BattlePhase GetPhase {
			get { return phase; }
		}

		public void SetPlayerSelection(int selection) {
			playerMoveSelection = selection;
			playerOnField.MakeMoveSelection ("move1");
		}

		void SelectionPhase() {
			if (playerMoveSelection > 0) {
				playerOnField.IsSelecting = false;
				playerOnField.WantsToAttack = true;
				phase = BattlePhase.Attacking;
				enemyOnField.MakeMoveSelection ("move1");
			}
		}

		/// <summary>
		/// Updates the relevant player while the battle is in the attack phase.
		/// </summary>
		/// <param name="gametime">Gametime.</param>
		void AttackPhase(GameTime gametime) {
			//if the player is attacking
			if (playerOnField.IsAttacking) {
				playerOnField.BattleUpdate (gametime);
				//if the enemy is attacking
			} else if (enemyOnField.IsAttacking) {
				enemyOnField.BattleUpdate (gametime);
			} else {
				DeterminePostAttackState ();
			}
		}


		void DeterminePostAttackState() {
			//if one want to attack and the other currently is not
			if (!playerOnField.IsAttacking && enemyOnField.WantsToAttack) {
				enemyOnField.WantsToAttack = false;
				enemyOnField.IsAttacking = true;
			} else if (!enemyOnField.IsAttacking && playerOnField.WantsToAttack) {
				playerOnField.WantsToAttack = false;
				playerOnField.IsAttacking = true;
			} else {
				CheckIfBattleOver ();
			}
		}

		void CheckIfBattleOver() {
			playerParty.RemoveDeadCombatants ();
			enemyParty.RemoveDeadCombatants ();
			if (!playerParty.IsAtLeastOneAlive()) {
				phase = BattlePhase.BattleLost;
			} else if (!enemyParty.IsAtLeastOneAlive()) {
				phase = BattlePhase.BattleWon;
				//TODO: calculate xp
			} else {
				phase = BattlePhase.MoveSelection;
			}
		}

		//done
		/// <summary>
		/// Determines the first attacker based on who's faster.
		/// </summary>
		void DetermineFirstAttacker() {
			if (enemyOnField.Speed > playerOnField.Speed) {
				enemyOnField.IsAttacking = true;
			} else {
				playerOnField.IsAttacking = true;
			}
		}

		//done
		/// <summary>
		/// Draw the arena and the combatants
		/// </summary>
		/// <param name="spritebatch">Spritebatch.</param>
		public void Draw(SpriteBatch spritebatch) {
			//draw arena
			enemyOnField.BattleDraw (spritebatch);
			playerOnField.BattleDraw (spritebatch);
		}


	}
}

