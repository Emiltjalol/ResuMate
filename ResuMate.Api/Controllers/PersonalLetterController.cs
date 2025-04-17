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

            string prompt = $@"
                Skriv ett professionellt personligt brev på svenska för {model.Name} som söker jobb som {model.ProfessionalTitle} på {model.CompanyName}.
                Jag har {model.YearsOfExperience} år erfarenhet inom yrket som {model.ProfessionalTitle}
                Företaget arbetar med: {model.BusinessOverview}
                Om {model.Name}: {model.AboutMe}
                Hur kan jag bidra till er verksamhet: {model.YourValueToUs}
                Motivation: {model.WhyThisCompany}
                Styrkor: {model.Strenghts}
                Svagheter: {model.Weaknesses}
                Karriärmål: {model.CareerGoals}
                Hobbies: {model.Hobbies}
                Extra information: {model.ExtraInfo}
                Du behöver inte ta med någon kontaktinformation angående mig eller företaget i fråga för det kommer automatiskt genereras i ett annat steg.
                Om det är något fält som är tomt så ska den informationen inte vara med i brevet som t.ex hobbys eller extrainfo.
               
                ";
            
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
