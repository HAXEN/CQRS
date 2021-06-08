using Xunit;

namespace CQRS.Commanding.Tests
{
    public class CommandHandler_Tests
    {
        public class TestCommandHandler : CommandHandler
        {
        }

        [Fact]
        public void Should_be_able_to_Create_CommandHandler()
        {
            var handler = new TestCommandHandler();
            Assert.NotNull(handler);
        }
    }
}
