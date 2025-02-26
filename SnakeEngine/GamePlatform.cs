using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class GamePlatform
    {
        public Snake snake { get; private set; }
        public Food food { get; private set; }
        public List<Stone> stones { get; }
        public Map map { get; }
        public Score score { get; }
        public GameSettings gameSettings { get; }
        public Func<Map> displayCallback { get; private set; }

        public GamePlatform(Score _score, GameSettings _gameSettings)
        {
            score = _score;
            map = new Map(gameSettings.width, gameSettings.height);
            snake = new Snake(new Point(gameSettings.width / 2 - 1, gameSettings.height / 2 - 1));
            food = new Food(new Point(gameSettings.width / 2 + 1, gameSettings.height / 2 + 1));
            gameSettings = _gameSettings;
        }
        public bool ProcessGameTurn(Direction direction = Direction.Right)
        {
            if (displayCallback == null)
                return false;

            snake.Move(direction);

            if ((!BorderBlocksTheRoad(direction))
                && (!StoneBlocksTheRoad())
                && (!BodyBlocksTheRoad()))
            {
                score.ClearScore();
                return false;
            }

            if (snake.Head.Location == food.Location)    
            { 
                StepOnFood();
                score.AddToScore(1);
            }

            map.Render(snake, food, stones);

            return true;
        }
        private bool BorderBlocksTheRoad(Direction direction)
        {
          
            if ((snake.Head.Location.X < 0)
               || (snake.Head.Location.Y < 0)
               || (snake.Head.Location.X > gameSettings.width)
               || (snake.Head.Location.Y > gameSettings.height))
            {
                if (gameSettings.borderMirroring)
                {
                    MirrorLocation(direction);
                    return false;
                }
                else
                    return true;
            }
            return false;
        }
        private bool StoneBlocksTheRoad()
        {
            Point headLoc = snake.Head.Location;

            if (map.mapCells[headLoc.X][headLoc.Y].Type == ObjectType.Stone)
            {
                return true;
            }
            return false;
        }
        private void MirrorLocation(Direction direction)
        {
            Point prevHeadLoc = snake.Head.Location;
            var snakeBody = snake.Body;

            switch (direction)
            {
                case Direction.Up:
                    snake = new Snake(new SnakeHead(new Point(prevHeadLoc.X, gameSettings.height - 1)), snakeBody);
                    break;
                case Direction.Down:
                    snake = new Snake(new SnakeHead(new Point(prevHeadLoc.X, 0)), snakeBody);
                    break;
                case Direction.Right:
                    snake = new Snake(new SnakeHead(new Point(0, prevHeadLoc.Y)), snakeBody);
                    break;
                case Direction.Left:
                    snake = new Snake(new SnakeHead(new Point(gameSettings.width - 1, prevHeadLoc.Y)), snakeBody);
                    break;
            }
        }
        private bool BodyBlocksTheRoad()
        {
            List<Point> snakePartsLocs = snake.GetLocation();

            for (var i = 1; i < snakePartsLocs.Count; i++)
            {
                if (snake.Head.Location == snakePartsLocs[i])
                    return true;
            }
            return false;
        }
        private void StepOnFood()
        {
            food.Effect(snake);

            var variants = new List<Point>();

            for (var i = 0; i < gameSettings.width; i++)
            {
                for (var j = 0; j < gameSettings.height; j++)
                {
                    if (map.mapCells[i][j].Type == ObjectType.Empty)
                        variants.Add(new Point(i, j));
                }
            }
            Random rand = new Random();
            int variantPeek = rand.Next(0, variants.Count - 1);
            Point newFoodLoc = variants[variantPeek];

            food = new Food(newFoodLoc);
        }
        public void SetDisplayCallback(Func<Map> _displayCallback)
        {
            displayCallback = _displayCallback;
        }
    }
}
