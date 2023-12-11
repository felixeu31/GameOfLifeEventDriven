using GameOfLifeEventDriven.Models;

namespace GameOfLifeEventDriven.Events;

public record NextCellStateCalculatedEvent(Position Position, CellState CellState);