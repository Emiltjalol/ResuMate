using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ResuMate.Components.Models;



namespace ResuMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalLetterController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonalLetterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonalLetterModel model)
        {
            string prompt = $@"
Skriv ett professionellt personligt brev på svenska för {model.Name} som söker jobb som {model.ProfessionalTitle} på {model.CompanyName}.
Företaget arbetar med: {model.BusinessOverview}
Om {model.Name}: {model.AboutMe}
Motivation: {model.Motivation}
Styrkor: {model.Strenghts}
Svagheter: {model.Weaknesses}
Karriärmål: {model.CareerGoals}
Hobbies: {model.Hobbies}
";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "DIN_OPENAI_API_KEY");

            var body = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "user", content = prompt }
            }
            };

            var request = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var chatGptResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", request);
            var jsonResponse = await chatGptResponse.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var reply = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(reply);
        }
    }

}
