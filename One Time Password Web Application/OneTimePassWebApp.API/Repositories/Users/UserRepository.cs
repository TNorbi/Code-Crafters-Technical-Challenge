using Microsoft.EntityFrameworkCore;
using OneTimePassWebApp.API.Data;

namespace OneTimePassWebApp.API.Repositories.Users
{
    public class UserRepository: IUserRepository
    {
        private WebAppDbContext _context { get;}

        public UserRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Models.Users>> getAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return users;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
