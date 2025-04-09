namespace ResuMate.Components.Models
{
    public class CvDto
    {
        public string? Name { get; set; }
        public string? Yrkestitel { get; set; }
        public string? Email { get; set; }        
        public string? Adress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }        
        public string? AboutMe { get; set; }
        public string? ProfileImage { get; set; }
        public string? Languages { get; set; } 
        public List <ReferenceDto> References { get; set; }     
        public List <string>? Skills { get; set; }
        public List<EducationDto>? Educations { get; set; }
        public List<ExperienceDto>? Experiences { get; set; }
        public List<string>? Certifications { get; set; }              
        
    }
}
