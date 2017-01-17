using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Interfaces;

namespace EmblemonLib.Utilities
{
	public class SceneManager : Dictionary<string, Scene>
	{
		ContentManager content;

		public string CurrentScene {
			get;
			private set;
		}

		//
		public SceneManager (ContentManager content) : base ()
		{
			this.content = content;	
		}

		public void Update(GameTime gameTime) {
			this [CurrentScene].Update (gameTime);
		}

		void LoadScene() {
			this [CurrentScene].Load (content);
		}

		public void Draw(SpriteBatch spriteBatch) {
			this [CurrentScene].Draw (spriteBatch);
		}

		public void ChangeScene(string name) {
			if (ContainsKey (name)) {
				this [CurrentScene].Unload ();
				CurrentScene = name;
				LoadScene ();
			}
		}
	}
}

