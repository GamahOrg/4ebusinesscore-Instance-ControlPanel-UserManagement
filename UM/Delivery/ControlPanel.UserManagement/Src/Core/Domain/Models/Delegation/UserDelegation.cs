using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class UserDelegation : InfoEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApprovedTo { get; set; }
        public string? ApprovedToNotes { get; set; }
        public bool? IsApprovedFrom { get; set; }
        public string? ApprovedFromNotes { get; set; }
        public bool? FromAdmin { get; set; }

        [ForeignKey("DelegeatedToUser")]
        public Guid? DelegeatedToUserId { get; set; }
        [ForeignKey("DelegeatedFromUser")]
        public Guid? DelegeatedFromUserId { get; set; }
        [InverseProperty("AssignedToDelegations")]
        public User DelegeatedToUser { get; set; }
        [InverseProperty("AssignedFromDelegations")]
        public User DelegeatedFromUser { get; set; }
        public ICollection<DelegationRolePermession> DelegationRolePermessions { get; set; }

    }
}
