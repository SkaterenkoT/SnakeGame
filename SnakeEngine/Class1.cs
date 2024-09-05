using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public enum Type
    {
        Empty,
        SnakeHead, 
        SnakeBody,
        Stone,
        Food,
    }
    public enum Direction
    {
        Up,
        Left,
        Down,
        Right,
    }

    public class MapObject
    {
        protected int[] Location = new int[2];
        protected Type type;
        public int[] GetLocation()
        {
            return Location;
        }
    }
    public class SnakeHead : MapObject
    {
        public SnakeHead(int x, int y)
        {
            Location = new int[2] { x, y };
            type = Type.SnakeHead;
        }
    }
    public class SnakeBody : MapObject
    {
        public SnakeBody(int x, int y)
        {
            Location = new int[2] { x, y };
            type = Type.SnakeBody;
        }
    }
    public class Food : MapObject
    {
        public Food(int x, int y)
        {
            Location = new int[2] { x, y };
            type = Type.Food;
        }
        public virtual void Effect(MapObject mapObject)
        {

        }
    }
    public class Stone : MapObject
    {
        public Stone(int x, int y)
        {
            Location = new int[2] { x, y };
            type = Type.Stone;
        }
    }
    public class Fruit : Food
    {
        public Fruit(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = Type.Food;
        }
        public override void Effect(MapObject mapObject)
        {

        }
    }
    public class Snake
    {
        private SnakeHead snakeHead;
        private List<SnakeBody> snakeBody;
        public Snake(int x, int y)
        {
            snakeHead = new SnakeHead(x, y);
        }
        public List<(int x, int y)> GetLocation()
        {
            List<(int x, int y)> Location = new List<(int x, int y)>();
            Location.Add((snakeHead.GetLocation()[0], snakeHead.GetLocation()[1]));
            for (int i = 0; i < snakeBody.Count; i++)
            {
                Location.Add((snakeBody[i].GetLocation()[0], snakeBody[i].GetLocation()[0]));
            }
            return Location;
        }
        public bool Move(Direction direction, Food food, int maxLocX, int maxLocY, List<Stone> stones = null)
        {
            if (snakeHead.GetLocation()[0] == food.GetLocation()[0] && snakeHead.GetLocation()[1] == food.GetLocation()[1])
            {
                snakeBody.Add(new SnakeBody(snakeHead.GetLocation()[0], snakeHead.GetLocation()[1]));
            }
        }
    }
}
