using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ResuMate.Data;
using ResuMate.Shared.Models;

namespace ResuMate.Services.CvServices
{
    public class DeleteCvService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public DeleteCvService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> DeleteCvAsync(int cvId, string userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Console.WriteLine($"Försöker ta bort CV med ID: {cvId} och UserId: {userId}");

                var cvToDelete = await context.GeneratedCvs
                    .FirstOrDefaultAsync(cv => cv.Id == cvId && cv.UserId == userId);

                if (cvToDelete != null)
                {
                    Console.WriteLine($"Hittade CV: {cvToDelete.FileName} (Id: {cvToDelete.Id})");

                    context.GeneratedCvs.Remove(cvToDelete);
                    var affectedRows = await context.SaveChangesAsync();
                    Console.WriteLine($"SaveChangesAsync returnerade: {affectedRows}");

                    return affectedRows > 0;
                }

                Console.WriteLine("CV:t hittades inte.");
                return false;
            }
        }



        public async Task<List<GeneratedCv>> GetUserGeneratedCvsAsync(string userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.GeneratedCvs
                    .Where(cv => cv.UserId == userId)
                    .ToListAsync();
            }
        }
    }

}

