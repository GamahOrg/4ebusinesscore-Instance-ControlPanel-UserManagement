using Domain.Enums;
using Domain.Models ;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class QuickAccess : EnitiyInfo
    {
        public bool IsChecked { get; set; }
		// To be Discussed
		public Guid WorkSpaceCardId { set; get; }
		[ForeignKey("User")]
		public Guid? UserId { get; set; }
		public User User { get; set; }
    }
}