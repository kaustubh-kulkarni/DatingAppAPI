using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }
        // Post request for registration
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            // Hashing algo for creating password hash
            // by using we mean to dispose the class after its use
            using var hmac = new HMACSHA512(); 

            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };
            // Add the user
            // Saying EF that we want to add this to users table
            // _context.table name.function()
            _context.Users.Add(user);
            // This part actually saves it to DB
            await _context.SaveChangesAsync(); 

            return user;
        }
    }
}