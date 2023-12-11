using GameOfLifeEventDriven.Models;

namespace GameOfLifeEventDriven.Events;

public record CellStateChangedEvent(Position Position, CellState CellState);