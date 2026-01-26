using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class RolePermissions : BaseEntity
    {
        [ForeignKey("Role")]
        public Guid RoleId { set; get; }
        public virtual Role Role { set; get; }
        [ForeignKey("Permission")]
        public Guid PermissionId { set; get; }
        public virtual Permission Permission { set; get; }
        public ICollection<DelegationRolePermession> DelegationRolePermessions { get; set; }
        public Guid WorkSpaceId { set; get; }
        public VerbPermissionEnum? VerbType { get; set; }
        // To be discussed
        public bool IsChecked { get; set; } = false;
    }
}