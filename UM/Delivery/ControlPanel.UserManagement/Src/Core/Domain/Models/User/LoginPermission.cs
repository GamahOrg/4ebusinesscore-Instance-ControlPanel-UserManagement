using Domain.Models ;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Domain.Models
{
	public class LoginPermission : InfoEntity
    {

		public bool IsNafaz { get; set; }
		public bool IsExpired { get; set; }
		public DateTime? ExpirationDateFrom { get; set; }
		public DateTime? ExpirationDateTo { get; set; }

		[ForeignKey("SystemUser")]
		public Guid? SystemUserId { set; get; }
		[InverseProperty("LoginPermissions")]
		public virtual User SystemUser { set; get; }

	}
}
