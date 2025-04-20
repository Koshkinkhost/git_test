using api_for_kursach.ViewModels;
using api_for_kursach.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using api_for_kursach.Exceptions;
using Microsoft.EntityFrameworkCore;
using api_for_kursach.DTO;
namespace api_for_kursach.Repositories
{
    public interface IAuthRepository
    {
        Task AddUserAsync(string login,string password);
        Task<ArtistDTO> GeId(string login);
    }
    public class AuthRepository : IAuthRepository
    {
        private readonly MusicLabelContext _context; 
        public AuthRepository(MusicLabelContext context)
        {
            _context = context;
        }

       

        public async Task AddUserAsync(string login,string password)
        {
            User user = new User() { Username = login, PasswordHash = password, Role = "artist" };


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            Artist artist=new Artist() { UserId=user.UserId,Name=user.Username};
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();

        }

        public async Task<ArtistDTO> GeId(string login)
        {
            return await _context.Users
                   .Where(u => u.Username == login)
                   .Join(
                       _context.Artists,
                       u => u.UserId,         // связываем по UserId
                       a => a.UserId,
                       (u, a) => new ArtistDTO
                       {
                           Id = a.ArtistId,
                           name = a.Name
                       }
                   ).FirstOrDefaultAsync();
        }
    }
}
