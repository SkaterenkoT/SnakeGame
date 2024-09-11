using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public enum ObjectType
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
    public class SnakeHead : MapObject
    {
        public SnakeHead(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.SnakeHead;
        }
    }
    public class SnakeBody : MapObject
    {
        public SnakeBody(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.SnakeBody;
        }
    }
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
    public class Stone : MapObject
    {
        public Stone(int x, int y) : base(x, y)
        {
            Location = new int[2] { x, y };
            type = ObjectType.Stone;
        }
    }
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
    public class Map
    {
        private int width;
        private int height;
        private MapObject[][] mapCells;
        public Map(int _width, int _height)
        {
            height = _height >= 5 ? _height : 8;
            width = _width >= 5 ? _width : 8;
        }
        public void Render(Snake snake, Food food, List<Stone> stones = null)
        {
            List<(int x, int y)> snakeLocs = snake.GetLocation();
            if (mapCells != null)
            {
                for (int i = snakeLocs.Count - 1; i >= 0; i--)
                {
                    if (i == snakeLocs.Count - 1)
                    {
                        if (snakeLocs[i].x > 0)
                        {
                            if (mapCells[snakeLocs[i].x - 1][snakeLocs[i].y].GetObjectType() != ObjectType.Stone)
                                mapCells[snakeLocs[i].x - 1][snakeLocs[i].y].SetObjectType(ObjectType.Empty);
                        }
                        if (snakeLocs[i].x < width)
                        {
                            if (mapCells[snakeLocs[i].x + 1][snakeLocs[i].y].GetObjectType() != ObjectType.Stone)
                                mapCells[snakeLocs[i].x + 1][snakeLocs[i].y].SetObjectType(ObjectType.Empty);
                        }
                        if (snakeLocs[i].y > 0)
                        {
                            if (mapCells[snakeLocs[i].x][snakeLocs[i].y - 1].GetObjectType() != ObjectType.Stone)
                                mapCells[snakeLocs[i].x][snakeLocs[i].y - 1].SetObjectType(ObjectType.Empty);
                        }
                        if (snakeLocs[i].y < height)
                        {
                            if (mapCells[snakeLocs[i].x][snakeLocs[i].y + 1].GetObjectType() != ObjectType.Stone)
                                mapCells[snakeLocs[i].x][snakeLocs[i].y + 1].SetObjectType(ObjectType.Empty);
                        }
                    }
                    mapCells[snakeLocs[i].x][snakeLocs[i].y].SetObjectType(ObjectType.SnakeBody);
                    if (i == 0)
                        mapCells[snakeLocs[i].x][snakeLocs[i].y].SetObjectType(ObjectType.SnakeHead);
                }
                mapCells[food.GetLocation()[0]][food.GetLocation()[1]].SetObjectType(ObjectType.Food);
            }
            else
            {
                mapCells = new MapObject[width][];
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                    {
                        mapCells[i][j] = new MapObject(i, j);
                        mapCells[i][j].SetObjectType(ObjectType.Empty);
                    }
                if (stones != null)
                    foreach (Stone stone in stones)
                        mapCells[stone.GetLocation()[0]][stone.GetLocation()[1]].SetObjectType(ObjectType.Stone);
                for (int i = 0; i < snakeLocs.Count; i++)
                    if (i == 0)
                        mapCells[snakeLocs[i].x][snakeLocs[i].y].SetObjectType(ObjectType.SnakeHead);
                    else
                        mapCells[snakeLocs[i].x][snakeLocs[i].y].SetObjectType(ObjectType.SnakeBody);
            }
        }
        public int GetWidth()
        {
            return width;
        }
        public int GetHeight() 
        {
            return height;
        }
    }
    public class GameSettings
    {
        public bool bordermirroring;
        public int stonesCount;
        public int width;
        public int height;
        public int speed;
        public GameSettings(bool _borderMirroring, int _stonesCount, int _width, int _height, int _speed)
        {
            bordermirroring = _borderMirroring;
            stonesCount = _stonesCount;
            width = _width;
            height = _height;
            speed = _speed;
        }
    }
    public class Score
    {
        private int numeric;
        public int GetScore()
        {
            return numeric;
        }
        public void AddToScore(int number)
        {
            numeric += number;
        }
    }
    public class GamePlatform
    {
        private Snake snake;
        private Food food;
        private List<Stone> stones;
        private Map map;
        private Score score;
        private bool borderMirroring;
        private int stonesCount;
        private Func<Map> displayCallback;
        private int speed;
        public GamePlatform(Score _score, GameSettings gameSettings)
        {
            score = _score;
            map = new Map(gameSettings.width, gameSettings.height);
            borderMirroring = gameSettings.bordermirroring;
            speed = gameSettings.speed;
            snake = new Snake(gameSettings.width / 2 - 1, gameSettings.height / 2 - 1);
            food = new Food(gameSettings.width / 2 + 1, gameSettings.height / 2 + 1);
            stonesCount = gameSettings.stonesCount;
        }
        public bool ProcessGameTurn(Direction direction)
        {
            bool gameContinue = snake.Move(direction, food, map.GetWidth(), map.GetHeight(), borderMirroring, stones : this.stones);
            map.Render(snake, food, stones);
            return gameContinue;
        }
        public void SetDisplayCallback(Func<Map> _displayCallback)
        {
            this.displayCallback = _displayCallback;
        }
    }
}
