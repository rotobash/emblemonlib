using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmblemonLib.Utilities
{
	public class PathAnimation
	{
		List<Animation> movingAnims;
        Animation movingAnim;
        bool singleAnimation;

		List<Animation> staticAnims;
		float movementSpeed;
		Vector2 destinationVec;

		public bool HasFinished {
			get;
			private set;
		}

		public PathAnimation (List<Animation> movingAnimations, List<Animation> staticAnimations, Vector2 destination, float speed=1f)
		{
			movingAnims = movingAnimations;
            staticAnims = staticAnimations;

            singleAnimation = false;
			destinationVec = destination;
			movementSpeed = speed;
        }

        public PathAnimation(Animation animation, List<Animation> staticAnimations, Vector2 destination, float speed = 1f)
        {
            movingAnim = animation;
            staticAnims = staticAnimations;

            singleAnimation = true;
            destinationVec = destination;
            movementSpeed = speed;
        }

        public void Update(GameTime gameTime) {

            UpdateAnimations(gameTime);

            if (staticAnims != null)
            {
                foreach (Animation staticAnim in staticAnims)
                {
                    staticAnim.Update(gameTime);
                }
            }

		}

        void UpdateAnimations(GameTime gameTime)
        {
            for (int i = 0; i < movingAnims.Count; i++)
            {
                bool positionChanged = UpdatePosition(movingAnims[i]);
                if (positionChanged)
                    movingAnim.Update(gameTime);
                else
                {
                    movingAnims.RemoveAt(i);
                    i--;
                }
            }
        }

        bool UpdatePosition(Animation movingAnim)
        {
            float x = 0, y = 0;

            if (movingAnim.Location.Y < destinationVec.Y)
            {
                y = movementSpeed;

            }
            if (movingAnim.Location.X < destinationVec.X)
            {
                x = movementSpeed;
            }
            if (x != 0f && y != 0f) {
                movingAnim.UpdateLocation(movingAnim.Location + new Vector2(x, y));
                return true;
            }
            return false;
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

