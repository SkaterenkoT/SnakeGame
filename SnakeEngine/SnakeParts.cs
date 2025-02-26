using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class SnakeHead : MapObject
    {
        public SnakeHead(Point location) : base(location, ObjectType.SnakeHead) { }
    }
    public class SnakeBody : MapObject
    {
        public SnakeBody(Point location) : base(location, ObjectType.SnakeBody) { }
    }
}
