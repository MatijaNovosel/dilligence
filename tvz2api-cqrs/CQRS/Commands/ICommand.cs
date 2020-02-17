namespace tvz2api_cqrs.CQRS.Commands
{
  public interface ICommand { }
  public interface ICommand<TResult> : ICommand { }
}
