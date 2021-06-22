using System.Threading;
using System.Threading.Tasks;
using CQRS.Commanding.Testing;
using Xunit;

namespace CQRS.Commanding.Tests
{
    public class CommandHandlerTests
    {
        private readonly TestPersistUsingEventStream _handler;
        private readonly NoEventHandler _noEventHandler;

        public CommandHandlerTests()
        {
            _handler = new TestPersistUsingEventStream();
            _noEventHandler = new NoEventHandler();

            Assert.NotNull(_handler);
        }

        [Fact]
        public void Should_handle_event_when_handler_exists_for_persisted_events()
        {
            _handler.RePlayEvents(player => player
                .Append<TestEvent>(e =>
                {
                    e.Id = "Id";
                    e.Name = "Name";
                }));

            Assert.Equal(0, _handler.PersistedVersion());
            Assert.True(_handler.EventHandled);
        }

        [Fact]
        public void Should_not_handle_event_when_handler_is_missing_for_persisted_events()
        {
            _handler.AppendPersisted(new NoHandlerEvent());
            Assert.Equal(0, _handler.PersistedVersion());
            Assert.False(_handler.EventHandled);
        }

        [Fact]
        public void Should_throw_if_private_handler_for_event_is_missing_for_command()
        {
            Assert.ThrowsAsync<MissingEventHandlerException>(() => _noEventHandler.Handle(new NoEventHandlerCommand()));
        }

        [Fact]
        public void Should_be_able_to_handle_Command_and_produce_UnPersisted_events()
        {
            _handler.Handle(new TestCommand{Id = "id", Name = "name"});
            Assert.NotEmpty(((IPersistUsingEventStream)_handler).UnPersistedEvents());
            Assert.True(_handler.EventHandled);
        }

        [Fact]
        public void Should_be_able_to_Append_Persisted_events()
        {
            _handler.RePlayEvents(replay => replay
                .Append<TestEvent>(evt =>
                {
                    evt.Id = "Id";
                    evt.Name = "Name";
                })
                .Append<TestEvent>(evt =>
                {
                    evt.Id = "Id1";
                    evt.Name = "Name1";
                })); 

            Assert.Equal(1, _handler.PersistedVersion());
        }

        public class TestEvent : IEvent
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class NoHandlerEvent : IEvent
        {
        }

        public class NoEventHandlerCommand : ICommand
        {}

        public class TestCommand : ICommand
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class NoEventHandler : PersistedCommandHandler<NoEventHandlerCommand>
        {
            public bool EventHandled { get; set; }

            public override async Task Handle(NoEventHandlerCommand command, CancellationToken token = default)
            {
                AddEvent(new NoHandlerEvent());
            }

            public override string StreamId(NoEventHandlerCommand command)
            {
                return "";
            }
        }

        public class TestPersistUsingEventStream : PersistedCommandHandler<TestCommand>
        {
            public bool EventHandled { get; set; }

            private void Apply(TestEvent @event)
            {
                EventHandled = true;
            }

            public override async Task Handle(TestCommand command, CancellationToken token = default)
            {
                AddEvent(new TestEvent
                {
                    Id = "1",
                    Name = "name",
                });
            }

            public override string StreamId(TestCommand command)
            {
                return "";
            }
        }
    }
}
