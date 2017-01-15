using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using EmblemonLib.Interfaces;

namespace EmblemonLib.Data
{
	public class NonPlayerCharacter : Character
	{
		public NonPlayerCharacter()
		{
		}

		public bool IsPlayable {
			get { return false; }
		}

		public void OverworldUpdate(GameTime gameTime) {
			
		}
		public void OverworldDraw(SpriteBatch spritebatch) {
			
		}

		public void MoveTo (Vector2 newPosition) {
			
		}
	}
}
