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
            _persistedVersion++;
        }

        int ICommandHandler.PersistedVersion => _persistedVersion;
        IEnumerable<IEvent> ICommandHandler.UnPersistedEvents() => _unPersistedEvents;

        protected void AddEvent(IEvent @event)
        {
            ApplyEvent(@event);

            _unPersistedEvents.Add(@event);
        }

        private void ApplyEvent(IEvent @event)
        {
            var applyMethod = GetType()
                .GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {@event.GetType()}, null);

            if (applyMethod == null)
                throw new MissingEventHandlerException();

            applyMethod.Invoke(this, new[] {@event});
        }
    }
}
