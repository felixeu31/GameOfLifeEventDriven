namespace GameOfLifeEventDriven;

public class Cell : INotificationHandler<IterationStarted>, INotificationHandler<CellStateChanged>
{
    private CellState _cellState;
    public readonly Position _position;
    private readonly Mediator _mediator;
    private readonly Dictionary<Position, CellState> _neighbours = new();

    private Cell(CellState cellState, Position position, Mediator mediator)
    {
        _cellState = cellState;
        _position = position;
        _mediator = mediator;
    }

    public static Cell LiveCell(Position position, Mediator mediator)
    {
        return new Cell(CellState.Alive, position, mediator);
    }
    public static Cell DeadCell(Position position, Mediator mediator)
    {
        return new Cell(CellState.Dead, position, mediator);
    }

    public CellState CellState => _cellState;
    public Position Position => _position;
    private int LiveNeighbours()
    {
        return _neighbours.Count(x => x.Value.Equals(CellState.Alive));
    }

    public void Handle(IterationStarted notification)
    {
        CellState nextState;
        if (_cellState == CellState.Dead && LiveNeighbours() != 3)
        {
            nextState = CellState.Dead;
        }
        else if (LiveNeighbours() == 2 || LiveNeighbours() == 3)
        {
            nextState = CellState.Alive;
        }
        else
        {
            nextState = CellState.Dead;
        }

        _mediator.Publish(new CellNextState(_position, nextState));
    }

    public void Handle(CellStateChanged notification)
    {
        if (_position.Equals(notification.Position))
        {
            _cellState = notification.CellState;
        }

        if (_position.IsNeighbourOf(notification.Position))
        {
            _neighbours[notification.Position] = notification.CellState;
        }
    }

}