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
        public Animation Overlay { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
        public float InflictChance { get; set; }
        public Target Target { get; set; }
        public Method Method { get; set; }
        public StatusInfliction Infliction { get; set; }
        public Effect Effect { get; set; }

        public Move()
        {
        }

        public void Update(GameTime gameTime)
        {
            Overlay.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Overlay.Draw(spriteBatch);
        }

        public void UpdateLocation(Vector2 newLocation)
        {
            Overlay.UpdateLocation(newLocation);
        }
    }
}

