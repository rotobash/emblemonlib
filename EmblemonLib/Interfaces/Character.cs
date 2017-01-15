using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EmblemonLib.Interfaces
{
	public interface Character
	{
		bool IsPlayable { get; }

		void OverworldUpdate(GameTime gameTime);
		void OverworldDraw(SpriteBatch spritebatch);

		void MoveTo (Vector2 newPosition);

	}
}
