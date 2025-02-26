using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEngine
{
    public class Score
    {
        public int count { get; private set; }
        public void ClearScore()
        {
            count = 0;
        }
        public void AddToScore(int number)
        {
            count += number;
        }
    }
}
