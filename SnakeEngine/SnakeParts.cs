using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class SnakeHead : MapObject
    {
        public SnakeHead(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.SnakeHead;
        }
    }
    public class SnakeBody : MapObject
    {
        public SnakeBody(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.SnakeBody;
        }
    }
}
