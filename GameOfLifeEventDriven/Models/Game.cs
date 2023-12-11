
using System.Collections.ObjectModel;
using GameOfLifeEventDriven.Events;
using GameOfLifeEventDriven.Helpers;

namespace GameOfLifeEventDriven.Models;

public class Game : INotificationHandler<NextCellStateCalculatedEvent>
{
    private readonly List<Cell> _cells = new();
    private readonly Mediator _mediator;
    private Dictionary<Position, CellState> _nextCellsStates = new();


    public Game(int rows, int columns, List<Position> livingPositions)
    {
        _mediator = new Mediator();

        _mediator.Subscribe<NextCellStateCalculatedEvent>(this);

        BuildGameCells(rows, columns, livingPositions);

        PublishInitialCellStates();
    }

    private void BuildGameCells(int rows, int columns, List<Position> livingPositions)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                var position = new Position(row, column);
                Cell cell = livingPositions.Contains(position)
                    ? Cell.LiveCell(position, _mediator)
                    : Cell.DeadCell(position, _mediator);
                _mediator.Subscribe<IterationStartedEvent>(cell);
                _mediator.Subscribe<CellStateChangedEvent>(cell);
                _cells.Add(cell);
            }
        }
    }

    private void PublishInitialCellStates()
    {
        foreach (var cell in _cells)
        {
            _mediator.Publish(new CellStateChangedEvent(cell.Position, cell.CellState));
        }
    }

    public void IterateGeneration()
    {
        _mediator.Publish(new IterationStartedEvent());
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.ToDictionary(x => x.Position, x => x).AsReadOnly();


    public void Handle(NextCellStateCalculatedEvent notification)
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
            _mediator.Publish(new CellStateChangedEvent(cell.Key, cell.Value));
        }
        _nextCellsStates = new();
    }

    private bool AllCellsCalculatedItsNextState()
    {
        return _nextCellsStates.Count == _cells.Count;
    }
}