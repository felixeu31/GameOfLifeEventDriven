namespace GameOfLifeEventDriven;

public record Position(int Row, int Column)
{
    public bool IsNeighbourOf(Position position)
    {
        if (this.Row == position.Row && this.Column == position.Column + 1) return true;
        if (this.Row == position.Row && this.Column == position.Column - 1) return true;
        if (this.Row == position.Row + 1 && this.Column == position.Column) return true;
        if (this.Row == position.Row - 1 && this.Column == position.Column) return true;
        if (this.Row == position.Row + 1 && this.Column == position.Column + 1) return true;
        if (this.Row == position.Row + 1 && this.Column == position.Column - 1) return true;
        if (this.Row == position.Row - 1 && this.Column == position.Column - 1) return true;
        if (this.Row == position.Row - 1 && this.Column == position.Column + 1) return true;

        return false;
    }

    public bool ItsMe(Position notificationPosition)
    {
        return this == notificationPosition;
    }
}