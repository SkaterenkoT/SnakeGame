using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Snake
    {
        public SnakeHead Head { get; private set; }
        public LinkedList<SnakeBody> Body { get; private set; }
        public Snake(Point location)
        {
            Head = new SnakeHead(location);
        }
        public Snake(SnakeHead head, LinkedList<SnakeBody> body)
        {
            Head = head;
            Body = body;
        }
        public List<Point> GetLocation()
        {
            var Location = new List<Point>();
            Location.Add(new Point(Head.Location.X, Head.Location.Y));
            foreach (SnakeBody bodyPart in Body)
            {
                Location.Add(new Point(bodyPart.Location.X, bodyPart.Location.Y));
            }
            return Location;
        }
        public void Move(Direction direction)
        {
            Point prevHeadLoc = Head.Location;
            Head = new SnakeHead(prevHeadLoc.GetNeighbor(direction));

            if (Body.Any())
            {
                Body.RemoveLast();
                Body.AddFirst(new SnakeBody(prevHeadLoc));
            }
        }
    }
}
