using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Food : MapObject
    {
        public Food(Point location) : base(location, ObjectType.Food) { }
        public virtual void Effect(Snake snake)
        {
            List<Point> snakePartsLocs = snake.GetLocation();

            var newPart = new SnakeBody(new Point(snakePartsLocs[snakePartsLocs.Count - 1].X
                                                    , snakePartsLocs[snakePartsLocs.Count - 1].Y));

            var (head, body) = (snake.Head, snake.Body);
            body.AddLast(newPart);

            snake = new Snake(head, body);
        }
    }
}
