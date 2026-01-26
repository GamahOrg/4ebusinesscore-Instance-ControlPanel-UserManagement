using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class EnitiyInfo : BaseEntity
	{
		public Guid? CreatedBy { set; get; }
		public DateTime CreatedOn { set; get; }
		public Guid? UpdatedBy { set; get; }
		public DateTime? UpdatedOn { set; get; }
	}
}
