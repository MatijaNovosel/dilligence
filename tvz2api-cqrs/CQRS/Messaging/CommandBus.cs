using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Linq;

namespace tvz2api_cqrs.CQRS.Messaging
{
  public class CommandBus : ICommandBus
  {
    IServiceProvider _serviceProvider;

    public CommandBus(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void Execute<TCommand>(TCommand command) where TCommand : ICommand
    {
      Validate(command);
      var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      handler.Handle(command);
    }

    public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
      await ValidateAsync(command);
      var handler = _serviceProvider.GetService<ICommandHandlerAsync<TCommand>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      await handler.HandleAsync(command);
    }

    public ICommandResult<TResult> Execute<TResult>(ICommand command)
    {
      Validate(command);
      var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
      dynamic handler = _serviceProvider.GetService(handlerType);

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      return handler.Handle((dynamic)command);
    }

    public async Task<ICommandResult<TResult>> ExecuteAsync<TResult>(ICommand command)
    {
      await ValidateAsync(command);
      var handlerType = typeof(ICommandHandlerAsync<,>).MakeGenericType(command.GetType(), typeof(TResult));
      dynamic handler = _serviceProvider.GetService(handlerType);

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      return await handler.HandleAsync((dynamic)command);
    }

    public ICommandResult<TResult> Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
      Validate(command);
      var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      return handler.Handle(command);
    }

    public async Task<ICommandResult<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
      await ValidateAsync(command);
      var handler = _serviceProvider.GetService<ICommandHandlerAsync<TCommand, TResult>>();

      if (handler == null)
      {
        throw new InvalidOperationException("No command handler registered");
      }

      return await handler.HandleAsync(command);
    }

    public void Validate<TCommand>(TCommand command) where TCommand : ICommand
    {
      var validator = _serviceProvider.GetService<IValidator<TCommand>>();

      if (validator != null)
      {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
          throw new CommandValidationException(validationResult.Errors.Select(t => new ValidationResult(t.PropertyName, t.ErrorMessage)));
        }
      }
    }

    public async Task ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
      var validator = _serviceProvider.GetService<IValidator<TCommand>>();

      if (validator != null)
      {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
          throw new CommandValidationException(validationResult.Errors.Select(t => new ValidationResult(t.PropertyName, t.ErrorMessage)));
        }
      }
    }

  }
}
