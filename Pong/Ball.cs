using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Ball : Sprite
    {
        public Paddle PlayerPaddle { get; }
        public Paddle OpponentPaddle { get; }
        private Paddle attachedToPaddle;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries, Paddle playerPaddle, Paddle opponentPaddle) : base(texture, location, gameBoundaries)
        {
            PlayerPaddle = playerPaddle;
            OpponentPaddle = opponentPaddle;
        }

        private void InvertYVelocity()
        {
            var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
            Velocity = newVelocity;
        }

        private void SetXVelocity(bool pos)
        {
            if (!pos && Velocity.X > 0f || pos && Velocity.X < 0f)
            {
                var newVelocity = new Vector2(-Velocity.X, Velocity.Y);
                Velocity = newVelocity;
            }
        }

        private Boolean CollisionTop()
        {
            return Top >= GameBoundaries.Height;
        }

        private Boolean CollisionBottom()
        {
            return Bottom <= 0;
        }

        protected override void CheckBounds()
        {
            if (CollisionTop() || CollisionBottom())
            {
                InvertYVelocity();
            }

            // Detect collision with opponent paddle
            if ((Right >= OpponentPaddle.Left) &&
                (Bottom <= OpponentPaddle.Top) &&
                (Top >= OpponentPaddle.Bottom))
            {
                SetXVelocity(false);
            }

            // detect collision with player paddle
            if ((Left <= PlayerPaddle.Right) &&
                (Bottom <= PlayerPaddle.Top) &&
                (Top >= PlayerPaddle.Bottom))
            {
                SetXVelocity(true);
            }

            // detect goes past opponent >> touches right wall >> collision 
            if (Right >= GameBoundaries.Width)
            {
                ResetGame(true);
            } else if (Left <= 0)
            {
                ResetGame(false);
            }
        }

        private void ResetGame(bool paddleWon)
        {
            // update score

            Velocity = Vector2.Zero;
            AttachTo(PlayerPaddle);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            // fire the ball from starting position
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(4f, attachedToPaddle.Velocity.Y);
                Velocity = newVelocity;
                attachedToPaddle = null;
            }

            // stick to paddle
            if (attachedToPaddle != null)
            {
                StickToAttached();
            }

            // call base class
            base.Update(gameTime, gameObjects);
        }

        private void StickToAttached()
        {
            Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
            Location.Y = attachedToPaddle.Location.Y + (attachedToPaddle.Height / 2) - (texture.Height / 2);
        }
    }
}