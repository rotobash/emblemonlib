using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmblemonLib.Interfaces
{
	public interface Scene
	{
		void Load(ContentManager content);

		void Update(GameTime gameTime);

		void Draw(SpriteBatch spriteBatch);

		void Unload();
	}
}

