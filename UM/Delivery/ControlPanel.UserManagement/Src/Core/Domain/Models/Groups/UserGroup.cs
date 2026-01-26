using Domain.Models ;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Domain.Models
{
    public class UserGroup :InfoEntity
	{

		[ForeignKey("GroupMember")]
		public Guid UserId { get; set; }
		public User GroupMember { get; set; }

		[ForeignKey("Group")]
		public Guid GroupId { get; set; }
		public Group Group { get; set; }
	}
}
