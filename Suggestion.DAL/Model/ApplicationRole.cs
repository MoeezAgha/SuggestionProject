using Microsoft.AspNetCore.Identity;

namespace Suggestion.BL.Model
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }


}
