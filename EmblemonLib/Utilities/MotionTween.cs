using Microsoft.Xna.Framework;

namespace EmblemonLib.Utilities
{
    public class MotionTween
    {
        public double Speed { get; set; }
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public Vector2 CurrentPosition { get; }
        public bool Moving { get; private set; }


        private Vector2 currentPosition;
        private Vector2 direction;
        private readonly float distance;


        public MotionTween(Vector2 start, Vector2 end, double speed)
        {
            Start = start;
            End = end;
            Speed = speed;
            Moving = true;

            direction = Vector2.Normalize(end - start);
            distance = Vector2.Distance(start, end);
        }

        public void Update(GameTime gameTime)
        {
            if (Moving)
            {
                var moveSpeed = (float)(Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                currentPosition.X += direction.X * moveSpeed;
                currentPosition.Y += direction.Y * moveSpeed;

                if (Vector2.Distance(Start, currentPosition) >= distance)
                {
                    currentPosition = End;
                    Moving = false;
                }
            }
        }
        
        public void Reset()
        {
            Moving = true;
            currentPosition = Start;
        }
    }
}
