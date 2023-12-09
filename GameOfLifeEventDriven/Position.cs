namespace GameOfLifeEventDriven;

public class Position
{
    private readonly int _row;
    private readonly int _column;

    public Position(int row, int column)
    {
        _row = row;
        _column = column;
    }
}