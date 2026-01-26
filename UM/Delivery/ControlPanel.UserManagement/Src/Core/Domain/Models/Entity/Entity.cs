using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models 
{
	public class Entity : InfoEntity
	{
		public Guid Id { set; get; }
	}
}
