namespace GameOfLifeEventDriven;

public class Cell : INotificationHandler<Game.IterationStarted>, INotificationHandler<Game.NewNeighbour>
{
    private bool _isAlive;
    public readonly Position _position;
    private readonly Dictionary<Position, Cell> _neighbours = new();

    private Cell(bool isAlive, Position position)
    {
        _isAlive = isAlive;
        _position = position;
    }

    public static Cell LiveCell(Position position)
    {
        return new Cell(true, position);
    }
    public static Cell DeadCell(Position position)
    {
        return new Cell(false, position);
    }

    public bool IsAlive => _isAlive;
    public Position Position => _position;
    private int LiveNeighbours()
    {
        return _neighbours.Count(x => x.Value.IsAlive);
    }

    public void Handle(Game.IterationStarted notification)
    {
        if (LiveNeighbours() == 2 || LiveNeighbours() == 3)
        {
            _isAlive = true;
        }
        else
        {
            _isAlive = false;
        }
    }

    public void Handle(Game.NewNeighbour notification)
    {
        if (notification.Position != _position)
        {
            _neighbours[notification.Position] = notification.Cell;
        }
    }
}