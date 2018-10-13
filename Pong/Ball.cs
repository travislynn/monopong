using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Ball : Sprite
    {
        private Paddle attachedToPaddle;

        public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries) : base(texture, location, gameBoundaries)
        {
            
        }

        public bool PastOpponent
        {
            get { return BoundingBox.Left >= GameBoundaries.Right; }
        }

        public bool PastPlayer
        {
            get { return BoundingBox.Right <= GameBoundaries.Left; }
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

        protected override void CheckBounds(GameObjects gameObjects)
        {
            if (CollisionTop() || CollisionBottom())
            {
                InvertYVelocity();
            }

            // Detect collision with opponent paddle
            //if ((Right >= gameObjects.ComputerPaddle.Left) &&
            //    (Bottom <= gameObjects.ComputerPaddle.Top) &&
            //    (Top >= gameObjects.ComputerPaddle.Bottom))
            //{
            //    SetXVelocity(false);
            //}

            //// detect collision with player paddle
            //if ((Left <= gameObjects.PlayerPaddle.Right) &&
            //    (Bottom <= gameObjects.PlayerPaddle.Top) &&
            //    (Top >= gameObjects.PlayerPaddle.Bottom))
            //{
            //    SetXVelocity(true);
            //}

            //// detect goes past opponent >> touches right wall >> collision 
            //if (Right >= GameBoundaries.Width)
            //{
            //    ResetGame(true, gameObjects.PlayerPaddle);
            //} else if (Left <= 0)
            //{
            //    ResetGame(false, gameObjects.PlayerPaddle);
            //}
        }

        //private void ResetGame(bool paddleWon, Paddle paddle)
        //{
        //    // update score

        //    Velocity = Vector2.Zero;
        //    AttachTo(paddle);
        //}

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            // fire the ball from starting position
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(4f, attachedToPaddle.Velocity.Y * .9f);
                Velocity = newVelocity;
                attachedToPaddle = null;
            }

            // stick to paddle
            if (attachedToPaddle != null)
            {
                StickToAttached();
            }
            else
            {
                // detect paddle collisions
                if (BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
                {
                    SetXVelocity(false);
                } else if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox))
                {
                    SetXVelocity(true);
                }
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