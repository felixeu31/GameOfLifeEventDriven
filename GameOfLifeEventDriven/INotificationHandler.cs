namespace GameOfLifeEventDriven;

public interface INotificationHandler<TNotification>
{
    void Handle(TNotification notification);
}