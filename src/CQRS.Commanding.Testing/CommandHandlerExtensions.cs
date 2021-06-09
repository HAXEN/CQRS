using System.Data;

namespace CQRS.Commanding.Testing
{
    public static class CommandHandlerExtensions    
    {
        public static int PersistedVersion<THandler>(this THandler handler)
            where THandler : class, ICommandHandler
        {
            return handler.PersistedVersion;
        }

        public static THandler AppendPersisted<THandler>(this THandler handler, IEvent @event)
            where THandler : class, ICommandHandler
        {
            handler.AppendPersisted(@event);
            return handler;
        }
    }
}
