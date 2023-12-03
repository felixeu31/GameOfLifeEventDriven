using System.Collections.ObjectModel;
using FluentAssertions;

namespace GameOfLifeEventDriven
{
    public class GameOfLifeTest
    {
        [Fact]
        public void lonely_cell_dies_by_underpopulation()
        {
            // Arrange
            Game game = new Game();

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells.FirstOrDefault().Value.IsAlive.Should().BeFalse();
        }
    }

    public class Game
    {
        private readonly Dictionary<Position, Cell> _cells;

        public Game()
        {
            _cells = new Dictionary<Position, Cell>
            {
                { new Position(0, 0), new Cell()}
            };
        }

        public void IterateGeneration()
        {
        }

        public ReadOnlyDictionary<Position, Cell> Cells => _cells.AsReadOnly();
    }

    public class Position
    {
        private readonly int _row;
        private readonly int _column;

        public Position(int row, int column)
        {
            _row = row;
            _column = column;
        }
    }

    public class Cell
    {
        private bool _isAlive;

        public Cell()
        {
            _isAlive = true;
        }

        public bool IsAlive => _isAlive;
    }
}