namespace ResuMate.Components.Models
{
    public class CvDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int BirthYear { get; set; }
        public required string Adress { get; set; }
        public required string PostralCode { get; set; }
        public required string City { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Weaknesses { get; set; }
        public string? Strenghts { get; set; }
        public string? AboutMe { get; set; }
        public string? Languages { get; set; }
        public string? CareerGoals { get; set; }
        public string? References { get; set; }
        public List<EducationDto>? Educations { get; set; }
        public List<ExperienceDto>? Experiences { get; set; }
        public List<string>? Certifications { get; set; }
        public List<string>? Hobbies { get; set; }

    }
}
