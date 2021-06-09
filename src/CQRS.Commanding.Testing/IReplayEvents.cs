using System;

namespace CQRS.Commanding.Testing
{
    public interface IReplayEvents
    {
        IReplayEvents Append<TEvent>(Action<TEvent> evt) where TEvent: class, IEvent, new();
    }
}