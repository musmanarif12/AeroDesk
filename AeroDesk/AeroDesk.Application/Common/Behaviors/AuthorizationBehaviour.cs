using AeroDesk.Application.Common.Exceptions;
using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Common.Security;
using MediatR;

namespace AeroDesk.Application.Common.Behaviors
{
    public class AuthorizationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType()
                .GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>()
                .ToList();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated
                if (!_currentUserService.IsAuthenticated)
                {
                    throw new UnauthorizedException();
                }

                // Role-based check
                var rolesAttributes = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
                    .ToList();

                if (rolesAttributes.Any())
                {
                    var authorized = false;

                    foreach (var roleAttribute in rolesAttributes)
                    {
                        var allowedRoles = roleAttribute.Roles
                            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                        if (allowedRoles.Any(role =>
                                string.Equals(role, _currentUserService.Role, StringComparison.OrdinalIgnoreCase)))
                        {
                            authorized = true;
                            break;
                        }
                    }

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException(
                            $"Role '{_currentUserService.Role}' is not authorized to perform this action.");
                    }
                }
            }

            return await next();
        }
    }
}