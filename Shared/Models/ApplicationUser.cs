using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ExampleApp.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {

    }

    public class ApplicationUserDTO
    {
        public ApplicationUserDTO()
        {

        }

        public ApplicationUserDTO(ApplicationUser applicationUser)
        {

            UserId = applicationUser.Id;
            Username = applicationUser.UserName;
            Email = applicationUser.Email;
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
