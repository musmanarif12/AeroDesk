using AeroDesk.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AeroDesk.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public int? UserId
        {
            get
            {
                var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(value, out var id) ? id : null;
            }
        }

        public string? Email =>
            User?.FindFirstValue(ClaimTypes.Email);

        public string? Role =>
            User?.FindFirstValue(ClaimTypes.Role);

        public int? PassengerId
        {
            get
            {
                var value = User?.FindFirstValue("PassengerId");
                return int.TryParse(value, out var id) ? id : null;
            }
        }
    }
}