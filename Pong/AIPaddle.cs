using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class AIPaddle : Sprite
    {
        private Ball _ball;
        private readonly AIDifficulty difficulty;

        public Ball Ball
        {
            get { return _ball; }
            set { _ball = value; }
        }

        public AIPaddle(Texture2D texture, Vector2 location, Rectangle gameBoundaries, AIDifficulty difficulty) : base(texture, location, gameBoundaries)
        {
            this.difficulty = difficulty;
        }

        private float Speed()
        {
            switch (difficulty)
            {
                case AIDifficulty.Delayed:
                    return 4.2f;
                case AIDifficulty.Slower:
                    return 4.2f;
                case AIDifficulty.Perfect:
                    return GameConstants.PaddleSpeed;
                case AIDifficulty.Threshold:
                    return GameConstants.PaddleSpeed;
                default:
                    return GameConstants.PaddleSpeed;
            }
        }

        public float YThreshold()
        {
            switch (difficulty)
            {
                case AIDifficulty.Threshold:
                    return GameBoundaries.Height / 5f;
                default:
                    return 0f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Ball.Top > Top + YThreshold())
            {
                var newVal = new Vector2(0, Speed());
                Velocity = newVal;
            } else if (Ball.Bottom < Bottom - YThreshold())
            {
                var newVal = new Vector2(0, -Speed());
                Velocity = newVal;
            }
            //else
            //{
            //    var newVal = new Vector2(0, 0);
            //    Velocity = newVal;
            //}

            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            // stop vertical movement when collision with game boundaries
            Location.Y = MathHelper.Clamp(Location.Y, 0, GameBoundaries.Height - texture.Height);
        }
    }
}