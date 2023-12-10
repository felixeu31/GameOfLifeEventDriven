using System.Collections.ObjectModel;

namespace GameOfLifeEventDriven;

public class Game
{
    private readonly Dictionary<Position, Cell> _cells;
    private readonly Mediator _mediator;


    public Game()
    {
        _mediator = new Mediator();

        var cell = new Cell();
        _mediator.Subscribe(cell);

        _cells = new Dictionary<Position, Cell>
        {
            { new Position(0, 0), cell }
        };
    }

    public void IterateGeneration()
    {
        _mediator.Publish(new IterationStarted());
    }

    public ReadOnlyDictionary<Position, Cell> Cells => _cells.AsReadOnly();

    public class IterationStarted
    {

    }
}