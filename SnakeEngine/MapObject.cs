using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public abstract class MapObject
    {
        public Point Location { get; }
        public ObjectType Type { get; }
        public MapObject(Point location, ObjectType type)
        {
            Location = location;
            Type = type;
        }
    }
}
