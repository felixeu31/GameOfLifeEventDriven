using System.Collections.ObjectModel;
using System.Linq;

namespace GameOfLifeEventDriven;

public class Game : INotificationHandler<Cell.CellNextState>
{
    private readonly List<Cell> _cells = new ();
    private readonly Mediator _mediator;
    private Dictionary<Position, CellState> _nextCellsStates = new();


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
                _mediator.Subscribe<CellStateChanged>(cell);
                _cells.Add(cell);
            }
        }

        foreach (var cell in _cells)
        {
            _mediator.Publish(new CellStateChanged(cell.Position, cell.CellState));
        }
    }

    public void IterateGeneration()
    {
        _mediator.Publish(new IterationStarted());
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.ToDictionary(x => x.Position, x => x).AsReadOnly();

    public record IterationStarted;

    public record CellStateChanged(Position Position, CellState CellState);

    public void Handle(Cell.CellNextState notification)
    {
        _nextCellsStates[notification.Position] = notification.CellState;

        if (AllCellsCalculatedItsNextState())
        {
            FinishIteration();
        }
    }

    private void FinishIteration()
    {
        foreach (var cell in _nextCellsStates)
        {
            _mediator.Publish(new CellStateChanged(cell.Key, cell.Value));
        }
        _nextCellsStates = new();
    }

    private bool AllCellsCalculatedItsNextState()
    {
        return _nextCellsStates.Count == _cells.Count;
    }
}