using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfLifeEventDriven;

public class Game
{
    private readonly List<Cell> _cells = new ();
    private readonly Mediator _mediator;


    public Game(int rows, int columns, List<Position> livingPositions)
    {
        _mediator = new Mediator();

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                var position = new Position(row, column);
                Cell cell = livingPositions.Contains(position) ? Cell.LiveCell(position) : Cell.DeadCell(position);
                _mediator.Subscribe<IterationStarted>(cell);
                _mediator.Subscribe<NewNeighbour>(cell);
                _mediator.Publish(new NewNeighbour(position, cell));
                _cells.Add(cell);
            }
        }
    }

    public void IterateGeneration()
    {
        _mediator.Publish(new IterationStarted());
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.ToDictionary(x => x.Position, x => x).AsReadOnly();

    public record IterationStarted;

    public record NewNeighbour(Position Position, Cell Cell);
}