using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public enum PlayerType
    {
        Human,
        Computer,
        PerfectComputer
    }

    public class Paddle : Sprite
    {
        private readonly PlayerType _playerType;
        //public Ball Ball { get; set; }

        public Paddle(Texture2D texture, Vector2 location, Rectangle gameBoundaries, PlayerType playerType) : 
            base(texture, location, gameBoundaries)
        {
            _playerType = playerType;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (_playerType == PlayerType.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    // move paddle up
                    Velocity = new Vector2(0, -5f);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    // move paddle down
                    Velocity = new Vector2(0, GameConstants.PaddleSpeed);
                }
            } else if (_playerType == PlayerType.Computer)
            {
                if (gameObjects.Ball.Bottom > Top + YThreshold())
                {
                    var newVal = new Vector2(0, Speed());
                    Velocity = newVal;
                }
                else if (gameObjects.Ball.Top < Bottom - YThreshold())
                {
                    var newVal = new Vector2(0, -Speed());
                    Velocity = newVal;
                }
            }
            
            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds(GameObjects gameObjects)
        {
            // stop vertical movement when collision with game boundaries
            
            // check collision with top or bottom
            if (Velocity != Vector2.Zero && (Location.Y + texture.Height >= GameBoundaries.Height || Location.Y <= 0))
            {
                Velocity = Vector2.Zero;
            }
            Location.Y = MathHelper.Clamp(Location.Y, 0, GameBoundaries.Height - texture.Height);
        }

        public float YThreshold()
        {
            switch (_playerType)
            {
                case PlayerType.Computer:
                    return GameBoundaries.Height / 7.5f;
                    //return 0f;
                default:
                    return 0f;
            }
        }

        private float Speed()
        {
            switch (_playerType)
            {
                default:
                    return GameConstants.PaddleSpeed;
            }
        }



    }


}