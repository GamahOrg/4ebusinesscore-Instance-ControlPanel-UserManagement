using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Models ;

namespace Domain.Models
{
    public class Group : InfoEntity
	{
		public string GroupName { get; set; }

		[ForeignKey("Manager")]
		public Guid? ManagerId { get; set; }
		public User Manager { get; set; }
		public string Description { get; set; }
		public string? ImageUrl { get; set; }
        public Guid OrgnizationId { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
		public virtual ICollection<GroupLog> Grouplogs { get; set; }
		public virtual ICollection<GroupRole> GroupRoles { get; set; }
	}
}
