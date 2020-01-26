using System;
using System.Collections.Generic;
using System.Text;

namespace SlapCheckersLib.Game
{
    class History
    {
        private List<Move> _moves;
        public History()
        {
            _moves = new List<Move>();
        }

        public void addToHistory(Move move)
        {
            _moves.Add(move);
        }
    }
}
