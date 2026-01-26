using _4EPlatform_Domain.Models.Lookup;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User : IdentityUser<Guid>
    {

        [ForeignKey("Creator")]
        public Guid? CreatedBy { set; get; }
        public User Creator { get; set; }
        public DateTime CreatedOn { set; get; }
        public Guid? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
        public State State { get; set; }

        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? ForthName { get; set; }
        public UserType UserType { get; set; }
        public UserGender UserGender { get; set; }
        public string? NationalId { get; set; }
        public string? Address { get; set; }

        public string? Mobile { get; set; }
        public string? TransactionNumber { get; set; }
        public string? DomainUrl { get; set; }

        [InverseProperty("Employee")]
        public virtual List<InformationJob> InformationJobs { get; set; }

        private ICollection<HomeSetting> quickAccessList;



        // to be added
        public string? ImageUrl { get; set; }
        public string? Nationality { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastLoginTime { get; set; }

        [InverseProperty("SystemUser")]
        public virtual List<LoginPermission> LoginPermissions { get; set; }
        public bool? IsEmployee { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual List<UserRole>? UserRoles { get; set; }
        public bool IsFirstPasswordChanged { set; get; }
        public DateTime? CodeExpirationDate { get; set; }
        public string? LastCodeSent { get; set; }

        [ForeignKey("JobName")]
        public Guid? JobNameId { get; set; }
        public LK_JobName JobName { get; set; }

        [InverseProperty("Users")]
        public Guid? DomainId { set; get; }
        public Domains Domain { get; set; }

        public bool AllowExternalRequest { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? DomainOrgName { get; set; }


        public bool? FromSeed { get; set; }
        [ForeignKey("Manager")]
        public Guid? ManagerId { set; get; }
        public User Manager { get; set; }
        public string? CommonNameAD { get; set; }
        public string? ManagerNameAD { get; set; }
        public bool? Locked { get; set; }


        [InverseProperty("SysetmUser")]
        public ICollection<ScientificDataAndExperiences> ScientificDataAndExperiences { get; set; }

        public ICollection<QuickAccess> QuickAccess { get; set; }
        public ICollection<HomeSetting> HomeSette { get; set; }
        public LanguageForm LanguageForm { get; set; }

        [InverseProperty("User")]
        public ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<UserExperience> UserExperiences { get; set; }

        [InverseProperty("User")]
        public ICollection<UserDelegation> CreatedDelegations { get; set; }

        [InverseProperty("DelegeatedToUser")]
        public ICollection<UserDelegation> AssignedToDelegations { get; set; }
        [InverseProperty("DelegeatedFromUser")]
        public ICollection<UserDelegation> AssignedFromDelegations { get; set; }

        [InverseProperty("Manager")]
        public ICollection<Group> Groups { get; set; }

        [InverseProperty("User")]
        public ICollection<Domains> UserDomains { get; set; }
        [InverseProperty("SystemUser")]
        public FunctionalData FunctionalData { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}