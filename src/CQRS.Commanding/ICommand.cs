namespace CQRS.Commanding
{
    public static class CommandExtensions
    {
        public static ICommandResult Success(this ICommand command) => ICommandResult.SuccessResult();
    }

    public interface ICommand
    {
    }
}
