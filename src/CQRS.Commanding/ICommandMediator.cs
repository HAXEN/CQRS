using System.Threading;
using System.Threading.Tasks;
using CQRS.Commanding.Impl;

namespace CQRS.Commanding
{
    public interface ICommandMediator
    {
        Task<ICommandResult> Execute(ICommand command, CancellationToken token);
        static ICommandMediator Create(Configuration config) => new CommandMediator(config.Validator);
    }
}