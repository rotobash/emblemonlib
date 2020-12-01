using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using EmblemonLib.Interfaces;

namespace EmblemonLib.Utilities
{
	public class SceneManager
	{
        readonly ContentManager content;
        readonly Stack<Scene> sceneStack;

		//
		public SceneManager (ContentManager content)
		{
			this.content = content;
			sceneStack = new Stack<Scene>();
		}

		public void Update(GameTime gameTime)
		{
			sceneStack.Peek().Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var scene in sceneStack)
            {
				scene.Draw(spriteBatch);
            }
		}

		public void PushNewScene(Scene item)
        {
			item.Load(content);
			sceneStack.Push(item);
		}

		public void PopScene()
        {
			var scene = sceneStack.Pop();
			scene.Unload();
		}
	}
}

