using System;
using System.Collections.Generic;

public static class Publisher
{
    private static Dictionary<Type, List<ISubscriber>> _subscribers = new Dictionary<Type, List<ISubscriber>>();

    public static void Subscribe(ISubscriber subscriber, Type messageType)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Add(subscriber);
        }
        else
        {
            var subscribers = new List<ISubscriber> { subscriber };

            _subscribers.Add(messageType, subscribers);
        }
    }

    public static void Publish(IPublisherMessage messsage)
    {
        var messageType = messsage.GetType();

        if (!_subscribers.ContainsKey(messageType)) return;

        foreach (ISubscriber subscriber in _subscribers[messageType])
        {
            subscriber.OnPublish(messsage);
        }
    }

    public static void Unsubscribe(ISubscriber subscriber, Type messageType)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Remove(subscriber);
        }
    }
}
