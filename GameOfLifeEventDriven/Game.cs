using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfLifeEventDriven;

public class Game : INotificationHandler<Cell.CellNextState>
{
    private readonly List<Cell> _cells = new ();
    private readonly Mediator _mediator;
    private Dictionary<Position, bool> _nextCellsStates;


    public Game(int rows, int columns, List<Position> livingPositions)
    {
        _mediator = new Mediator();

        _mediator.Subscribe<Cell.CellNextState>(this);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                var position = new Position(row, column);
                Cell cell = livingPositions.Contains(position) ? 
                    Cell.LiveCell(position, _mediator) 
                    : Cell.DeadCell(position, _mediator);
                _mediator.Subscribe<IterationStarted>(cell);
                _mediator.Subscribe<CellChange>(cell);
                _cells.Add(cell);
            }
        }

        foreach (var cell in _cells)
        {
            _mediator.Publish(new CellChange(cell.Position, cell.IsAlive));
        }
    }

    public void IterateGeneration()
    {
        _nextCellsStates = new ();
        _mediator.Publish(new IterationStarted());
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.ToDictionary(x => x.Position, x => x).AsReadOnly();

    public record IterationStarted;

    public record CellChange(Position Position, bool IsAlive);

    public void Handle(Cell.CellNextState notification)
    {
        _nextCellsStates[notification.Position] = notification.IsAlive;

        if (_nextCellsStates.Count != _cells.Count) return;

        foreach (var cell in _nextCellsStates)
        {
            _mediator.Publish(new CellChange(cell.Key, cell.Value));
        }
    }
}