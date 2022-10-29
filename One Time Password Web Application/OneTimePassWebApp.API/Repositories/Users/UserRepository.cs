using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneTimePassWebApp.API.Data;
using OneTimePassWebApp.API.Data.Models.DTOs.Users;

namespace OneTimePassWebApp.API.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private WebAppDbContext _context { get; }

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

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Data.Models.Users?> getUserByUserId(int userId)
        {
            try
            {
                var result = await _context.Users.FindAsync(userId);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Data.Models.Users?> getUserByUsername(string userName)
        {
            try
            {
                var result = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Data.Models.Users?> registerNewUser(Data.Models.Users newUser)
        {
            try
            {

                var searchPassword = await _context.Users.Where(x => x.Password == newUser.Password).FirstOrDefaultAsync();

                if (searchPassword != null)
                {
                    return new Data.Models.Users { UserId = -1 };
                }

                var searchUsername = await _context.Users.Where(x => x.UserName == newUser.UserName).FirstOrDefaultAsync();

                if (searchUsername != null)
                {
                    return new Data.Models.Users { UserId = -2 };
                }

                var response = _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return response.Result.Entity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserLoginDTO?> loginUser(Data.Models.Users user)
        {
            try
            {
                PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

                var searchUser = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();

                if (searchUser != null)
                {
                    var result = passwordHasher.VerifyHashedPassword(user.UserName, searchUser.Password, user.Password);

                    if(result == PasswordVerificationResult.Success)
                    {
                        UserLoginDTO userLoginDTO = new UserLoginDTO
                        {
                            UserName = searchUser.UserName,
                            UserID = searchUser.UserId
                        };

                        return userLoginDTO;
                    }
                    
                }

                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string?> generateOTP(int userID)
        {
            try
            {
                var result = await _context.Users.FindAsync(userID);

                if(result == null)
                {
                    return null;
                }

                //valahogy igy kene asszem
                //string OTP = createNewOTP(result.email)

                string OTP = createNewOTP();

                return OTP;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string createNewOTP()
        {
            return "";
        }
    }
}
