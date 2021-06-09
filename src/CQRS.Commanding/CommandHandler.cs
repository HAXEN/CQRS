using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CQRS.Commanding
{
    public abstract class CommandHandler : ICommandHandler
    {
        private int _persistedVersion = -1;
        private readonly List<IEvent> _unPersistedEvents = new();

        void ICommandHandler.AppendPersisted(IEvent @event)
        {
            ApplyEvent(@event, false);
            _persistedVersion++;
        }

        int ICommandHandler.PersistedVersion => _persistedVersion;
        IEnumerable<IEvent> ICommandHandler.UnPersistedEvents() => _unPersistedEvents;

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
