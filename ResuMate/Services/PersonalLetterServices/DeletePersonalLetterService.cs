using Microsoft.EntityFrameworkCore;
using ResuMate.Data;
using ResuMate.Shared.Models;

namespace ResuMate.Services.PersonalLetterServices
{
    public class DeletePersonalLetterService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public DeletePersonalLetterService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> DeleteLetterAsync(int letterId, string userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Console.WriteLine($"Försöker ta bort persnligt brev med ID: {letterId} och UserId: {userId}");

                var letterToDelete = await context.GeneratedPersonalLetters
                    .FirstOrDefaultAsync(cv => cv.Id == letterId && cv.UserId == userId);

                if (letterToDelete != null)
                {
                    Console.WriteLine($"Hittade personligt brev: {letterToDelete.FileName} (Id: {letterToDelete.Id})");

                    context.GeneratedPersonalLetters.Remove(letterToDelete);
                    var affectedRows = await context.SaveChangesAsync();
                    Console.WriteLine($"SaveChangesAsync returnerade: {affectedRows}");

                    return affectedRows > 0;
                }

                Console.WriteLine("Personliga brevet hittades inte.");
                return false;
            }
        }


        public async Task<List<GeneratedPersonalLetter>> GetUserGeneratedlettersAsync(string userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.GeneratedPersonalLetters
                    .Where(letter => letter.UserId == userId)
                    .ToListAsync();
            }
        }

    }

}

