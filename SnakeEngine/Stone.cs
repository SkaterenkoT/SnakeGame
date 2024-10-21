using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Stone : MapObject
    {
        public Stone(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.Stone;
        }
    }
}
