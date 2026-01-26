
using Domain.Enums;

namespace Domain.Models
{
    public class Permission : EnitiyInfo
    {
        public string NameAr { get; set; }
        public string NameEn { set; get; }
        public bool IsAllAuthorized { get; set; }
        public string? MethodVerd { get; set; }
        public string? Path { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public Guid? WorkspaceId { set; get; }
        public long? WorkspaceKey { set; get; }
        public PermissionType PermissionType { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
