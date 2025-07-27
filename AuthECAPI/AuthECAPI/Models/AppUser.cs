using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthECAPI.Models
{
    //we inherit this class from the identity model
    public class AppUser : IdentityUser
    {
        //extra property
        [PersonalData]
        [Column(TypeName ="nvarchar(150")]
        public string? FullName { get; set; }
    }
}
