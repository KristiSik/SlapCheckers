using System;
using System.Collections.Generic;
using System.Text;

namespace SlapCheckersLib.Game
{
    class Move
    {
        public int userId { get; set; }
        public string fromPosition { get; set; }
        public string toPosition { get; set; }
        public Move(int userId, string fromPosition, string toPosition)
        {
            this.userId = userId;
            this.fromPosition = fromPosition;
            this.toPosition = toPosition;
        }   
    }
}
