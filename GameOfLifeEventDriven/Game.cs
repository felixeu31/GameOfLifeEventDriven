using System.Collections.ObjectModel;

namespace GameOfLifeEventDriven;

public class Game
{
    private readonly Dictionary<Position, Cell> _cells;

    public event EventHandler<IterationEventArgs>? RaiseIterationEvent;

    public Game()
    {
        _cells = new Dictionary<Position, Cell>
        {
            { new Position(0, 0), new Cell(this) }
        };
    }

    public void IterateGeneration()
    {
        RaiseIterationEvent(this, new IterationEventArgs(0));
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.AsReadOnly();
}