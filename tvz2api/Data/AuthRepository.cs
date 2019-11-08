using System;
using System.Threading.Tasks;
using tvz2api.Models;
using Microsoft.EntityFrameworkCore;

namespace tvz2api.Data
{
    public class AuthRepository : IAuthRepository 
    {
        private readonly tvz2Context _context;
        
        public AuthRepository(tvz2Context context) 
        {
            _context = context;
        }
        
        public async Task<Korisnik> Login(string username, string password) 
        {
            var user = await _context.Korisnik.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);
            if(user == null) 
            {
                return null;
            }
            if(!verifyPasswordHash(password, user.passwordHash, user.passwordSalt)) 
            {
                return null;
            }
            return user;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++) 
                {
                    if(computedHash[i] != passwordHash[i]) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<Korisnik> Register(Korisnik user, string password) 
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            
            await _context.Korisnik.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> userExists(string username) 
        {
            if (await _context.Korisnik.AnyAsync(x => x.Username == username)) 
            {
                return true;
            }
            return false;
        }
    }
}