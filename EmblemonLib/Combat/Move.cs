using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Utilities;

namespace EmblemonLib.Combat
{
	public interface Move
	{
		bool isPhysical { get; }
		AnimationRule animation { get; }
		void Update (GameTime gameTime);
		void Draw (SpriteBatch spriteBatch);
	}
}

