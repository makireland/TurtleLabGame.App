using TurtleLabGame.Domain.Enums;

namespace TurtleLabGame.Domain.Entities
{
    public class Board
    {
        public char[][] board;
        public Turtle Turtle { get; set; }
        int exitX, exitY;
        public List<Tuple<int, int>> mines;
        private IEnumerable<string> moves;

        public Board(string boardFile, string movesFile)
        {
            Turtle = new Turtle();
            BoardGrid(boardFile);
            ReadMoves(movesFile);
        }

        private void BoardGrid(string boardFile)
        {
            string[] lines = File.ReadAllLines(boardFile);
            board = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                board[i] = lines[i].ToCharArray();
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("Current Board:");

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (i == Turtle.PlayerX && j == Turtle.PlayerY)
                    {
                        Console.Write("T");  // Turtle
                    }
                    else
                    {
                        bool isMine = false;
                        foreach (var mine in mines)
                        {
                            if (mine.Item1 == i && mine.Item2 == j)
                            {
                                isMine = true;
                                break;
                            }
                        }

                        if (isMine)
                        {
                            Console.Write("*");  // Mine
                        }
                        else if (i == exitX && j == exitY)
                        {
                            Console.Write("E");  // Exit
                        }
                        else
                        {
                            Console.Write(".");  // Empty tile
                        }
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public bool IsMoveValid()
        {
            if (Turtle.PlayerX < 0 || Turtle.PlayerX >= board.Length || Turtle.PlayerY < 0 || Turtle.PlayerY >= board[0].Length || board[Turtle.PlayerX][Turtle.PlayerY] == '*')
            {
                return false;
            }

            return true;
        }

        public bool IsCloseToMine()
        {
            foreach (var mine in mines)
            {
                int mineX = mine.Item1;
                int mineY = mine.Item2;

                if (Math.Abs(Turtle.PlayerX - mineX) <= 1 && Math.Abs(Turtle.PlayerY - mineY) <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        private void ReadMoves(string movesFile)
        {

            moves = File.ReadLines(movesFile);

            // Set initial turtle position and direction
            Turtle.PlayerX = 1;
            Turtle.PlayerY = 0;
            Turtle.PlayerDirection = Direction.North;

            // Set exit point
            exitX = 2;
            exitY = 4;

            // Initialize mines list
            mines = new List<Tuple<int, int>>();

            // Initialize mines list
            mines = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(3, 3)
            };

            // Print initial board
            PrintBoard();
        }

        public void RunGame()
        {
            
            // Execute moves
            foreach (var move in moves)
            {
                ExecuteMove(move);

                if (!IsMoveValid())
                {
                    Console.WriteLine("Failure");
                    return;
                }

                if (Turtle.PlayerX == exitX && Turtle.PlayerY == exitY)
                {
                    Console.WriteLine("Success");
                    return;
                }

                if (IsCloseToMine())
                {
                    Console.WriteLine("Still in danger!");
                    return;
                }
            }

            Console.WriteLine("Imcomplete");
        }

        private void PrintBoard(char lastMove)
        {
            Console.WriteLine("Current Board:");

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (i == Turtle.PlayerX && j == Turtle.PlayerY)
                    {
                        Console.Write("T");  // Turtle
                    }
                    else
                    {
                        bool isMine = false;
                        foreach (var mine in mines)
                        {
                            if (mine.Item1 == i && mine.Item2 == j)
                            {
                                isMine = true;
                                break;
                            }
                        }

                        if (isMine)
                        {
                            Console.Write("*");  // Mine

                        }
                        else if (i == exitX && j == exitY)
                        {
                            Console.Write("E");  // Exit

                        }
                        else
                        {
                            Console.Write(".");  // Empty tile
                        }
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Last Move: {lastMove}");
            Console.WriteLine();
        }

        public void ExecuteMove(string move)
        {
            foreach (char action in move)
            {
                switch (action)
                {
                    case 'm':
                        Turtle.Move();
                        break;
                    case 'r':
                       Turtle.RotateRight();
                        break;
                    default:
                        throw new ArgumentException($"Invalid action: {action}");
                }

                PrintBoard(action);
            }
        }
    }
}
