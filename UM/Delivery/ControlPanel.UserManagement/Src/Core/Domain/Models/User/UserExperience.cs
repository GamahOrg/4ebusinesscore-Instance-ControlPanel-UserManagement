namespace Domain.Models
{
    public class UserExperience : Entity 
	{

		public Guid UserExperienceId { set; get; }

		public string ExpertName { get; set; }
		public long YearsOfExperience { get; set; }
		public string Specialization { get; set; }
		public string PlaceOfExperince { get; set; }

	}
}