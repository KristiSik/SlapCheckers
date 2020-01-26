namespace SlapCheckersLib.Game
{
    public class Move
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
