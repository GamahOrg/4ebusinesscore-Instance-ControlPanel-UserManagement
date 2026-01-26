using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ScientificDataAndExperiences : InfoEntity
    {
        public string? QualificationType { get; set; }
        public string? University { get; set; }
        public string? Specialization { get; set; }
        public DateTime? Date { get; set; }
        public string? ScientificExpertise { get; set; }
        public string? Attachments { get; set; }

        [ForeignKey("SysetmUser")]
        public Guid? SysetmUserId { set; get; }
        public User SysetmUser { get; set; }
    }
}
