using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    class Emptyness : MapObject
    {
        public Emptyness(Point location) : base(location, ObjectType.Empty) { }
    }
}
