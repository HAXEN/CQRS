using CQRS.Commanding.Testing;
using Xunit;

namespace CQRS.Commanding.Tests
{
    public class CommandHandlerTests
    {
        private readonly TestCommandHandler _handler;

        public CommandHandlerTests()
        {
            _handler = new TestCommandHandler();
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
            Assert.Throws<MissingEventHandlerException>(() => _handler.Execute(new NoEventHandlerCommand()));
        }

        [Fact]
        public void Should_be_able_to_handle_Command_and_produce_UnPersisted_events()
        {
            _handler.Execute(new TestCommand{Id = "id", Name = "name"});
            Assert.NotEmpty(((ICommandHandler)_handler).UnPersistedEvents());
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

        public class NoEventHandlerCommand {}

        public class TestCommand
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class TestCommandHandler : CommandHandler
        {
            public bool EventHandled { get; set; }

            private void Apply(TestEvent @event)
            {
                EventHandled = true;
            }

            public void Execute(TestCommand command)
            {
                AddEvent(new TestEvent
                {
                    Id = "1",
                    Name = "name",
                });
            }

            public void Execute(NoEventHandlerCommand command)
            {
                AddEvent(new NoHandlerEvent());
            }
        }
    }
}
