namespace GameOfLifeEventDriven;

public record CellStateChanged(Position Position, CellState CellState);