using Microsoft.AspNetCore.Identity;

namespace Suggestion.BL.Model
{


    //public class UserLogin : IdentityUserLogin<int>, IBaseEntity
    //{


    //    public string? CreatedBy { get; set; }
    //    public string? ModifiedBy { get; set; }


    //    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    //    public DateTime DateModified { get; set; } = DateTime.UtcNow;
    //}

    public class ApplicationUser : IdentityUser<int>, IBaseEntity
    {
        public ApplicationUser()
        {

            CreatedBy = Email;
            ModifiedBy = Email;
            UserName = Email;

        }


        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public string? ProfileImage { get; set; }
        public string? UserImage { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }


}
