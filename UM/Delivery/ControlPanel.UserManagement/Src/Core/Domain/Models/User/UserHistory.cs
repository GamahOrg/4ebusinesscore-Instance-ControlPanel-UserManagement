using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models
{
    public class UserHistory : IdentityUserLogin<Guid>
	{
		[Required]
		public Guid Id { get; set; }

		[Required, ForeignKey("UserId")]
		public User User { get; set; }
		public State State { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
