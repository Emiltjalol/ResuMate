using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResuMate.Shared.Models
{
    public class GeneratedPersonalLetter
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public byte[] PersonalLetterData { get; set; } = Array.Empty<byte>();

        public string? FileName { get; set; }

        public string? Base64CvData { get; set; }
    }
}
