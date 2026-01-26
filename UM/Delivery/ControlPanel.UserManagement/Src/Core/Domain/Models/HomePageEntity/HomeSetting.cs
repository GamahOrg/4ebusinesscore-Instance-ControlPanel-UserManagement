using _4EPlatform_Domain.Models.Lookup;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class HomeSetting : InfoEntity
    {
        public int Index { get; set; }
        public bool IsChecked { get; set; }

        [ForeignKey("UserAssginLookup")]
        public Guid UserAssginLookupId { get; set; }
        [InverseProperty("HomeSette")]
        public User UserAssginLookup { get; set; }

        [ForeignKey("Lookup")]
        public Guid LookupId { get; set; }
        public LK_HomeSetting Lookup { get; set; }

    }
}
