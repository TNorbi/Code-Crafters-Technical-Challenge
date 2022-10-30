using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneTimePassWebApp.API.Data;
using OneTimePassWebApp.API.Data.AES;
using OneTimePassWebApp.API.Data.Models;
using OneTimePassWebApp.API.Data.Models.DTOs.Users;
using OneTimePassWebApp.API.Data.Requests.OTP;

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

                //var searchUser = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();
                //var searchUser = await _context.Users.Where(x => x.UserName.Equals(user.)).FirstOrDefaultAsync();

                var searchUser = await _context.Users.Where(x => EF.Functions.Collate(x.UserName, "SQL_Latin1_General_CP1_CS_AS") == user.UserName).FirstOrDefaultAsync();

                if (searchUser != null)
                {
                    var result = passwordHasher.VerifyHashedPassword(user.UserName, searchUser.Password, user.Password);

                    if (result == PasswordVerificationResult.Success)
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

                //user with given userid doesn't exists
                if (result == null)
                {
                    return null;
                }

                string OTP = createNewOTP();

                return OTP;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string createNewOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = alphabets + small_alphabets + numbers;

            int length = 5;
            string OTP = string.Empty;

            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (OTP.IndexOf(character) != -1);

                OTP += character;
            }

            string OTP_encrypted = AesEncryptDecrypt.EncryptUsingCBC(OTP);

            return OTP_encrypted;
        }

        public async Task<OTPCheckers?> verifyOTP(OTPVerifyRequest request)
        {
            OTPCheckers checkers = new OTPCheckers();

            try
            {
                var searchUser = await _context.Users.FindAsync(request.UserID);

                if (searchUser == null)
                {
                    return null;
                }

                var expireDate = DateTimeOffset.FromUnixTimeSeconds(request.ExpireDate);

                if (request.DateTime <= expireDate)
                {
                    var enteredOTP_decrypted = AesEncryptDecrypt.DecryptUsingCBC(request.EnteredOTP);
                    var originalOTP_decrypted = AesEncryptDecrypt.DecryptUsingCBC(request.OriginalOTP);

                    if (enteredOTP_decrypted == originalOTP_decrypted)
                    {
                        checkers.IsOTP = true;
                        checkers.isDateExpired = false;
                    }
                    else
                    {
                        checkers.IsOTP = false;
                        checkers.isDateExpired = false;
                    }
                }
                else
                {
                    checkers.IsOTP = false;
                    checkers.isDateExpired = true;
                }

                return checkers;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
