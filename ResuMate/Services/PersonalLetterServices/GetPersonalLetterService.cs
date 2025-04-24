using Microsoft.EntityFrameworkCore;
using ResuMate.Data;
using ResuMate.Shared.Models;


namespace ResuMate.Services.PersonalLetterServices
{
    public class GetPersonalLetterService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public GetPersonalLetterService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<GeneratedPersonalLetter>> GetUserPersonalLettersAsync(string userId)
        {            
            await using var db = _contextFactory.CreateDbContext();
            
            var generatedLetters = await db.GeneratedPersonalLetters
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            
            foreach (var letter in generatedLetters)
            {
                if (letter.PersonalLetterData != null)
                {
                    letter.Base64CvData = Convert.ToBase64String(letter.PersonalLetterData);
                }
            }

            return generatedLetters;
        }
    }
}
