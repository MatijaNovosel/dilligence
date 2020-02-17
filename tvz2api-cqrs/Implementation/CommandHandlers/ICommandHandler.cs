using tvz2api_cqrs.Implementation.Commands;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    void Handle(TCommand command);
  }

  public interface ICommandHandlerAsync<TCommand> where TCommand : ICommand
  {
    Task HandleAsync(TCommand command);
  }

  public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
  {
    ICommandResult<TResult> Handle(TCommand command);
  }

  public interface ICommandHandlerAsync<TCommand, TResult> where TCommand : ICommand<TResult>
  {
    Task<ICommandResult<TResult>> HandleAsync(TCommand command);
  }
}
