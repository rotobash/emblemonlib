using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Utilities;

namespace EmblemonLib.Combat
{
	public class Spell : Move
	{
		public Spell (AnimationRule moveAnimation)
		{
			animation = moveAnimation;
		}


		public bool isPhysical { get { return false; } }

		public AnimationRule animation { 
			get;
			private set;
		}

		public void Update(GameTime gameTime) {
			animation.Update (gameTime);
		}

		public void Draw(SpriteBatch spriteBatch) {
			animation.Draw (spriteBatch);
		}
	}
}

