using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }
        // Post request for registration
        [HttpPost("register")]
        // Function to register the user
        // Typical implementation is datatype_async_Task<ActionResult<Database entity>>_functionname(params)
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Check if the user already exists
            if (await UserExists(registerDto.Username)) return BadRequest("Username already exist");

            var user = _mapper.Map<AppUser>(registerDto);
            // Hashing algo for creating password hash
            // by using we mean to dispose the class after its use
            using var hmac = new HMACSHA512();
            // Object creation
            // Setting up the params using DTO
            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
          
            // Add the user
            // Saying EF that we want to add this to users table
            // _context.table name.function()
            _context.Users.Add(user);
            // This part actually saves it to DB
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }
        // Method for login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            // Check if the user is empty or not
            if (user == null) return Unauthorized("Invalid Username");
            // reverse the computed hash
            using var hmac = new HMACSHA512(user.PasswordSalt);
            // password that is in loginDto
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };


        }

        // Function to check if username already exists or not
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}