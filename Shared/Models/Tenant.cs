using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleApp.Shared.Models
{
    public class Tenant : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Host { get; set; }
        public string DatabaseConnection { get; set; }
    }

    public class UserTenantAccess : BaseModel
    {
        [Required]
        public int TenantId { get; set; }
        [Required]
        public string UserId { get; set; }
        public Tenant Tenant { get; set; }
        public ApplicationUser User { get; set; }

    }


    //------------------------------------//
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }       
    }


    public class Announcement : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        public string Message { get; set; }       
        public User User { get; set; }
    }

    public class Document : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        public string Folder { get; set; }
        public string StorageLocation { get; set; }
        public int Version { get; set; }
        public User User { get; set; }
    }

    public class BankDetail : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string Branch { get; set; }
        public string Type { get; set; }
        public User User { get; set; }
    }

    public class JobDetail : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ReportsToUserId { get; set; }

        public string Division { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }

        public User User { get; set; }
        public User ReportsToUser { get; set; }
    }


    public class LeaveRequest : BaseModel
    {
        [Required]
        public int UserId { get; set; }       

        public string Description { get; set; }
        public int Used { get; set; }
        public int Accrued { get; set; }
        public int Balance { get; set; }
        public string Type { get; set; }

        public User User { get; set; }      
    }

    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int IsDeleted { get; set; }
    }

    public class UserAccessDTO
    {
        public UserAccessDTO()
        {

        }
        public int UserTenantAccessId { get; set; }

        public UserAccessDTO(UserTenantAccess userTenantAccess)
        {
            UserTenantAccessId = userTenantAccess.Id;
            UserId = userTenantAccess.UserId;
            Username = userTenantAccess.User?.UserName;
            Email = userTenantAccess.User?.Email;
        }

        public UserAccessDTO(int userTenantAccessId, string userId, string username, string email)
        {
            UserTenantAccessId = userTenantAccessId;
            UserId = userId;
            Username = username;
            Email = email;
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

    }
}
