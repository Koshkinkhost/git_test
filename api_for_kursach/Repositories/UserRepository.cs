using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace api_for_kursach.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginAsync(string login);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task AddUserAsync(User user,Artist artist);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ContextDb _context;

        public UserRepository(ContextDb context)
        {
            _context = context;
        }

        // Получить пользователя по логину, включая роль
        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users .FirstOrDefaultAsync(u => u.Username == login);
        }

        // Получить роль по имени
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        // Добавить нового пользователя
        public async Task AddUserAsync(User user, Artist artist)
        {
            // Проверяем, существует ли роль
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
            if (role == null)
            {
                throw new ArgumentException("Role not found");
            }

            // Добавляем артиста
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();  // Сохраняем, чтобы получить ArtistId

            // Привязываем артиста к пользователю
            user.ArtistId = artist.ArtistId;  // Устанавливаем ArtistId у пользователя

            // Добавляем пользователя
            _context.Users.Add(user);
            await _context.SaveChangesAsync();  // Сохраняем пользователя в базу данных
        }


    }
}
