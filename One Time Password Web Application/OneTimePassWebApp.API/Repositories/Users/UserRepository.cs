﻿using Microsoft.EntityFrameworkCore;
using OneTimePassWebApp.API.Data;
using OneTimePassWebApp.API.Data.Models;

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

        public async Task<Data.Models.Users?> getUserByUserId(int userId)
        {
            try
            {
                var result = await _context.Users.FindAsync(userId);

                return result;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Data.Models.Users?> getUserByUsername(string userName)
        {
            try
            {
                var result = await _context.Users.Where(x=> x.UserName == userName).FirstOrDefaultAsync();

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

                if(searchPassword != null)
                {
                    //jelszo letezik, le kell kezeljem valahogy
                }

                var searchUsername = await _context.Users.Where(x => x.UserName == newUser.UserName).FirstOrDefaultAsync();

                if (searchUsername != null)
                {
                    //felhasznalonev letezik, le kell kezeljem valahogy
                }

                var response = _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return response.Result.Entity;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
