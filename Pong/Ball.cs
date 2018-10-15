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

        // I'm using this method so that clips on vertical boundaries behave appropriately
        // simply inverting makes the ball get stuck as it is within the paddle for multiple ticks
        // eg.  Only invert if ball is heading in the opposite direction
        private void SetXVelocity(bool pos, Paddle paddle)
        {
            if (!pos && Velocity.X > 0f || pos && Velocity.X < 0f)
            {
                var centerX = BoundingBox.Center.X;
                var centerY = BoundingBox.Center.Y;

                var paddleYCenter = paddle.BoundingBox.Center.Y;
                var paddleTop = paddle.BoundingBox.Top;
                var paddleBottom = paddle.BoundingBox.Bottom;


                float newVelX = GameConstants.BallXSpeed; //Velocity.X;
                float newVelY =  Velocity.Y;
                if (Velocity.X < 0) newVelX = -newVelX;

                int yPercent = 50;
                int xPercent = 50;

                if (centerY > paddleYCenter + paddle.BoundingBox.Height / 2 ||
                    centerY < paddleYCenter - paddle.BoundingBox.Height / 2)
                {
                    yPercent = 75;
                    xPercent = 25;
                }

                // at center point, decide to reflect up or down
                if (centerY >= paddleYCenter)
                {
                    // go up
                    newVelY = GameConstants.BallXSpeed;
                }
                else
                {
                    // go down
                    newVelY = -GameConstants.BallXSpeed;
                }

                // enforce mins and max
                if (yPercent > 90) yPercent = 90;
                if (yPercent < 10) yPercent = 10;
                if (xPercent > 90) xPercent = 90;
                if (xPercent < 10) xPercent = 10;

                
                // apply percent changes to velocities.  Should be no change yet at 50/50
                newVelY = newVelY * yPercent / 100;
                newVelX = newVelX * xPercent / 100;


                var newVelocity = new Vector2(-newVelX, newVelY);
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
                var newVelocity = new Vector2(gameObjects.PlayerPaddle.Velocity.Y, GameConstants.BallXSpeed);
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
                    SetXVelocity(false, gameObjects.ComputerPaddle);
                } else if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox))
                {
                    SetXVelocity(true, gameObjects.PlayerPaddle);
                }
            }

            // call base class
            base.Update(gameTime, gameObjects);
        }

        private void StickToAttached()
        {

            Location.X = attachedToPaddle.BoundingBox.Right;
            //Location.Y = attachedToPaddle.BoundingBox.Top - (attachedToPaddle.BoundingBox.Height / 2);

            //Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
            Location.Y = attachedToPaddle.Location.Y + (attachedToPaddle.Height / 2) - (texture.Height / 2);
        }
    }
}