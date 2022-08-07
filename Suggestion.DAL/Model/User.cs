using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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


        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public string? ProfileImage { get; set; }
        public string? UserImage { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        public ICollection<Tweet> UserTweet { get; set; }
    }

    public class Tweet
    {

        [Key]
        public int TweetId { get; set; }

        [StringLength(300)]

        public string UserTweet { get; set; }


        public ApplicationUser User { get; set; }

    }


}
