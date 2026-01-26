using Domain.Models ;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class FunctionalData : InfoEntity
    {

		public string? JobCode { get; set; }
		public DateTime? HiringDate { get; set; }
		public string? Vacation { get; set; }
		public string? LengthOfService { get; set; }
		public string? Upgrade { get; set; }

        [ForeignKey("SystemUser")]
        public Guid? SystemUserId { set; get; }
        public User SystemUser { get; set; }
    }
}
