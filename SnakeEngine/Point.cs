﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
        public Point GetNeighbor(Direction dir)
        {
            switch (dir)
            {
                case Direction.Down:
                    return new Point(X, Y + 1);
                case Direction.Up:
                    return new Point(X, Y - 1);
                case Direction.Left:
                    return new Point(X - 1, Y);
                case Direction.Right:
                    return new Point(X + 1, Y);
                default:
                    return new Point(X, Y);
            }
        }
    }
}
