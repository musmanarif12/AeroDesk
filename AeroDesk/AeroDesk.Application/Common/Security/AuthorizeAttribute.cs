namespace AeroDesk.Application.Common.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Comma-separated role names, e.g. "Administrator,AirlineManager"
        /// Leave empty to just require authentication (any logged-in user).
        /// </summary>
        public string Roles { get; set; } = string.Empty;
    }
}