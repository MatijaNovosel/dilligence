using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Exceptions
{

  [Serializable]
  public class CommandValidationException : Exception
  {
    public CommandValidationException(IEnumerable<ValidationResult> validationResults)
    {
      ValidationResults = validationResults;
    }
    public CommandValidationException(string message) : base(message) { }
    public CommandValidationException(string message, Exception inner) : base(message, inner) { }
    protected CommandValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public IEnumerable<ValidationResult> ValidationResults { get; }
  }
}
