using tvz2api_cqrs.Infrastructure.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tvz2api_cqrs.Infrastructure.CommandHandlers
{
    public interface IValidationHandlerAsync<TCommand> where TCommand : ICommand
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync(TCommand command);
    }

    public interface IValidationHandler<TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}
