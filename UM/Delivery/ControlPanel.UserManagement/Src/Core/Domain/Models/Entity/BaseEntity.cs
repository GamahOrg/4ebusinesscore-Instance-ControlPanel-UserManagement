using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models
{
    public class BaseEntity
	{
		public Guid Id { set; get; }
		public State State { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
