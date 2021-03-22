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
        
    }
}