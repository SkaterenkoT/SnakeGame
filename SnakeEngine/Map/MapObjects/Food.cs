﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Food : MapObject
    {
        public Food(Point location) : base(location, ObjectType.Food) { }

        public virtual void Effect(Snake snake, List<Point> emptyCells)
        {
            snake.AddTail(this.Location);

            var random = new Random();
            var cellPeek = random.Next(0, emptyCells.Count - 1);
            var newFoodLocation = emptyCells[cellPeek];

            this.Location = newFoodLocation;
        }
    }
}
