using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
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
}
