using Microsoft.EntityFrameworkCore;
using ResuMate.Data;
using ResuMate.Shared.Models;

namespace ResuMate.Services.CvServices
{
    public class GetCvService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public GetCvService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<GeneratedCv>> GetUserGeneratedCvsAsync(string userId)
        {
            await using var db = _contextFactory.CreateDbContext();

            var cvs = await db.GeneratedCvs
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            foreach (var cv in cvs)
            {
                if (cv.CvData != null)
                {
                    cv.Base64CvData = Convert.ToBase64String(cv.CvData);
                }
            }

            return cvs;
        }
    }
}
