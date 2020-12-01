using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmblemonLib.Utilities
{
	public class PathAnimation
	{
		List<Animation> movingAnims;
		List<Animation> staticAnims;
        Queue<MotionTween> motionPaths;

        MotionTween currentPath;

		public bool HasFinished {
			get;
			private set;
		}

		public PathAnimation (List<Animation> movingAnimations, List<Animation> staticAnimations, List<MotionTween> paths)
		{
			movingAnims = movingAnimations;
            staticAnims = staticAnimations;

            motionPaths = new Queue<MotionTween>();
            foreach (var path in paths)
            {
			    motionPaths.Enqueue(path);
            }
            SetNextPath();
        }

        public PathAnimation(Animation animation, List<Animation> staticAnimations, List<MotionTween> paths)
        {
            movingAnims = new List<Animation>() 
            { 
                animation
            };

            staticAnims = staticAnimations;

            motionPaths = new Queue<MotionTween>();
            foreach (var path in paths)
            {
                motionPaths.Enqueue(path);
            }
            SetNextPath();
        }

        public void Update(GameTime gameTime)
        {
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
            currentPath.Update(gameTime);
            for (int i = 0; i < movingAnims.Count; i++)
            {
                var movingAnim = movingAnims[i];
                movingAnim.Location = currentPath.CurrentPosition;
                movingAnim.Update(gameTime);
            }

            if (!currentPath.Moving)
            {
                SetNextPath();
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
			foreach (Animation staticAnim in staticAnims)
            {
				staticAnim.Draw(spritebatch);
			}

			foreach (Animation movingAnim in movingAnims) 
            {
				movingAnim.Draw(spritebatch);
			}
		}

        private void SetNextPath()
        {
            currentPath = motionPaths.Dequeue();
            currentPath.Reset();
        }
	    
	}
}

