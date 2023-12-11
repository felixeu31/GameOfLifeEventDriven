namespace GameOfLifeEventDriven;

public class Cell : INotificationHandler<Game.IterationStarted>, INotificationHandler<Game.CellChange>
{
    private bool _isAlive;
    public readonly Position _position;
    private readonly Mediator _mediator;
    private readonly Dictionary<Position, bool> _neighbours = new();

    private Cell(bool isAlive, Position position, Mediator mediator)
    {
        _isAlive = isAlive;
        _position = position;
        _mediator = mediator;
    }

    public static Cell LiveCell(Position position, Mediator mediator)
    {
        return new Cell(true, position, mediator);
    }
    public static Cell DeadCell(Position position, Mediator mediator)
    {
        return new Cell(false, position, mediator);
    }

    public bool IsAlive => _isAlive;
    public Position Position => _position;
    private int LiveNeighbours()
    {
        return _neighbours.Count(x => x.Value);
    }

    public void Handle(Game.IterationStarted notification)
    {
        bool nextState;
        if (_isAlive == false && LiveNeighbours() != 3)
        {
            nextState = false;
        }
        else if (LiveNeighbours() == 2 || LiveNeighbours() == 3)
        {
            nextState = true;
        }
        else
        {
            nextState = false;
        }

        _mediator.Publish(new CellNextState(_position, nextState));
    }

    public void Handle(Game.CellChange notification)
    {
        if (_position.ItsMe(notification.Position))
        {
            _isAlive = notification.IsAlive;
        }

        if (_position.IsNeighbourOf(notification.Position))
        {
            _neighbours[notification.Position] = notification.IsAlive;
        }
    }

    public record CellNextState(Position Position, bool IsAlive);
}