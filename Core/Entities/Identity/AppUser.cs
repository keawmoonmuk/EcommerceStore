using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    //inherritan IdentityUser for identity
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}