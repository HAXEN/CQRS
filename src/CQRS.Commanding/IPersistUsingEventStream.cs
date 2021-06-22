using System.Collections.Generic;

namespace CQRS.Commanding
{
    public interface IPersistUsingEventStream
    {
        void AppendPersisted(IEvent @event);
        int PersistedVersion { get; }
        IEnumerable<IEvent> UnPersistedEvents();
    }
}