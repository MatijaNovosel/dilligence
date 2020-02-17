namespace tvz2api_cqrs.Implementation.Commands
{
  public interface ICommand { }
  public interface ICommand<TResult> : ICommand { }
}
