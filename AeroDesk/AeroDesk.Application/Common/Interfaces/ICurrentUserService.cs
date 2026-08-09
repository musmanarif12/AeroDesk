namespace AeroDesk.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? Email { get; }
        string? Role { get; }
        int? PassengerId { get; }
        bool IsAuthenticated { get; }
    }
}