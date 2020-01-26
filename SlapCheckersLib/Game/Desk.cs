using System;
using System.Collections.Generic;
using System.Text;

namespace SlapCheckersLib.Game
{
    class Desk
    {
        private int _size = 8;
        private int[,] _board;
        private History _history;

        public Desk()
        {
            _board = new int[_size, _size];
            _history = new History();
            for (int i = 0; i < _size; i++)
            {
                for(int j = 0; j < _size; j++)
                {
                    if(i < (_size/2 - 1))
                    {
                        _board[i, j] = (i % 2 == 0 ? 0 : 1);
                    }
                    else if (i > (_size / 2 - 1))
                    {
                        _board[i, j] = (i % 2 == 0 ? 0 : 2);
                    }
                }
            }
        }

        private void CleanCell(int x, int y)
        {
            _board[x, y] = 0;
        }
        private void SetInCell(int x, int y)
        {
            _board[x, y] = 1;
        }

        public void MakeMove(Move move)
        {
            var from = move.FromPosition;
            var to = move.ToPosition;
            CleanCell(from.x, from.y);
            SetInCell(to.x, to.y);

            _history.addToHistory(move);
        }

    }
}
