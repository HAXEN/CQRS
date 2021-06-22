using Moq;
using System.Threading;
using CQRS.Commanding.Impl;
using Xunit;

namespace CQRS.Commanding.Tests
{
    public class CommandMediator_Tests
    {
        [Fact]
        public async void Should_return_Error_with_validation_messages_for_invalid_Command()
        {
            var validatorMock = new Mock<IValidate>();
            validatorMock
                .Setup(x => x.ValidationMessages(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => IValidationMessage.Create("Name", "May not be empty").ToArray());

            var commandMediator = new CommandMediator(validatorMock.Object);

            var result = await commandMediator.Execute(new TestCommand(), CancellationToken.None);

            Assert.False(result.Success);
            Assert.NotNull(result.ValidationErrors);
            Assert.NotEmpty(result.ValidationErrors);
            
            validatorMock.VerifyAll();
        }

        public class TestCommand : ICommand
        {
            public string Name { get; set; }
        }
    }
}