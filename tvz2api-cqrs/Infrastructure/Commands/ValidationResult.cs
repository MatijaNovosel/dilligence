namespace tvz2api_cqrs.Infrastructure.Commands
{
    public class ValidationResult
  {
    public ValidationResult() { }

    public ValidationResult(string propertyName, string message)
    {
      PropertyName = propertyName;
      Message = message;
    }

    public ValidationResult(string message)
    {
      Message = message;
    }

    public string PropertyName { get; set; }
    public string Message { get; set; }
  }
}
