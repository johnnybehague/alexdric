﻿using Alexdric.Application.Common;
using Alexdric.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Alexdric.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count > 0)
                .SelectMany(r => r.Errors)
                .Select(r => new BaseError() { PropertyMessage = r.PropertyName, ErrorMessage = r.ErrorMessage })
                .ToList();

            if (failures.Count > 0)
            {
                throw new CustomValidationException(failures);
            }
        }

        return await next(cancellationToken);
    }
}
