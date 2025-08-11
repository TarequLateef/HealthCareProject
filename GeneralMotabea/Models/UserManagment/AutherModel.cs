using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeneralMotabea.Core.Models.UserManagment
{
    public class AutherModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        //public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }

    public class RegisterModel
    {
        [Required, StringLength(40)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(140)]
        public string UserName { get; set; }
        [Required, StringLength(120)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
    }

    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AddRoleModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string RoleName { get; set; }
    }

    public class RevokeToken
    {
        public string? Token { get; set; }
    }
}
