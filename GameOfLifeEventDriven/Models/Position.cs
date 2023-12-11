namespace GameOfLifeEventDriven.Models;

public record Position(int Row, int Column)
{
    public bool IsNeighbourOf(Position position)
    {
        if (Row == position.Row && Column == position.Column + 1) return true;
        if (Row == position.Row && Column == position.Column - 1) return true;
        if (Row == position.Row + 1 && Column == position.Column) return true;
        if (Row == position.Row - 1 && Column == position.Column) return true;
        if (Row == position.Row + 1 && Column == position.Column + 1) return true;
        if (Row == position.Row + 1 && Column == position.Column - 1) return true;
        if (Row == position.Row - 1 && Column == position.Column - 1) return true;
        if (Row == position.Row - 1 && Column == position.Column + 1) return true;

        return false;
    }
}