using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
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
}
