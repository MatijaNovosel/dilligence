using tvz2api_cqrs.Infrastructure.Commands;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Infrastructure.Messaging
{
    public interface ICommandBus
  {
    void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    ICommandResult<TResult> Execute<TResult>(ICommand command);
    Task<ICommandResult<TResult>> ExecuteAsync<TResult>(ICommand command);
    ICommandResult<TResult> Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    Task<ICommandResult<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    void Validate<TCommand>(TCommand command) where TCommand : ICommand;
    Task ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand;
  }
}
