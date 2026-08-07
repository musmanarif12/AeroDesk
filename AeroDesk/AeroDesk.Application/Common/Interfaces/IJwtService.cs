using AeroDesk.Domain.Entities;

namespace AeroDesk.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}