using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmblemonLib.Utilities
{
	public class Animation
	{
		bool isSpriteSheet;
		double timePassed;

		bool onetimerun;
		bool paused = false;
		bool stopped = false;

		Texture2D spriteSheet;
		Rectangle currentFrame;

		List<Texture2D> frameData;
		int totalFrames;
		int currentFrameIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmblemonLib.Utilities.Animation"/> class.
		/// </summary>
		/// <param name="frameData">The frames of the animation.</param>
		/// <param name="location">Where to draw the animation.</param>
		/// <param name="delay">Time between frames.</param>
		public Animation (List<Texture2D> frameData, Vector2 location, double delay=0.25f, bool onetimerun=false)
		{
			isSpriteSheet = false;
			this.frameData = frameData;
			Delay = delay;
			Location = location;
			this.onetimerun = onetimerun;

			currentFrameIndex = 0;
			totalFrames = frameData.Count;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmblemonLib.Utilities.Animation"/> class.
		/// </summary>
		/// <param name="spriteSheet">Sprite sheet.</param>
		/// <param name="frameSize">The size of any frame of animation for the spritesheet.</param>
		/// <param name="location">Where to draw the animation.</param>
		/// <param name="delay">Time between delays.</param>
		public Animation(Texture2D spriteSheet, Rectangle frameSize, Vector2 location, double delay=0.25f, bool onetimerun=false) 
		{
			isSpriteSheet = true;
			this.spriteSheet = spriteSheet;
			Delay = delay;
			Location = location;
			this.onetimerun = onetimerun;

			currentFrame = frameSize;
			currentFrame.X = 0;
			currentFrame.Y = 0;
		}

		public void Update(GameTime gameTime) {
			timePassed = (paused || stopped) ? 0 : timePassed + gameTime.ElapsedGameTime.TotalSeconds;
			if (timePassed >= Delay) { 
				timePassed = 0;
				if (isSpriteSheet) {
					currentFrame.X += currentFrame.Width;
					if (currentFrame.X >= spriteSheet.Bounds.Width) {
						currentFrame.X = 0;
						currentFrame.Y = (currentFrame.Y >= spriteSheet.Bounds.Height) ? 0 : currentFrame.Y + currentFrame.Height;
						if (onetimerun) {
							stopped = true;
							HasFinished = true;
						}
					}
				} else {
					currentFrameIndex = (currentFrameIndex >= totalFrames) ? 0 : ++currentFrameIndex;
					if (onetimerun) {
						stopped = true;
						HasFinished = true;
					}
				}
			}
		}

		public void Start() {
			if (stopped) {
				if (isSpriteSheet) {
					currentFrame.X = 0;
					currentFrame.Y = 0;
				} else {
					currentFrameIndex = 0;
				}
			}
			paused = false;
			stopped = false;
			HasFinished = false;
		}

		public void Pause() {
			paused = true;
		}

		public void Stop() {
			stopped = true;
			HasFinished = true;
		}

		/// <summary>
		/// Draws the current frame of animation.
		/// </summary>
		/// <param name="spriteBatch">An instantiated copy of the spritebatch.</param>
		public void Draw(SpriteBatch spriteBatch) {
			if (!paused) {
				if (isSpriteSheet) {
					spriteBatch.Draw (spriteSheet, Location, currentFrame, Color.White);
				} else {
					spriteBatch.Draw (frameData [currentFrameIndex], Location, Color.White);
				}
			}
		}

		public void DrawOvertop(SpriteBatch spriteBatch, Animation otherAnimation) {
			otherAnimation.Draw (spriteBatch);
			this.Draw (spriteBatch);
		}

		public void UpdateLocation(Vector2 newLocation) {
			Location = newLocation;
		}

		public Vector2 Location {
			get;
			private set;
		}

		public double Delay {
			get;
			private set;
		}

		public bool HasFinished {
			get;
			private set;
		}
	}
}

