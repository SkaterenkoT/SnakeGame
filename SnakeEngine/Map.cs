using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Map
    {
        public int Width 
        {
            get { return Width; }
            private set
            {
                if (value > 2)
                    Width = value;
            }
        }
        public int Height
        {
            get { return Height; }
            private set
            {
                if (value > 2)
                    Height = value;
            }
        }
        public MapObject[][] mapCells { get; private set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Render(Snake snake, Food food, List<Stone> stones = null)
        {
            List<Point> snakeLocs = snake.GetLocation();

            if (mapCells == null)
            {
                InitializeMap();
                PlaceStones(stones);
            }

            ClearAroundTail(snakeLocs);

            AddSnake(snakeLocs);

            AddFood(food);
        }

        private void InitializeMap()
        {
            mapCells = new MapObject[Width][];
            for (int i = 0; i < Width; i++)
            {
                mapCells[i] = new MapObject[Height];
                for (int j = 0; j < Height; j++)
                    mapCells[i][j] = new Emptyness(new Point(i, j));
            }
        }

        private void PlaceStones(List<Stone> stones)
        {
            if (stones != null)
            {
                foreach (Stone stone in stones)
                {
                    Point loc = stone.Location;
                    mapCells[loc.X][loc.Y] = new Stone(new Point(loc.X,loc.Y));
                }
            }
        }

        private void ClearAroundTail(List<Point> snakeLocs)
        {
            Point tail = snakeLocs[snakeLocs.Count - 1];
            Point bodyPartBeforeTail = null;

            if (snakeLocs.Count > 1)
                bodyPartBeforeTail = snakeLocs[snakeLocs.Count - 2];


            ClearCellIfNotSnakeBody(tail.X - 1, tail.Y, bodyPartBeforeTail);
            ClearCellIfNotSnakeBody(tail.X + 1, tail.Y, bodyPartBeforeTail);
            ClearCellIfNotSnakeBody(tail.X, tail.Y - 1, bodyPartBeforeTail);
            ClearCellIfNotSnakeBody(tail.X, tail.Y + 1, bodyPartBeforeTail);
        }

        private void ClearCellIfNotSnakeBody(int x, int y, Point bodyPartBeforeTail)
        {
            var cell = mapCells[x][y];

            if ((x >= 0) 
                || (x < Width) 
                || (y >= 0) 
                || (y < Height))
            {
                if ((cell.Type != ObjectType.Stone)
                    && (cell.Location != bodyPartBeforeTail))
                {
                    mapCells[x][y] = new Emptyness(new Point(x, y));
                }
            }
        }

        private void AddSnake(List<Point> snakeLocs)
        {
            for (int i = 0; i < snakeLocs.Count; i++)
            {
                Point loc = snakeLocs[i];
                if (i == 0)
                {
                    if ((loc.X <= Width)
                        || (loc.Y <= Height)
                        || (loc.X >= 0)
                        || (loc.Y >= 0))
                        mapCells[loc.X][loc.Y] = new SnakeHead(new Point(loc.X, loc.Y));
                    else
                        return;
                }
                else
                {
                    mapCells[loc.X][loc.Y] = new SnakeBody(new Point(loc.X, loc.Y));
                }
            }
        }

        private void AddFood(Food food)
        {
            Point foodLoc = food.Location;
            mapCells[foodLoc.X][foodLoc.Y] = new Food(new Point(foodLoc.X, foodLoc.Y));
        }
    }
}
