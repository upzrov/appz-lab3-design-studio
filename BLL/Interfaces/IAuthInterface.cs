namespace BLL.Interfaces
{
    public interface IAuthService
    {
        bool ValidateUser(string username, string password, out string? role);
        void RegisterUser(string username, string password, string role);
    }
}
