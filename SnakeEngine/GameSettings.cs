using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public struct GameSettings
    {
        public bool BorderMirroring { get; }
        public int StonesCount { get; }
        public int Width { get; }
        public int Height { get; }
        public int Speed { get; }

        public GameSettings(bool borderMirroring, int stonesCount, int width, int height, int speed)
        {
            BorderMirroring = borderMirroring;
            StonesCount = stonesCount;
            Width = width;
            Height = height;
            Speed = speed;
        }
    }
}
