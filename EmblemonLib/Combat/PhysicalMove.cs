using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Utilities;

namespace EmblemonLib.Combat
{
	public class PhysicalMove : Move
	{

		public PhysicalMove (AnimationRule moveAnimation)
		{
			animation = moveAnimation;
		}

		public bool isPhysical { 
			get { return true; } 
		}

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

