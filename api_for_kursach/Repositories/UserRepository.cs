using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace api_for_kursach.Repositories
{ 

    public interface IUserRepository
    {
        Task GetRoleByNameAsync(string v);
        Task<User> GetUserByLoginAsync(string login, string role);
        Task<User> GetUserByLoginAsync(string login);

        //Task<Role> GetRoleByNameAsync(string roleName);
        
    }

    public class UserRepository : IUserRepository
    {
        private readonly MusicLabelContext _context;

        public UserRepository(MusicLabelContext context)
        {
            _context = context;
        }

        public Task GetRoleByNameAsync(string v)
        {
            throw new NotImplementedException();
        }

        // Получить пользователя по логину, включая роль
        public async Task<User> GetUserByLoginAsync(string login,string role)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == login&&u.Role==role);
        }
        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == login);
        }


        // Получить роль по имени


        // Добавить нового пользователя
        //public async Task AddUserAsync(User user, Artist artist, Author author)
        //{
        //    // Проверяем, существует ли роль
        //    var role = await _context.Roles
        //        .FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
        //    if (role == null)
        //    {
        //        throw new ArgumentException("Role not found");
        //    }

        //    // Добавляем артиста
        //    _context.Artists.Add(artist);
        //    await _context.SaveChangesAsync();  // Сохраняем, чтобы получить ArtistId

        //    // Привязываем артиста к пользователю
        //    user.ArtistId = artist.ArtistId;  // Устанавливаем ArtistId у пользователя

        //    // Добавляем пользователя
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();  // Сохраняем пользователя в базу данных
        //    _context.Authors.Add(author);
        //    await _context.SaveChangesAsync();

        //}


    }
}
    
