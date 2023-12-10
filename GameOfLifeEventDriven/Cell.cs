namespace GameOfLifeEventDriven;

public class Cell : INotificationHandler<Game.IterationStarted>
{
    private bool _isAlive;

    public Cell()
    {
        _isAlive = true;
    }

    public bool IsAlive => _isAlive;

    public void Handle(Game.IterationStarted notification)
    {
        _isAlive = false;
    }
}