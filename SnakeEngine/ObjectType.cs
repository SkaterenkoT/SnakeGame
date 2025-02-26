using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public enum ObjectType : byte
    {
        Empty,
        SnakeHead,
        SnakeBody,
        SnakeTail,
        Stone,
        Food,
    }
}
