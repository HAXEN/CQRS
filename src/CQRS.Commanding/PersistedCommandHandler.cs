using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commanding
{
    public abstract class CommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        public abstract Task Handle(TCommand command, CancellationToken token = default);
        public abstract string StreamId(TCommand command);  
    }

    public abstract class PersistedCommandHandler<TCommand> : CommandHandler<TCommand>, IPersistUsingEventStream
        where TCommand : class, ICommand
    {
        private int _persistedVersion = -1;
        private readonly List<IEvent> _unPersistedEvents = new();

        void IPersistUsingEventStream.AppendPersisted(IEvent @event)
        {
            ApplyEvent(@event, false);
            _persistedVersion++;
        }

        int IPersistUsingEventStream.PersistedVersion => _persistedVersion;
        IEnumerable<IEvent> IPersistUsingEventStream.UnPersistedEvents() => _unPersistedEvents;

        protected void AddEvent(IEvent @event)
        {
            ApplyEvent(@event, true);
            _unPersistedEvents.Add(@event);
        }

        private void ApplyEvent(IEvent @event, bool throwIfMissingHandler)
        {
            var applyMethod = GetType()
                .GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {@event.GetType()}, null);

            if (applyMethod == null)
                if (throwIfMissingHandler)
                    throw new MissingEventHandlerException();
                else
                    return;

            applyMethod.Invoke(this, new[] {@event});
        }
    }
}
