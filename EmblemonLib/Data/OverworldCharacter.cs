using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using EmblemonLib.Combat;
using EmblemonLib.Interfaces;
using EmblemonLib.Utilities;

namespace EmblemonLib.Data
{
	public class OverworldCharacter : Character
    {
        string name;
        bool isMoving;
        Animation overWorldAnimation;

        public OverworldCharacter(string name, Animation overworldAnim) {
            this.name = name;
            overWorldAnimation = overworldAnim;
        }

        public string Name
        {
            get { return name; }
        }

        public void OverworldUpdate(GameTime gameTime)
        {
            if (!isMoving)
            {
                overWorldAnimation.Pause();
            }
            else
            {
                isMoving = false;
            }
            overWorldAnimation.Update(gameTime);
        }

        public void OverworldDraw(SpriteBatch spritebatch)
        {
            overWorldAnimation.Draw(spritebatch);
        }

        public void MoveTo(Vector2 newPosition)
        {
            isMoving = true;
            overWorldAnimation.UpdateLocation(newPosition);
        }
    }
}

