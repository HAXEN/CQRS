using System.Collections.Generic;

namespace CQRS.Commanding
{
    public interface ICommandResult
    {
        bool Success { get; }
        IEnumerable<IValidationMessage> ValidationErrors { get; }

        static ICommandResult SuccessResult() => new CommandResult(true);
        static ICommandResult ValidationFailed(IEnumerable<IValidationMessage> errors) => new CommandResult(false, errors);
    }

    internal class CommandResult : ICommandResult
    {
        public bool Success { get; private init; }
        public IEnumerable<IValidationMessage> ValidationErrors { get; private init; }

        internal CommandResult(bool success, IEnumerable<IValidationMessage> validationErrors = null)
        {
            Success = success;
            ValidationErrors = validationErrors ?? new IValidationMessage[0];
        }
    }
}