using _4EPlatform_Domain.Models.Lookup;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{

    public class LanguageForm : InfoEntity
        {

            public bool NightMode { get; set; }
            public string? Image { get; set; }

            [ForeignKey("UserAssginLookup")]
            public Guid UserAssginLookupId { get; set; }
            [InverseProperty("LanguageForm")]
            public User UserAssginLookup { get; set; }

            [ForeignKey("Sitelanguage")]
            public Guid SitelanguageId { get; set; }
            public LK_LanguageForm Sitelanguage { get; set; }
        }
    }

