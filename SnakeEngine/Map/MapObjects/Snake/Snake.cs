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
        public LinkedList<SnakeTail> Body { get; private set; }

        public Snake(Point location)
        {
            Head = new SnakeHead(location);
        }

        public Snake(SnakeHead head, LinkedList<SnakeTail> body)
        {
            Head = head;
            Body = body;
        }

        public void AddTail(Point location)
        {
            Body.AddFirst(new SnakeTail(location));
        }

        public void Move(Direction direction, GameSettings gameSettings)
        {
            Point oldHeadLocation = Head.Location;
            Point possibleNewLocation = oldHeadLocation.GetNeighbor(direction);


            if (gameSettings.BorderMirroring
                && (possibleNewLocation.X < 0
                    || possibleNewLocation.X > gameSettings.Width
                    || possibleNewLocation.Y < 0
                    || possibleNewLocation.Y > gameSettings.Height))
                Head = new SnakeHead(MirrorLocation(direction, gameSettings));
            else
                Head = new SnakeHead(oldHeadLocation.GetNeighbor(direction));


            if (Body.Any())
            {
                if (Body.First.Value.Location != oldHeadLocation)
                {
                    Body.RemoveLast();
                    Body.AddFirst(new SnakeTail(oldHeadLocation));
                }
            }
        }

        private Point MirrorLocation(Direction direction, GameSettings gameSettings)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(Head.Location.X, gameSettings.Height - 1);
                case Direction.Down:
                    return new Point(Head.Location.X, 0);
                case Direction.Right:
                    return new Point(0, Head.Location.Y);
                case Direction.Left:
                    return new Point(gameSettings.Width - 1, Head.Location.Y);
                default:
                    return null;
            }
        }

    }
}
