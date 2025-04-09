using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ResuMate.Components.Models
{
    public class PersonalLetterModel : CvModel
    {      
        public string? Weaknesses { get; set; }
        public string? Strenghts { get; set; }      
        public string? CareerGoals { get; set; }                   
        public string? Hobbies { get; set; }
        public string CompanyName { get; set; }
        public string Motivation { get; set; }
        public string BusinessOverview { get; set; }

    }
}
