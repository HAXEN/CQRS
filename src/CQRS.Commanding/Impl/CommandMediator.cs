using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commanding.Impl
{
    internal class CommandMediator : ICommandMediator
    {
        private readonly IValidate _validator;

        internal CommandMediator(IValidate validator)
        {
            _validator = validator;
        }

        public async Task<ICommandResult> Execute(ICommand command, CancellationToken token)
        {
            var validationErrors = await _validator.ValidationMessages(command, token);
            if(validationErrors != null && validationErrors.Any())
                return ICommandResult.ValidationFailed(IValidationMessage.Create("Name", "May not be empty").ToArray());

            return command.Success();
        }
    }
}