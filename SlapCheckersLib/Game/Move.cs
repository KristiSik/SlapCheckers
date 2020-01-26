using System;
using System.Collections.Generic;
using System.Text;

namespace SlapCheckersLib.Game
{
    class Move
    {
        public int UserId { get; set; }
        public (int x, int y) FromPosition { get; set; }
        public (int x, int y) ToPosition { get; set; }
        public Move(int userId, (int x, int y) fromPosition, (int x, int y) toPosition)
        {
            this.UserId = userId;
            this.FromPosition = fromPosition;
            this.ToPosition = toPosition;
        }   
    }
}
