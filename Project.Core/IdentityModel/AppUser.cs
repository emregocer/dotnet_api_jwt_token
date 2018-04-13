using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.IdentityModel
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>, IUser<int>, IEntity<int>
    {
        public AppUser()
        {
            CreatedTime = DateTime.Now;
        }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedTime { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppRole : IdentityRole<int, AppUserRole> { }

}
