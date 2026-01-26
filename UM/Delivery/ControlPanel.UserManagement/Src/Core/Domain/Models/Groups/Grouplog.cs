using Domain.Models ;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
 

namespace Domain.Models
{
	public class GroupLog : InfoEntity
    {
		[ForeignKey("Group")]
		public Guid GroupId { get; set; }
		public Group Group { get; set; }

		public string Description { get; set; }
	}
}
