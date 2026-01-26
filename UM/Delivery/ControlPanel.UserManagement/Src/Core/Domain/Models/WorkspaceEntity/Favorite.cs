using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;


namespace Domain.Models
{
    public class FavoriteWorkspace : InfoEntity
    {

        public Guid WorkspaceId { set; get; }

        [InverseProperty("LoggedInUser")]
        public Guid? UserId { get; set; }
        public User LoggedInUser { get; set; }

    }
}