using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Utilities;

namespace EmblemonLib.Combat
{
    public enum Target { Self, PartyMember, Enemy, EnemyParty, PlayerParty, Everyone, Any, None }
    public enum Method { Spell, Physical }
    public enum StatusInfliction { None, Poison, Stun, Confusion, Burn, Freeze }
    public enum Effect { Damaging, Curative, None }

    public class Move
	{
        float inflictChance;

        Target target;
        Method method;
        StatusInfliction infliction;
        Effect effect;

        Animation overlay;

        public Move (string name, int power, float inflictChance, Target target, Method method, StatusInfliction infliction, Effect effect, Animation animation)
        {
            Name = name;
            Power = power;
            InflictChance = inflictChance;
            Target = target;
            Method = method;
            Infliction = infliction;
            Effect = effect;
            overlay = animation;
        }

        public string Name
        {
            get;
            private set;
        }

        public int Power
        {
            get;
            private set;
        }

        public float InflictChance
        {
            get;
            private set;
        }

        public Target Target
        {
            get;
            private set;
        }

        public Method Method
        {
            get;
            private set;
        }

        public StatusInfliction Infliction
        {
            get;
            private set;
        }

        public Effect Effect
        {
            get;
            private set;
        }

        public void Update (GameTime gameTime)
        {
            overlay.Update(gameTime);
        }
		public void Draw (SpriteBatch spriteBatch)
        {
            overlay.Draw(spriteBatch);
        }

        public void UpdateLocation(Vector2 newLocation)
        {
            overlay.UpdateLocation(newLocation);
        }
	}
}

