using System;
using System.Collections.Generic;
using System.Text;

namespace SlapCheckersLib.Game
{
    class Functions
    {
        public static (int, int) PositionToInt(string position)
        {
            var result = (x: 0, y: 0);
            result.x = (int)position.ToUpper()[0] - 65;
            Int32.TryParse(position[1].ToString(), out result.y);

            return result;
        }

        public static string IntToPosition((int x, int y) position)
        {
            string result = "";
            result += (char)(position.x + 65);
            result += position.y;
            return result;
        }
    }
}
