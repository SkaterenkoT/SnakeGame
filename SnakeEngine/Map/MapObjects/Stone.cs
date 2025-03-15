using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Stone : MapObject
    {
        public Stone(Point location) : base(location, ObjectType.Stone) { }
    }
}
