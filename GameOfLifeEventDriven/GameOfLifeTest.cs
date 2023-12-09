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
}