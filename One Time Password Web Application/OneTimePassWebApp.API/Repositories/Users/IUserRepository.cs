namespace OneTimePassWebApp.API.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Data.Models.Users>> getAllUsers();
    }
}
