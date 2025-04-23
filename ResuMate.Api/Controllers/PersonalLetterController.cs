using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using ResuMate.Components.Models;

namespace ResuMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalLetterController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private string generatedLetter = "";
        public PersonalLetterController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonalLetterModel model)
        {

            var sb = new StringBuilder();
            sb.AppendLine($"Skriv ett professionellt personligt brev på svenska för {model.Name} som söker jobb som {model.ProfessionalTitle} på {model.CompanyName}.");
            sb.AppendLine($"Jag har {model.YearsOfExperience} erfarenhet inom yrket som {model.ProfessionalTitle}.");
            sb.AppendLine($"Företaget arbetar med: {model.BusinessOverview}");
            sb.AppendLine($"Om {model.Name}: {model.AboutMe}");
            sb.AppendLine($"Hur kan jag bidra till er verksamhet: {model.YourValueToUs}");
            sb.AppendLine($"Motivation: {model.WhyThisCompany}");
            sb.AppendLine($"Styrkor: {model.Strenghts}");
            sb.AppendLine($"Svagheter: {model.Weaknesses}");
            sb.AppendLine($"Karriärmål: {model.CareerGoals}");

            if (!string.IsNullOrWhiteSpace(model.Hobbies))
                sb.AppendLine($"Hobbies: {model.Hobbies}");

            if (!string.IsNullOrWhiteSpace(model.ExtraInfo))
                sb.AppendLine($"Extra information: {model.ExtraInfo}");
            
            sb.AppendLine("Använd all information ovan där det är relevant. Om 'Hobbies' eller 'Extra information' är angivet är det mycket viktigt att det vävs in naturligt i brevet.");

            
            sb.AppendLine("Du behöver inte ta med någon kontaktinformation angående mig eller företaget i fråga, för det kommer automatiskt genereras i ett annat steg.");

            string prompt = sb.ToString();


            var apiKey = _configuration["MY_API_KEY"];

            
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-4.1-mini",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var request = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            try
            {
                var chatGptResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", request);
                
                if (chatGptResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await chatGptResponse.Content.ReadAsStringAsync();

                    using var doc = JsonDocument.Parse(jsonResponse);
                    var reply = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    return Ok(reply);
                }
                else
                {                    
                    var errorResponse = await chatGptResponse.Content.ReadAsStringAsync();
                    generatedLetter = $"API-anropet misslyckades med statuskod {chatGptResponse.StatusCode}. Felmeddelande: {errorResponse}";
                    return BadRequest(generatedLetter);
                }
            }
            catch (Exception ex)
            {                
                generatedLetter = $"Något gick fel med API-anropet. Felmeddelande: {ex.Message}";
                return StatusCode(500, generatedLetter);
            }
        }
    }
}
