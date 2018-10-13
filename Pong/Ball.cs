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
                var newVelocity = new Vector2(6f, attachedToPaddle.Velocity.Y * .92f);
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

            Location.X = attachedToPaddle.BoundingBox.Right;
            //Location.Y = attachedToPaddle.BoundingBox.Top - (attachedToPaddle.BoundingBox.Height / 2);

            //Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
            Location.Y = attachedToPaddle.Location.Y + (attachedToPaddle.Height / 2) - (texture.Height / 2);
        }
    }
}