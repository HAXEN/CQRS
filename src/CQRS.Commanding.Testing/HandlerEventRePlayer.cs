using System;

namespace CQRS.Commanding.Testing
{
    internal class HandlerEventRePlayer<THandler> : IReplayEvents
        where THandler: class, ICommandHandler
    {
        private readonly THandler _handler;

        internal HandlerEventRePlayer(THandler handler)
        {
            _handler = handler;
        }

        public IReplayEvents Append<TEvent>(Action<TEvent> evt)
            where TEvent : class, IEvent, new()
        {
            var @event = Activator.CreateInstance<TEvent>();
            evt.Invoke(@event);
            _handler.AppendPersisted(@event);

            return this;
        }
    }
}