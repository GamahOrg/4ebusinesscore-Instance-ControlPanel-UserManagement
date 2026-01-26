using Domain.Models ;
using System.ComponentModel.DataAnnotations;
 

namespace Domain.Models
{
    public class HomePage : InfoEntity
    {
		[Required]
		public string NameAr { get; set; }
		[Required]
		public string NameEn { get; set; }
		public string ComponentName { get; set; }

	}
}