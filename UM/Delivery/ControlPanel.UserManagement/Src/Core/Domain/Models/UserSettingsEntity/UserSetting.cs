using Domain.Models ;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Domain.Models
{
    public class UserSetting : InfoEntity
    {
		public int SortIndex { get; set; }
		public bool IsChecked { get; set; }
		[ForeignKey("HomePage")]
		public Guid HomeId { get; set; }
		public HomePage HomePage { get; set; }

	}
}