using _4EPlatform_Domain.Models.Lookup;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models
{
	public class InformationJob : InfoEntity
	{
		public Guid OrgId { set; get; }
		public UserKind UserKind { get; set; }

		[ForeignKey("Employee")]
		public Guid? EmployeeId { set; get; }
		[InverseProperty("InformationJobs")]
		public virtual User Employee { set; get; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public JobTime? JobTime { get; set; }
		public DateTime HiringDate { get; set; }



		[ForeignKey("JobDegree")]
		public Guid? JobDegreeId { get; set; }
		public LK_JobDegree JobDegree { get; set; }

		[ForeignKey("JobRole")]
		public Guid? JobRoleId { get; set; }
		public LK_JobRole JobRole { get; set; }

	}
}
