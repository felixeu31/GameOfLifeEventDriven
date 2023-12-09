namespace GameOfLifeEventDriven;

public class IterationEventArgs : EventArgs
{
    public IterationEventArgs(int number)
    {
        Number = number;
    }

    public int Number { get; set; }
}