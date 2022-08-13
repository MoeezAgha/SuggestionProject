using System.ComponentModel.DataAnnotations;

namespace Suggestion.Shared.Model.ViewModel
{
    public class LoginParameters
    {


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }

    public class RegisterParameters
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }

        public Dictionary<string, string> ExposedClaims { get; set; }

        public string Token { get; set; }




    }
    public interface IUserInfo
    {
        Dictionary<string, string> ExposedClaims { get; set; }
        bool IsAuthenticated { get; set; }
        string UserName { get; set; }
    }


}