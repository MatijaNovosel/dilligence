namespace tvz2api_cqrs.Infrastructure.Commands
{
  public interface ICommand { }
  public interface ICommand<TResult> : ICommand { }
}
