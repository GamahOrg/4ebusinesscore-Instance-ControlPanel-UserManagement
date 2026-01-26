using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using _4EPlatform_Domain.Models.Lookup;

namespace Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Details { get; set; }
        public string Code { get; set; }
        public Guid? CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public Guid? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
        public State State { get; set; }

        [ForeignKey("RoleCategory")]
        public Guid? RoleCategoryId { get; set; }
        public LK_RoleCategory RoleCategory { get; set; }

        public virtual ICollection<User> Users { set; get; }
        public virtual ICollection<GroupRole> GroupRoles { set; get; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermissions>  RolePermissions { get; set; }
        public virtual ICollection<RolePermissionsGrouping> RolePermissionsGroupings { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
