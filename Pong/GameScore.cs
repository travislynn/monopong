using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class GameScore
    {
        private readonly SpriteFont font;
        private readonly Rectangle gameBoundaries;

        private string scoreString
        {
            get { return $"Score {PlayerScore} - {OpponentScore}"; }
        }

        private Vector2 scoreLocation
        {
            get { return new Vector2((gameBoundaries.Right / 2) - (font.MeasureString(scoreString).X / 2), gameBoundaries.Bottom / 2); }
        }

        public int PlayerScore { get; set; }
        public int OpponentScore { get; set; }

        public GameScore(SpriteFont font, Rectangle gameBoundaries)
        {
            this.font = font;
            this.gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, scoreString, scoreLocation, Color.Black);
        }
    }
}