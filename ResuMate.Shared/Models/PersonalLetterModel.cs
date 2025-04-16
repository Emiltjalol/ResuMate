using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ResuMate.Shared.Models;

namespace ResuMate.Components.Models
{
    public class PersonalLetterModel : CvModel
    {      
        public string? Weaknesses { get; set; }
        public string? Strenghts { get; set; }      
        public string? CareerGoals { get; set; }                   
        public string? Hobbies { get; set; }
        public string? CompanyName { get; set; }        
        public string? BusinessOverview { get; set; }        
        public string? WhyThisCompany { get; set; }
        public string? YourValueToUs { get; set; }
        public string? YearsOfExperience { get; set; }
        public string? ExtraInfo { get; set; } = string.Empty;



    }
}
