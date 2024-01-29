using TurtleLabGame.Domain.Enums;

namespace TurtleLabGame.Domain.Entities
{
    public abstract class Player
    {       
        public Direction PlayerDirection { get; set;}
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
       

        public Player()
        {
            
        }

        public virtual void Move()
        {
            switch (PlayerDirection)
            {
                case Direction.North:
                    PlayerX--;
                    break;
                case Direction.East:
                    PlayerY++;
                    break;
                case Direction.South:
                    PlayerX++;
                    break;
                case Direction.West:
                    PlayerY--;
                    break;
            }
        }

        public virtual void RotateRight()
        {
            PlayerDirection = (Direction)(((int)PlayerDirection + 1) % 4);
        }
    }
}