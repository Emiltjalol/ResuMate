namespace ResuMate.Components.Models
{
    public class PersonalLetterDto : CvDto
    {      
        public string? Weaknesses { get; set; }
        public string? Strenghts { get; set; }      
        public string? CareerGoals { get; set; }                   
        public List<string>? Hobbies { get; set; }
    }
}
