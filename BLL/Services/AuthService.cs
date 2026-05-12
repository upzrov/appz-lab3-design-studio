using System.Security.Cryptography;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AuthService(IUnitOfWork uow) : IAuthService
    {
        private const int Iterations = 10000;
        private const int HashSize = 20;
        private const int SaltSize = 16;

        public bool ValidateUser(string username, string password, out string? role)
        {
            role = null;
            var user = uow.GetRepository<User>().GetAll().FirstOrDefault(u => u.Username == username);

            if (user == null) return false;

            if (VerifyPassword(password, user.PasswordHash))
            {
                role = user.Role;
                return true;
            }

            return false;
        }

        public void RegisterUser(string username, string password, string role)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = role
            };

            uow.GetRepository<User>().Create(user);
            uow.Save();
        }

        private static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i]) return false;
            }
            return true;
        }
    }
}
