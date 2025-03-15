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
            var oldHeadLocation = Head.Location;

            Head = new SnakeHead(GetNewHeadLocation(direction, gameSettings));

            if (Body.Any())
            {
                if (Body.First.Value.Location != oldHeadLocation)
                {
                    Body.RemoveLast();
                    Body.AddFirst(new SnakeTail(oldHeadLocation));
                }
            }
        }

        private Point GetNewHeadLocation(Direction direction, GameSettings gameSettings)
        {
            Point possibleNewLocation = Head.Location.GetNeighbor(direction);

            if (possibleNewLocation.X < 0
            || possibleNewLocation.X > gameSettings.Width
            || possibleNewLocation.Y < 0
            || possibleNewLocation.Y > gameSettings.Height)
            {
                if (gameSettings.BorderMirroring)
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
                else
                    return Head.Location;
            }
            else
                return possibleNewLocation;
        }

    }
}
