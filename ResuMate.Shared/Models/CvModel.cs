namespace ResuMate.Shared.Models
{
    public class CvModel
    {
        public string? Name { get; set; }
        public string? ProfessionalTitle { get; set; }
        public string? Email { get; set; }        
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }        
        public string? AboutMe { get; set; }
        public string? ProfileImage { get; set; }
        public string? Languages { get; set; } 
        public List <ReferenceModel>? References { get; set; }     
        public List <string>? Skills { get; set; }
        public List<EducationModel>? Educations { get; set; }
        public List<ExperienceModel>? Experiences { get; set; }
        public List<string>? Certifications { get; set; }              
        
    }
}
