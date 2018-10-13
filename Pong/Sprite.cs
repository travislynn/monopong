using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public abstract class Sprite
    {
        protected readonly Texture2D texture;
        public Vector2 Location;
        protected readonly Rectangle GameBoundaries;
        public int Width => texture.Width;
        public int Height => texture.Height;
        public Vector2 Velocity { get; set; }

        public float Top => Location.Y + texture.Height;
        public float Bottom => Location.Y;
        public float Right => Location.X + texture.Width;
        public float Left => Location.X;


        protected Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
        {
            this.texture = texture;
            this.Location = location;
            this.GameBoundaries = gameBoundaries;
            this.Velocity = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            Location += Velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds();


    }
}