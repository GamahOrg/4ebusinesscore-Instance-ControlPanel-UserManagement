using Domain.Models ;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class GroupRole : BaseEntity
	{
		[Required, ForeignKey("GroupId")]
		public Group Group { get; set; }
		public Guid GroupId { get; set; }

		[Required, ForeignKey("RoleId")]
		public Role Role { get; set; }
		public Guid RoleId { get; set; }
	}
}
