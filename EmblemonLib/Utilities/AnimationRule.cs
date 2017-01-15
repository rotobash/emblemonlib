using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmblemonLib.Utilities
{
	public class AnimationRule
	{
		List<Animation> movingAnims;
		List<Animation> staticAnims;
		float movementSpeed;
		Vector2 destinationVec;

		public bool HasFinished {
			get;
			private set;
		}

		public AnimationRule (List<Animation> staticAnimations, List<Animation> movingAnimations, Vector2 destination, float speed=1f)
		{
			staticAnims = staticAnimations;
			movingAnims = movingAnimations;

			destinationVec = destination;
			movementSpeed = speed;
		}

		public void Update(GameTime gameTime) {
			int animationsFinished = 0;
			foreach (Animation movingAnim in movingAnims) {
				float x = 0;
				float y = 0;
				if (movingAnim.Location.Y < destinationVec.Y) {
					y = movementSpeed;

				}
				if (movingAnim.Location.X < destinationVec.X) {
					x = movementSpeed;
				}
				if (x == 0 && y == 0)
					animationsFinished++;
					
				movingAnim.UpdateLocation (movingAnim.Location + new Vector2 (x, y));
				movingAnim.Update (gameTime);
			}
			foreach (Animation staticAnim in staticAnims) {
				staticAnim.Update (gameTime);
			}
			HasFinished = (animationsFinished == movingAnims.Count);

		}

		public void Draw(SpriteBatch spritebatch) {
			foreach (Animation staticAnim in staticAnims) {
				staticAnim.Draw (spritebatch);
			}
			foreach (Animation movingAnim in movingAnims) {
				movingAnim.Draw (spritebatch);
			}
		}
	    
	}
}

