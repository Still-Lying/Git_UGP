using System;

namespace CodeBase.Tools
{
    public static class EventAggregator
    {
        public static void Subscribe<TEvent>(Action<object, TEvent> eventHandler) =>
            EventHolder<TEvent>.Event += eventHandler;

        public static void Unsubscribe<TEvent>(Action<object, TEvent> eventHandler) =>
            EventHolder<TEvent>.Event -= eventHandler;

        public static void Post<TEvent>(object sender, TEvent eventData) =>
            EventHolder<TEvent>.Post(sender, eventData);

        private static class EventHolder<T>
        {
            public static event Action<object, T> Event;

            public static void Post(object sender, T eventData) =>
                Event?.Invoke(sender, eventData);
        }
    }
}