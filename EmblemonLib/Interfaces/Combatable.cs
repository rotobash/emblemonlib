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
        Dictionary<string, Animation> battleAnimations;
        Animation currentAnimation;

        CharacterStats stats;
        Move currentMove;

        //pass in through constructor
        LevelingCurve levelCurve;
        Dictionary<string, LevelingCurve> attributeCurves;

        Dictionary<string, Move> moves;

        public Combatable(CharacterStats stats, Dictionary<string, Animation> battleAnimations, LevelingCurve levelCurve, Dictionary<string, LevelingCurve> attrCurves, Dictionary<string, Move> moves)
        {
            this.stats = stats;
            this.battleAnimations = battleAnimations;
            this.moves = moves;

            IsAttacking = false;
            WantsToAttack = false;
            IsSelecting = false;
        }

        public int Experience
        {
            get;
            private set;
        }

        //Read-Only Stats
        public int Health { get { return stats.Health; } }
        public int Magic { get { return stats.Magic; } }

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

        public int CalculateDamage(Move attack)
        {
            //TODO: base damage on move and other stats
            return Strength;
        }
        public int CalculateDefense(Move attack)
        {
            //TODO: base defense on move and other stats
            return Defense;
        }

        public bool ApplyEffect(Effects effect)
        {
            bool wasApplied = false;

            switch (effect)
            {
                case Effects.Confused:
                    //do some chance stuff here
                    //see if effect is applied
                    wasApplied = true;
                    break;
            }
            return wasApplied;
        }
    }
}
