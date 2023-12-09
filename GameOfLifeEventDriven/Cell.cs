namespace GameOfLifeEventDriven;

public class Cell
{
    private bool _isAlive;

    public Cell(Game game)
    {
        _isAlive = true;
        game.RaiseIterationEvent += HandleIterationEvent;
    }

    public bool IsAlive => _isAlive;

    void HandleIterationEvent(object sender, IterationEventArgs e)
    {
        _isAlive = false;
    }

}