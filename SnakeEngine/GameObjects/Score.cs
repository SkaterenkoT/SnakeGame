using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Score
    {
        public int Count { get; private set; }

        public void ClearScore()
        {
            Count = 0;
        }

        public void AddToScore(int number)
        {
            Count += number;
        }
    }
}
