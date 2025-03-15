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
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public List<Stone> Stones { get; }
        public Map Map { get; }
        public Score Score { get; }
        public GameSettings GameSettings { get; }
        public Func<Map> DisplayCallback { get; private set; }

        public GamePlatform(Score score, GameSettings gameSettings)
        {
            Score = score;
            Map = new Map(gameSettings.Width, gameSettings.Height);
            Snake = new Snake(new Point(gameSettings.Width / 2 - 1, gameSettings.Height / 2 - 1));
            Food = new Food(new Point(gameSettings.Width / 2 + 1, gameSettings.Height / 2 + 1));
            GameSettings = gameSettings;
        }

        public bool ProcessGameTurn(Direction direction = Direction.Right)
        {
            if (DisplayCallback == null)
                return false;

            if (GameOver(direction))
                return false;

            Snake.Move(direction, GameSettings);

            if (Snake.Head.Location == Food.Location)    
            {
                Food.Effect(Snake, Map.GetEmptyCells());
                Score.AddToScore(1);
            }

            Map.Render(Snake, Food, Stones);

            return true;
        }

        private bool GameOver(Direction direction)
        {
            Point newHeadLocation = Snake.Head.Location.GetNeighbor(direction);

            if (BorderBlocksTheRoad(newHeadLocation))
            {
                Score.ClearScore();
                return true; 
            }

            var lastTailPiece = Snake.Body.Last.Value;

            if (Map.MapCells[newHeadLocation.X][newHeadLocation.Y].Type == ObjectType.Stone
                || (Map.MapCells[newHeadLocation.X][newHeadLocation.Y].Type == ObjectType.SnakeTail
                    && newHeadLocation != lastTailPiece.Location))
            {
                Score.ClearScore();
                return true; 
            }

            return false;
        }

        private bool BorderBlocksTheRoad(Point headLocation)
        {
            if (GameSettings.BorderMirroring)
                return false;

            if ((headLocation.X < 0)
               || (headLocation.Y < 0)
               || (headLocation.X > GameSettings.Width)
               || (headLocation.Y > GameSettings.Height))
            {
                    return true;
            }

            return false;
        }

        public void SetDisplayCallback(Func<Map> _displayCallback)
        {
            DisplayCallback = _displayCallback;
        }
    }
}
