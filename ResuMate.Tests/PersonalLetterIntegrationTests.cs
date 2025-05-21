using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using ResuMate.Api;
using System.Text;
using System.Text.Json;
using Xunit;

namespace ResuMate.Tests
    {
        public class PersonalLetterIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
        {
            private readonly HttpClient _client;

            public PersonalLetterIntegrationTests(WebApplicationFactory<Program> factory)
            {

                var configuration = new ConfigurationBuilder()
                    .AddUserSecrets<PersonalLetterIntegrationTests>()
                    .Build();

                var apiKey = configuration["MY_API_KEY"];
               
            if (string.IsNullOrWhiteSpace(apiKey))
                    throw new Exception("MY_API_KEY saknas i user secrets.");

                _client = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                            { "MY_API_KEY", apiKey }
                    });
                    });
                }).CreateClient();
            }



            [Fact]
            public async Task PersonalLetterController_ShouldReturnLetter_FromOpenAI()
            {
                //Arrange

                var model = new
                {
                    Name = "Anna Testsson",
                    ProfessionalTitle = "Systemutvecklare",
                    CompanyName = "Tech AB",
                    YearsOfExperience = "3 års",
                    BusinessOverview = "utvecklar molnbaserade system",
                    AboutMe = "är engagerad och lösningsfokuserad",
                    YourValueToUs = "bidrar med struktur och energi",
                    WhyThisCompany = "ni verkar vara framåt och moderna",
                    Strenghts = "kommunikativ, analytisk",
                    Weaknesses = "ibland överambitiös",
                    CareerGoals = "bli tech lead",
                    Hobbies = "vandring, datorspel",
                    ExtraInfo = "har erfarenhet från både frontend och backend"
                };

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Act

                var response = await _client.PostAsync("/api/PersonalLetter", content);

                //Assert
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                Assert.Contains("Med vänliga hälsningar", result);
            }



            //[Fact]
            //public async Task Get_Endpoint_ReturnSuccessStatusCode()
            //{
            //    string url = "https://api.openai.com/v1/chat/completions";
            //    HttpResponseMessage response = await _client.GetAsync(url);
            //    response.EnsureSuccessStatusCode();
            //    Assert.True(response.IsSuccessStatusCode);
            //}
        }
    }


