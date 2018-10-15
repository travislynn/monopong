using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public static class GameConstants
    {
        public static readonly float PaddleSpeed = 300f;
        public static readonly float BallXSpeed = 340f;
        //public static readonly float BallYSpeedPercent = .95f;

        public static int AiReactionThresholdMin = 80;
        public static int AiReactionThresholdMax = 290;
    }
}
