using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Food : MapObject
    {
        public Food(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.Food;
        }
        public virtual void Effect(LinkedList<SnakeBody> mapObject, int x, int y, ObjectType type)
        {
            SnakeBody newPart = new SnakeBody(x, y);
            newPart.SetObjectType(type);
            mapObject.AddFirst(newPart);
        }
    }
}
