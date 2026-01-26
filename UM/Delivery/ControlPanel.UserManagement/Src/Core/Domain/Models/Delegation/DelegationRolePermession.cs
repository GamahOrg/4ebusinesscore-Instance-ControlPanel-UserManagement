using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class DelegationRolePermession : BaseEntity
    {
        [ForeignKey("UserDelegation")]
        public Guid? UserDelegationId { get; set; }
        public UserDelegation UserDelegation { get; set; }


        [ForeignKey("RolePermission")]
        public Guid? RolePermissionId { get; set; }
        public RolePermissions RolePermission { get; set; }

        public VerbPermissionEnum? VerbType { get; set; }
    }
}
