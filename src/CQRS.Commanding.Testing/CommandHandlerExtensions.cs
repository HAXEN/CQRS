using System;

namespace CQRS.Commanding.Testing
{
    public static class CommandHandlerExtensions    
    {
        public static int PersistedVersion<THandler>(this THandler handler)
            where THandler : class, IPersistUsingEventStream
        {
            return handler.PersistedVersion;
        }

        public static THandler RePlayEvents<THandler>(this THandler handler, Action<IReplayEvents> replay)
            where THandler : class, IPersistUsingEventStream
        {
            var rePlayer = new HandlerEventRePlayer<THandler>(handler);
            replay.Invoke(rePlayer);
            return handler;
        }

        public static THandler AppendPersisted<THandler>(this THandler handler, IEvent @event)
            where THandler : class, IPersistUsingEventStream
        {
            handler.AppendPersisted(@event);
            return handler;
        }
    }
}
