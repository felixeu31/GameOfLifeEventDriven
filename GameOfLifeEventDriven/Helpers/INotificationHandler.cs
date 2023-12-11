namespace GameOfLifeEventDriven.Helpers;

public interface INotificationHandler<TNotification>
{
    void Handle(TNotification notification);
}
