using System;

namespace Core.Utilities.FindeksAdapter
{
    public class FindexAdapter
    {
        public static int CalculateFindeksScore()
        {
            Random random = new Random();
            return random.Next(0, 1900);
        }
    }
}