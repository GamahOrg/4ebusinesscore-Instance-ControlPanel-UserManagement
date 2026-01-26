using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4EPlatform_Domain.Models.WorkspaceEntity
{
    public class HiddenWorkspace : InfoEntity
    {
        public Guid WorkspaceId { set; get; }


        [InverseProperty("LoggedInUser")]
        public Guid? UserId { get; set; }
        public Domain.Models.User LoggedInUser { get; set; }
    }
}