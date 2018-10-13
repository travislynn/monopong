using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class GameScore
    {
        private readonly SpriteFont Font;
        private readonly Rectangle GameBoundaries;

        public int PlayerScore { get; set; }
        public int OpponentScore { get; set; }

        public GameScore(SpriteFont font, Rectangle gameBoundaries)
        {
            Font = font;
            GameBoundaries = gameBoundaries;
        }
    }
}