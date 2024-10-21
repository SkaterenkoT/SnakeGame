using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
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
            bool gameContinue = snake.Move(direction, food, map.GetWidth(), map.GetHeight(), borderMirroring, stones: this.stones);
            map.Render(snake, food, stones);
            return gameContinue;
        }
        public void SetDisplayCallback(Func<Map> _displayCallback)
        {
            displayCallback = _displayCallback;
        }
    }
}
