using Moq;
using TurtleLabGame.Domain.Entities;

namespace TurtleLabGame.UnitTesting
{
    [TestFixture]
    public class BoardTests
    {
        private const string BoardFilePath = @"D:\www\TurtleLabGame.App\src\TurtleLabGame.App\files\board.txt";
        private const string MovesFilePath = @"D:\www\TurtleLabGame.App\src\TurtleLabGame.App\files\move.txt";

        [Test]
        public void BoardInitialization_ValidFiles_InitializeBoard()
        {
            // Arrange & Act
            var board = new Board(BoardFilePath, MovesFilePath);

            // Assert
            Assert.NotNull(board);
            Assert.NotNull(board.Turtle);
            // Add more assertions based on your specific initialization logic
        }

        [Test]
        public void ExecuteMove_ValidMoves_SuccessfullyMovesTurtle()
        {
            // Arrange
            var board = new Board(BoardFilePath, MovesFilePath);
            var turtleMock = Mock.Of<Turtle>();
            board.Turtle = turtleMock;

            // Act
            board.ExecuteMove("m");

            // Assert
            Mock.Get(turtleMock).Verify(x => x.Move(), Times.Once);
            Mock.Get(turtleMock).Verify(x => x.RotateRight(), Times.Never);
        }

        [Test]
        public void ExecuteRotate_ValidMoves_SuccessfullyMovesTurtle()
        {
            // Arrange
            var board = new Board(BoardFilePath, MovesFilePath);
            var turtleMock = Mock.Of<Turtle>();
            board.Turtle = turtleMock;

            // Act
            board.ExecuteMove("r");

            // Assert
            Mock.Get(turtleMock).Verify(x => x.RotateRight(), Times.Once);
            Mock.Get(turtleMock).Verify(x => x.Move(), Times.Never);
        }

        // Add more tests for other methods as needed

        [Test]
        public void IsMoveValid_TurtleOnMine_ReturnsTrue()
        {
            // Arrange
            var board = new Board(BoardFilePath, MovesFilePath);

            // Manually place turtle on a mine
            board.Turtle.PlayerX = 1;
            board.Turtle.PlayerY = 1;

            // Act & Assert
            Assert.IsTrue(board.IsMoveValid());
        }

        [Test]
        public void IsCloseToMine_TurtleCloseToMine_ReturnsTrue()
        {
            // Arrange
            var board = new Board(BoardFilePath, MovesFilePath);   
            board.mines = new List<Tuple<int, int>> { new Tuple<int, int>(1, 2) };

            // Act
            board.RunGame();

            //Assert
            Assert.IsTrue(board.IsCloseToMine());
        }
    }
}
