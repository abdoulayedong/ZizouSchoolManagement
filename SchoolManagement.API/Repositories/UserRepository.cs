using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolManagmentDBContext _context;

        public UserRepository(SchoolManagmentDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a User
        /// </summary>
        /// <param name="userToRegister"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Register(User userToRegister, string password)
        {
            if (userToRegister == null)
            {
                throw new ArgumentNullException(nameof(userToRegister));
            }
            else
            {
                byte[] passwordHash, passwordSalt;

                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                userToRegister.PasswordHash = passwordHash;

                userToRegister.PasswordSalt = passwordSalt;

                _context.Add(userToRegister);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return userToRegister;
                }
                else
                {
                    return null;
                }

            }
        }
        /// <summary>
        /// Authenticate a USer
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Login(string email, string password)
        {
            var doesUserExists = await UserExists(email);
            if (doesUserExists == true)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

                if (!VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt))
                    return null;

                return existingUser;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<User> GetUserById(int id)
        {
            if(id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
    }
}
