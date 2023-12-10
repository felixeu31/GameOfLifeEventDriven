namespace GameOfLifeEventDriven;

public class Mediator
{
    // Dictionary to store event handlers
    private readonly Dictionary<Type, List<object>> _handlers = new Dictionary<Type, List<object>>();

    // Method to subscribe a handler for a specific event
    public void Subscribe<TNotification>(INotificationHandler<TNotification> handler)
    {
        var eventType = typeof(TNotification);

        if (!_handlers.TryGetValue(eventType, out var handlers))
        {
            handlers = new List<object>();
            _handlers[eventType] = handlers;
        }

        handlers.Add(handler);
    }

    // Method to publish a notification to all subscribed handlers
    public void Publish<TNotification>(TNotification notification)
    {
        var eventType = typeof(TNotification);

        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            foreach (var handler in handlers)
            {
                ((INotificationHandler<TNotification>)handler).Handle(notification);
            }
        }
    }
}