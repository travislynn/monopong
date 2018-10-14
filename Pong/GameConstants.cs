using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public static class GameConstants
    {
        public static readonly float PaddleSpeed = 290f;
        public static readonly float BallXSpeed = 320f;
        public static readonly float BallYSpeedPercent = .92f;

        public static int AiReactionThresholdMin = 80;
        public static int AiReactionThresholdMax = 280;
    }
}
