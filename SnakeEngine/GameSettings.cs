using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class GameSettings
    {
        public bool bordermirroring;
        public int stonesCount;
        public int width;
        public int height;
        public int speed;
        public GameSettings(bool _borderMirroring, int _stonesCount, int _width, int _height, int _speed)
        {
            bordermirroring = _borderMirroring;
            stonesCount = _stonesCount;
            width = _width;
            height = _height;
            speed = _speed;
        }
    }
}
