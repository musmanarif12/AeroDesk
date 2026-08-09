namespace AeroDesk.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("You must be logged in to perform this action.")
        {
        }
    }
}