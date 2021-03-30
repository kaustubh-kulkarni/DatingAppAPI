using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    // User class for application
    public class AppUser
    {
        // Using shorthand version to get and set from other classes
        // Property for ID
        public int Id { get; set; }
        // Property for Username
        public string UserName { get; set; }
        // Property for passwords
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
        
    }
}