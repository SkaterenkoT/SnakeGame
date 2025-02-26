using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public struct GameSettings
    {
        public bool borderMirroring;
        public int stonesCount;
        public int width;
        public int height;
        public int speed;
        public GameSettings(bool _borderMirroring, int _stonesCount, int _width, int _height, int _speed)
        {
            borderMirroring = _borderMirroring;
            stonesCount = _stonesCount;
            width = _width;
            height = _height;
            speed = _speed;
        }
    }
}
