using Domain.Models;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models
{
    public class Domains : InfoEntity
    {

		public string DomainName { get; set; }
		public string DomainNickName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool? IsActive { get; set; } = true;
        public string Url { get; set; }
        public ActionNameEnum ActionName { get; set; } 
        public string Body { get; set; }
        public virtual ICollection<User> Users { get; set; }

	}
}
