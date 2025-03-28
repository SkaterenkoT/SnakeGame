using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public MapObject[][] MapCells { get; private set; }

        public Map(int width, int height)
        {
            if (width > 2)
                Width = width;
            else
                throw new ArgumentException("Значение ширины должно быть больше 2.");
            if (height > 2)
                Height = height;
            else
                throw new ArgumentException("Значение высоты должно быть больше 2.");
        }

        public void Render(Snake snake, Food food, List<Stone> stones = null) // беда
        {
            if (MapCells == null)
                InitializeMap(stones);
            else
                ClearDynamicObjects();

            MapCells[snake.Head.Location.X][snake.Head.Location.Y] = new SnakeHead(snake.Head.Location);

            foreach (var tail in snake.Body)
                MapCells[tail.Location.X][tail.Location.Y] = new SnakeTail(tail.Location);

            MapCells[food.Location.X][food.Location.Y] = new Food(food.Location);
        }

        private void ClearDynamicObjects()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var type = MapCells[i][j].Type;
                    if (type == ObjectType.SnakeTail
                        || type == ObjectType.SnakeHead
                        || type == ObjectType.Food)
                    { 
                        MapCells[i][j] = new Emptyness(new Point(i, j));
                    }
                }
            }
        }

        public List<Point> GetEmptyCells()
        {
            if (MapCells == null)
                return null;

            var EmptyCells = new List<Point>();

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (MapCells[i][j].Type == ObjectType.Empty)
                        EmptyCells.Add(MapCells[i][j].Location);
                }
            }
            return EmptyCells;
        }

        private void InitializeMap(List<Stone> stones)
        {
            MapCells = new MapObject[Width][];
            for (var i = 0; i < Width; i++)
            {
                MapCells[i] = new MapObject[Height];
                for (var j = 0; j < Height; j++)
                    MapCells[i][j] = new Emptyness(new Point(i, j));
            }
            PlaceStones(stones);
        }

        private void PlaceStones(List<Stone> stones)
        {
            if (stones != null)
            {
                foreach (var stone in stones)
                {
                    Point loc = stone.Location;
                    MapCells[loc.X][loc.Y] = new Stone(new Point(loc.X,loc.Y));
                }
            }
        }
    }
}
