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
    }
}
