using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
