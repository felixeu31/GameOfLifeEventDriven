using FluentAssertions;

namespace GameOfLifeEventDriven
{
    public class GameOfLifeTest
    {
        [Fact]
        public void lonely_cell_dies_by_underpopulation()
        {
            // Arrange
            Game game = new Game(1, 1, new List<Position>());

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Dead);
        }


        [Fact]
        public void live_cell_with_two_living_neighbours_lives()
        {
            // Arrange
            Game game = new Game(2,2, 
                new List<Position>
                {
                    new (0, 0),
                    new (1, 0),
                    new (0, 1)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Alive);
        }

        [Fact]
        public void last_introduced_live_cell_with_two_living_neighbours_lives()
        {
            // Arrange
            Game game = new Game(2, 2,
                new List<Position>
                {
                    new (0, 0),
                    new (1, 0),
                    new (0, 1)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 1)].CellState.Should().Be(CellState.Alive);
        }

        [Fact]
        public void live_cell_with_three_neighbours_lives()
        {
            // Arrange
            Game game = new Game(2, 2,
                new List<Position>
                {
                    new (0, 0),
                    new (1, 0),
                    new (0, 1),
                    new (1, 1)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Alive);
        }

        [Fact] public void live_cell_with_more_than_three_living_neighbours_dies_by_over_population()
        {
            // Arrange
            Game game = new Game(3, 3,
                new List<Position>
                {
                    new (0, 1),
                    new (0, 2),
                    new (1, 0),
                    new (1, 1),
                    new (1, 2)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0,1)].CellState.Should().Be(CellState.Dead);
        }

        [Fact]
        public void dead_cell_with_three_living_neighbours_becomes_live_by_reproduction()
        {
            // Arrange
            Game game = new Game(2, 2,
                new List<Position>
                {
                    new (1, 0),
                    new (0, 1),
                    new (1, 1)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Alive);
        }

        [Fact]
        public void live_cell_with_three_living_neighbour_but_other_living_cells_lives_because_is_only_affected_by_neighbours()
        {
            // Arrange
            Game game = new Game(3, 3,
                new List<Position>
                {
                    new (0, 0),
                    new (0, 1),
                    new (0, 2),
                    new (1, 0),
                    new (1, 1),
                    new (1, 2)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Alive);
        }


        [Fact]
        public void dead_cell_with_two_living_neighbours_remains_dead()
        {
            // Arrange
            Game game = new Game(2, 2,
                new List<Position>
                {
                    new (1, 0),
                    new (0, 1)
                });

            // Act
            game.IterateGeneration();

            // Assert
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Dead);
        }

        [Fact]
        public void cells_progress_across_multiple_generations()
        {
            // Arrange
            Game game = new Game(3, 3,
                new List<Position>
                {
                    new (0, 0),
                    new (0, 1),
                    new (1, 1),
                    new (1, 2),
                    new (2, 1)
                });

            // Act, assert
            game.IterateGeneration();
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(0, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(0, 2)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(1, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(1, 1)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(1, 2)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(2, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(2, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(2, 2)].CellState.Should().Be(CellState.Alive);

            game.IterateGeneration();
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(0, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(0, 2)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(1, 0)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(1, 1)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(1, 2)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(2, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(2, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(2, 2)].CellState.Should().Be(CellState.Alive);

            game.IterateGeneration();
            game.Cells[new Position(0, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(0, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(0, 2)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(1, 0)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(1, 1)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(1, 2)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(2, 0)].CellState.Should().Be(CellState.Dead);
            game.Cells[new Position(2, 1)].CellState.Should().Be(CellState.Alive);
            game.Cells[new Position(2, 2)].CellState.Should().Be(CellState.Dead);
        }
    }
}