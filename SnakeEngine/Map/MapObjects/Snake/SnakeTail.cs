using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class SnakeTail : MapObject
    {
        public SnakeTail(Point location) : base(location, ObjectType.SnakeTail) { }
    }
}
