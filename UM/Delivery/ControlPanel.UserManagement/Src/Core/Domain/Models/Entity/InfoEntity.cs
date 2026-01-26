using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class InfoEntity : BaseEntity
	{
		[ForeignKey("User")]
		public Guid? CreatedBy { set; get; }
		public User User { get; set; }
		public DateTime CreatedOn { set; get; }
		public Guid? UpdatedBy { set; get; }
		public DateTime? UpdatedOn { set; get; }
	}
}
