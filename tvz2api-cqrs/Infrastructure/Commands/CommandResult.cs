using System.Collections.Generic;

namespace tvz2api_cqrs.Infrastructure.Commands
{
    public interface ICommandResult<TResult>
  {
    TResult Payload { get; }
    bool IsSuccess { get; }
    List<string> FailureReason { get; }
  }

  public class CommandResult<TResult> : ICommandResult<TResult>
  {
    public CommandResult(string reason) => FailureReason.Add(reason);
    public CommandResult(TResult payload) => Payload = payload;
    public TResult Payload { get; }
    public bool IsSuccess { get { return FailureReason.Count == 0; } }
    public List<string> FailureReason { get; } = new List<string>();
    public static CommandResult<TResult> Fail(string reason) => new CommandResult<TResult>(reason);
    public static CommandResult<TResult> Success(TResult payload) => new CommandResult<TResult>(payload);
  }
}
