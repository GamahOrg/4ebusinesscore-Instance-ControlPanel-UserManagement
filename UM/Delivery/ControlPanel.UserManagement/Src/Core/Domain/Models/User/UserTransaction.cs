using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4EPlatform_Domain.Models.User
{
    public class UserTransaction: InfoEntity
    {
        [InverseProperty("LoggedInUser")]
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }


        public Domain.Models.User LoggedInUser { get; set; }
    }
}
