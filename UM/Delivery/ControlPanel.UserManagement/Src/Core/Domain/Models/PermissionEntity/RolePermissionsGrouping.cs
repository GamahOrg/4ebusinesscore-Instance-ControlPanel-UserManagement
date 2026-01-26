using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class RolePermissionsGrouping : BaseEntity
    {
        [ForeignKey("Role")]
        public Guid RoleId { set; get; }
        public virtual Role Role { set; get; }
        public long? WorkspaceKey { set; get; }
        public VerbPermissionEnum? VerbType { get; set; }
    }
}