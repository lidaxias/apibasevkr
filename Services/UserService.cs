using apibase.Models;
using Microsoft.EntityFrameworkCore;

namespace apibase.Services
{
    public class UserService : IUserService
    {
        // контекст базы данных
        private readonly MyDbContext _dbContext; 

        public UserService(MyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<UserModel> AuthenticateAsync(string userlogin, string password)
        {
            var users = await _dbContext.Users.FirstOrDefaultAsync();

            // находим пользователя по имени
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Login == userlogin);

            // пользователь не найден
            if (user == null)
                return null; 

            // проверяем пароль 
            if (!VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        /// <summary>
        /// Верификация пароля
        /// </summary>
        /// <param name="enteredPassword">Пароль, введенный пользователем (в открытом виде)</param>
        /// <param name="storedHash">Хэш пароля, хранящийся в бд</param>
        /// <returns></returns>
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<UserModel> RegisterAsync(string username, string password)
        {
            // хэшируем пароль перед сохранением
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new UserModel
            {
                Username = username,
                Password = password
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }

    public interface IUserService
    {
        Task<UserModel> AuthenticateAsync(string username, string password);
        Task<UserModel> RegisterAsync(string username, string password);

        Task<UserModel> GetByIdAsync(int userId);
    }
}
