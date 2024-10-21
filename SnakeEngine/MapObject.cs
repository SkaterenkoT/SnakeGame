using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class MapObject
    {
        protected int[] Location = new int[2];
        protected ObjectType type;
        public MapObject(int x, int y)
        {
            Location = new int[2] { x, y };
        }
        public int[] GetLocation()
        {
            return Location;
        }
        public ObjectType GetObjectType()
        {
            return type;
        }
        public virtual void SetObjectType(ObjectType type)
        {
            this.type = type;
        }
    }
}
