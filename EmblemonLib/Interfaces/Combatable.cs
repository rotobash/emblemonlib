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
		Random rand;

        Dictionary<string, Animation> battleAnimations;
        Animation currentAnimation;

		StatusInfliction currentInfliction = StatusInfliction.None;
        CharacterStats stats;
        Move currentMove;

        //pass in through constructor
        LevelingCurve levelCurve;
        Dictionary<string, LevelingCurve> attributeCurves;

        Dictionary<string, Move> moves;

        public Combatable(CharacterStats stats, Dictionary<string, Animation> battleAnimations, LevelingCurve levelCurve, Dictionary<string, LevelingCurve> attrCurves, Dictionary<string, Move> moves)
        {
			rand = new Random ();

            this.stats = stats;
            this.battleAnimations = battleAnimations;
            this.moves = moves;

			CurrentHealth = MaxHealth;
			CurrentMagic = MaxMagic;
			CurrentStamina = MaxStamina;

            IsAttacking = false;
            WantsToAttack = false;
            IsSelecting = false;
        }

        public int Experience
        {
            get;
            private set;
        }

		public int CurrentHealth { get; private set; }
		public int CurrentStamina { get; private set; }
		public int CurrentMagic { get; private set; }


        //Read-Only Stats
        public int MaxHealth { get { return stats.Health; } }
		public int MaxMagic { get { return stats.Magic; } }
		public int MaxStamina { get { return stats.Stamina; } }

        public int Level { get { return stats.Level; } }

        public int Strength { get { return stats.Strength; } }
        public int Defense { get { return stats.Defense; } }
        public int Power { get { return stats.Power; } }
        public int Fortitude { get { return stats.Fortitude; } }
        public int Speed { get { return stats.Speed; } }

        public Vector2 Location
        {
            get;
            private set;
        }

        public bool IsAlive { get { return (stats.Health > 0); } }

        //determines what phase of battle is happening
        internal bool WantsToAttack { get; set; }
        internal bool IsAttacking { get; set; }
        internal bool IsSelecting { get; set; }

        public bool AddExperience(int XP)
        {
            Experience += XP;
            if (Experience >= (int)levelCurve.GetExperienceForNextLevel(Level))
            {
                Experience -= (int)levelCurve.GetExperienceForNextLevel(Level);
                stats.LevelUp(attributeCurves);
                return true;
            }
            return false;
        }

        //Determine what move to make
        public Move MakeMoveSelection(string move)
        {
            if (moves.ContainsKey(move))
            {
                currentMove = moves[move];
            }
            return currentMove;
        }

        public bool ChangeAnimation(string key)
        {
            if (battleAnimations.ContainsKey(key))
            {
                currentAnimation.Stop();
                currentAnimation = battleAnimations[key];
                currentAnimation.Start();
                return true;
            }
            return false;
        }

        public void ChangeMove(Move newMove)
        {
            if (moves.ContainsKey(newMove.Name))
            {
                moves[newMove.Name] = newMove;
            }
            else
            {
                moves.Add(newMove.Name, newMove);
            }
        }

        public virtual void BattleUpdate(GameTime gameTime)
        {
            //wait till the animation is finished before moving on
            currentAnimation.Update(gameTime);
            if (IsAttacking)
            {
                currentMove.Update(gameTime);
                if (currentAnimation.HasFinished)
                {
                    IsAttacking = false;
                    Console.Write("Not attacking");
                }
            }
            else
            {
                currentAnimation.Stop();
                currentAnimation.Start();
                currentAnimation.Pause();
            }
        }

        public virtual void BattleDraw(SpriteBatch spritebatch)
        {
            if (IsAttacking)
            {
                currentMove.Draw(spritebatch);
            }
            currentAnimation.Draw(spritebatch);
        }

        public bool ApplyMove(Combatable movePerformer, Move attack)
        {
			if (attack.Effect == Combat.Effect.Damaging) {
				//TODO: calculate run chance
				if (Speed > movePerformer.Speed && rand.Next (10) < 5)
					return false;
				if (attack.Method == Method.Physical) {
					//TODO: calculate physical attack formula
					int damage = (movePerformer.Strength * attack.Power) - Defense;
					CurrentHealth = damage > 0 ? CurrentHealth - damage : CurrentHealth;
					CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
				} else {
					//TODO: calculate physical attack formula
					int damage = (movePerformer.Power * attack.Power) - Fortitude;
					CurrentHealth = damage > 0 ? CurrentHealth - damage : CurrentHealth;
					CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
				}
			} else if (attack.Effect == EmblemonLib.Combat.Effect.Curative) {
				if (attack.Method == Method.Physical) {
					//TODO: calculate physical heal formula
					int heal = (movePerformer.Strength * attack.Power);
					CurrentHealth += heal;
					CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
				} else {
					//TODO: calculate special heal formula
					int heal = (movePerformer.Power * attack.Power);
					CurrentHealth += heal;
					CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
				}
			} 
			return true;
		}

		//This shouldn't be here but in the main battle class
		public List<bool> ApplyMove(List<Combatable> targets, Move attack) {
			List<bool> successes = new List<bool> ();
			foreach (Combatable target in targets) {
				successes.Add (target.ApplyMove (this, attack));
			}
			return successes;
		}

		public bool CanPerformMove(Move attack) {
			if (attack.Method == Method.Physical) {
				if(CurrentStamina > attack.Cost) {
					CurrentStamina -= attack.Cost;
					return true;
				}
				return false;
				
			} else {
				if (CurrentMagic > attack.Cost) {
					CurrentMagic -= attack.Cost;
					return true;
				}
				return false;
			}
		}

		public void ApplyEffect(StatusInfliction effect)
        {
			currentInfliction = effect;
        }
    }
}
