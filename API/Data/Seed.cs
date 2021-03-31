// File to get data out of json class UserSeedData
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        // Returning void so task does not require anything
        public static async Task SeedUsers(DataContext context)
        {
            // Return if we have any users
            if (await context.Users.AnyAsync()) return;
            // Store it in variable where users are located
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;
                // Add the given user to Users
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
        
    }
}