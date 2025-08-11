using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GeneralMotabea.Core.Models.UserManagment
{
    public class ApplicationUser:IdentityUser
    {
        [Required, StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [StringLength(11)]
        public string? Phone { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(80)]
        public string Email { get; set; }
        public List<RefreshToken>? RefrshTokens { get; set; }
    }

    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
