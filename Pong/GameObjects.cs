using System.Xml.Schema;

namespace Pong
{
    public class GameObjects
    {
        public Ball Ball { get; set; }
        public Paddle PlayerPaddle { get; set; }
        public Paddle ComputerPaddle { get; set; }
        public GameScore Score { get; set; }
    }
}