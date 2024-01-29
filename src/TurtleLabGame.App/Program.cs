using TurtleLabGame.Domain.Entities;

string boardFilePath = @"files\board.txt"; // Replace with the actual path to the board file
string movesFilePath = @"files\move.txt"; // Replace with the actual path to the moves file



// Combine the base directory with the relative path
string fullPathBoard = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, boardFilePath);
string fullPathMove = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, movesFilePath);


var board = new Board(fullPathBoard, fullPathMove);

board.RunGame();

Console.ReadLine(); // To keep the console window open