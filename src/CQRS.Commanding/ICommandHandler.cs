using System.Collections.Generic;

namespace CQRS.Commanding
{
    public interface ICommandHandler
    {
        void AppendPersisted(IEvent @event);
        int PersistedVersion { get; }
        IEnumerable<IEvent> UnPersistedEvents();
    }
}