using OneTimePassWebApp.API.Repositories.Users;

namespace OneTimePassWebApp.API.Services.Users
{
    public class UserService: IUserService
    {
        private IUserRepository _userRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
