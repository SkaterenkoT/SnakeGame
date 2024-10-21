using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Snake
    {
        private SnakeHead snakeHead;
        private LinkedList<SnakeBody> snakeBody;
        public Snake(int x, int y)
        {
            snakeHead = new SnakeHead(x, y);
        }
        public List<(int x, int y)> GetLocation()
        {
            List<(int x, int y)> Location = new List<(int x, int y)>();
            Location.Add((snakeHead.GetLocation()[0], snakeHead.GetLocation()[1]));
            foreach (SnakeBody bodyPart in snakeBody)
            {
                Location.Add((bodyPart.GetLocation()[0], bodyPart.GetLocation()[1]));
            }
            return Location;
        }
        public bool Move(Direction direction, Food food, int maxLocX, int maxLocY, bool borderMirroring, int minLocX = 0, int minLocY = 0, List<Stone> stones = null)
        {
            int[] newHeadLoc;
            switch (direction)
            {
                case Direction.Left:
                    if (snakeHead.GetLocation()[0] == minLocX && borderMirroring)
                    {
                        newHeadLoc = new int[2] { maxLocX, snakeHead.GetLocation()[1] };
                    }
                    else if (snakeHead.GetLocation()[0] != minLocX)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0] - 1, snakeHead.GetLocation()[1] };
                    }
                    else return false;
                    break;
                case Direction.Right:
                    if (snakeHead.GetLocation()[0] == maxLocX && borderMirroring)
                    {
                        newHeadLoc = new int[2] { minLocX, snakeHead.GetLocation()[1] };
                    }
                    else if (snakeHead.GetLocation()[0] != maxLocX)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0] + 1, snakeHead.GetLocation()[1] };
                    }
                    else return false;
                    break;
                case Direction.Up:
                    if (snakeHead.GetLocation()[1] == minLocY && borderMirroring)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0], maxLocY };
                    }
                    else if (snakeHead.GetLocation()[1] != minLocY)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0], snakeHead.GetLocation()[1] - 1 };
                    }
                    else return false;
                    break;
                case Direction.Down:
                    if (snakeHead.GetLocation()[1] == maxLocY && borderMirroring)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0], minLocY };
                    }
                    else if (snakeHead.GetLocation()[1] != maxLocY)
                    {
                        newHeadLoc = new int[2] { snakeHead.GetLocation()[0], snakeHead.GetLocation()[1] + 1 };
                    }
                    else return false;
                    break;
                default:
                    newHeadLoc = new int[2] { 0, 0 };
                    break;
            }
            foreach (Stone barrier in stones)
            {
                if (barrier.GetLocation()[0] == newHeadLoc[0] && barrier.GetLocation()[1] == newHeadLoc[1])
                {
                    return false;
                }
            }
            foreach (SnakeBody barrier in snakeBody)
            {
                if (barrier.GetLocation()[0] == newHeadLoc[0] && barrier.GetLocation()[1] == newHeadLoc[1])
                {
                    return false;
                }
            }
            if (newHeadLoc[0] == food.GetLocation()[0] && newHeadLoc[1] == food.GetLocation()[1])
            {
                food.Effect(snakeBody, snakeHead.GetLocation()[0], snakeHead.GetLocation()[1], ObjectType.SnakeHead);
                snakeHead = new SnakeHead(newHeadLoc[0], newHeadLoc[1]);
                Random random = new Random();
                food = new Food(random.Next(minLocX, maxLocX), random.Next(minLocY, maxLocY));
            }
            else
            {
                if (snakeBody.Count > 0)
                {
                    snakeBody.AddFirst(new SnakeBody(snakeHead.GetLocation()[0], snakeHead.GetLocation()[1]));
                    snakeBody.RemoveLast();
                }
                snakeHead = new SnakeHead(newHeadLoc[0], newHeadLoc[1]);
            }
            return true;
        }
    }
}
