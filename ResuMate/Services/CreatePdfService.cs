using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResuMate.Components.Models;

namespace ResuMate.Services
{
    public class CreatePdfService
    {
        public CvDto CvDto { get; set; }
        public EducationDto EducationDto { get; set; }
        public ExperienceDto ExperienceDto { get; set; }

        public CreatePdfService(CvDto cvDto, EducationDto educationDto, ExperienceDto experienceDto)
        {
            CvDto = cvDto;
            EducationDto = educationDto;
            ExperienceDto = experienceDto;
        }

        CvDto cv = new CvDto
        {
            Name = "Emil Öhrström",
            Email = "Emil.ohrstrom@hotmail.com",
            BirthYear = 1993,
            Adress = "Bränntorp Björnmossen",
            PostalCode = "61892",
            City = "Kolmården",
            PhoneNumber = "073-123 45 67",
            Weaknesses = "Kan ibland vara för perfektionistisk i mitt arbete",
            Strenghts = "Snabb inlärningsförmåga, problemlösning, lagarbete",
            AboutMe = "Jag bor med min fru och två barn. Jag är en passionerad programmerare med ett stort intresse för teknik och innovation. På fritiden gillar jag att koda egna projekt och hålla mig uppdaterad om de senaste teknologierna.",
            Languages = "Svenska och engelska",
            CareerGoals = "Att bli världens bästa programmerare och skapa innovativa lösningar",
            References = "Finns vid behov",

            Educations = new List<EducationDto>()
    {
        new EducationDto()
        {
            School = "Campus Nyköping",
            Degree = "YH-examen",
            Specialization = "Systemutvecklare .Net",
            StartYear = 2023,
            EndYear = 2025
        },
        new EducationDto()
        {
            School = "Teknikgymnasiet Norrköping",
            Degree = "Gymnasieexamen",
            Specialization = "IT & Programmering",
            StartYear = 2009,
            EndYear = 2012
        },
           new EducationDto()
        {
            School = "En till utbildning för utfyllnad",
            Degree = "En random utbildning som inte finns",
            Specialization = "Livsmedel och restaurang",
            StartYear = 1999,
            EndYear = 2000
        }
    },

            Experiences = new List<ExperienceDto>()
    {
        new ExperienceDto()
        {
            JobTitle = "Idrottsplatsvaktmästare",
            Company = "Pema Partner",
            JobDescription = "Ansvarade för skötsel av idrottsanläggningar, inklusive gräsklippning, ismaskinskörning och underhåll av fotbollsplaner.",
            StartYear = 2020,
            EndYear = 2023
        },
        new ExperienceDto()
        {
            JobTitle = "Kyl- och värmepumpstekniker",
            Company = "Assemblin",
            JobDescription = "Servade och installerade kyl- och värmepumpsanläggningar, både i butiker och hos privatpersoner.",
            StartYear = 2018,
            EndYear = 2020
        },
        new ExperienceDto()
        {
            JobTitle = "IT-supporttekniker",
            Company = "Norrköpings IT-Service",
            JobDescription = "Hjälpte företag och privatpersoner med IT-support, nätverksinstallationer och felsökning av datorer och programvara.",
            StartYear = 2016,
            EndYear = 2018
        }
    },

            Skills = new List<string>()
    {
        "C#, .NET Core, ASP.NET",
        "SQL, Entity Framework, MongoDB",
        "JavaScript, React, Blazor",
        "REST API, GraphQL",
        "Docker, Kubernetes",
        "Agil utveckling, Scrum",
        "Problemlösning & analytiskt tänkande",
        "TDD & enhetstester"
    },

            Hobbies = new List<string>()
    {
        "Koda egna projekt på fritiden",
        "Gaming & e-sport",
        "Löpning och träning",
        "Följa teknikutveckling & läsa om AI"
    }
        };


        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Sätt A4-storlek på sidan
                page.Size(PageSizes.A4);

                // Öka marginalerna för att skapa mer utrymme runt om
                page.Margin(40);

                page.Content().Row(row =>
                {
                    // 📌 Vänsterkolumn (Profil, Bakgrund, Kompetenser, Kontakt)
                    row.RelativeItem(0.4f).Padding(5).Background(Colors.Grey.Lighten3).Column(col =>
                    {
                        // Profilbild (Placeholder)
                        col.Item().AlignCenter().Column(imageCol =>
                        {
                            var imagePath = "wwwroot/Images/img1.jpg"; // Ange vägen till din bild
                            var imageBytes = File.ReadAllBytes(imagePath); // Läs in bilden som en byte-array

                            imageCol.Item().Image(imageBytes).FitWidth();
                            imageCol.Item().Height(10); // Skapa utrymme under bilden
                        });
                        // Namn & Titel
                        col.Item().AlignCenter().Text(cv.Name).FontSize(26).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().AlignCenter().Text("Systemutvecklare").FontSize(16).Italic();

                        col.Item().PaddingVertical(10).LineHorizontal(1); // Skiljelinje

                        // Om Mig
                        col.Item().PaddingLeft(5).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        col.Item().PaddingLeft(5).Text(cv.AboutMe).FontSize(12).LineHeight(1.5f);

                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        // Kompetenser
                        //col.Item().PaddingTop(15).Text("KOMPETENSER").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);

                        //foreach (var skill in cv.Skills)
                        //{
                        //    col.Item().Text($"• {skill}").FontSize(12);
                        //}

                        //col.Item().PaddingVertical(10).LineHorizontal(1);

                        // Kontakt

                        col.Spacing(10); // Skapar utrymme mellan sektionerna

                        col.Item().AlignBottom().Column(contactCol =>
                        {
                            contactCol.Item().PaddingLeft(5).PaddingBottom(5).Text("KONTAKTA MIG PÅ:").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                            contactCol.Item().PaddingLeft(5).Text($"📧 {cv.Email}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📞 {cv.PhoneNumber}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📍 {cv.Adress}").FontSize(12);
                            contactCol.Item().PaddingLeft(22).Text($" {cv.PostalCode}, {cv.City}").FontSize(12);
                        });
                    });

                    // 📌 Högerkolumn (Erfarenhet & Utbildning + Styrkor & Svagheter)
                    row.RelativeItem(0.6f).Column(col =>
                    {
                        // 🏢 Arbetslivserfarenhet
                        col.Item().Text("ARBETSLIVSERFARENHET").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        foreach (var exp in cv.Experiences)
                        {
                            col.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                .FontSize(14).Bold();
                            col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                        }

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        // 🎓 Utbildning
                        col.Item().Text("UTBILDNING").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        foreach (var edu in cv.Educations)
                        {
                            col.Item().PaddingTop(10).Text($"{edu.Degree} i {edu.Specialization}").FontSize(14).Bold();
                            col.Item().Text($"{edu.School} ({edu.StartYear} - {edu.EndYear})").FontSize(12);
                        }

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        col.Item().Text("Kompetenser").FontSize(20).Bold().FontColor(Colors.Red.Darken1);

                        foreach (var skill in cv.Skills)
                        {
                            col.Item().Text($"• {skill}").FontSize(12);
                        }

                        //// 💪 Styrkor & Svagheter
                        //col.Item().Text("STYRKOR & SVAGHETER").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        //col.Item().PaddingVertical(5).LineHorizontal(1);

                        //// Styrkor
                        //col.Item().PaddingTop(10).Text("STYRKOR").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        //col.Item().Text("• Snabblärd och anpassningsbar").FontSize(12);
                        //col.Item().Text("• Problemlösningsorienterad").FontSize(12);
                        //col.Item().Text("• Teamspelare med stark kommunikationsförmåga").FontSize(12);

                        //// Svagheter
                        //col.Item().PaddingTop(10).Text("SVAGHETER").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        //col.Item().Text("• Kan ibland vara för självkritisk").FontSize(12);
                        //col.Item().Text("• Har en tendens att fastna i detaljer").FontSize(12);
                        //col.Item().Text("• Jobbar på att bli bättre på att delegera arbetsuppgifter").FontSize(12);
                    });
                });

                // 📌 Footer
                page.Footer().Height(40).AlignCenter().Text("Genererad av ResuMate :)").FontSize(12).Italic();
            });
        }
        public void Compose2(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Sätt A4-storlek på sidan
                page.Size(PageSizes.A4);

                // Öka marginalerna för att skapa mer utrymme runt om
                page.Margin(40);

                page.Content().Row(row =>
                {
                    // 📌 Vänsterkolumn (Profil, Bakgrund, Kompetenser, Kontakt)
                    row.RelativeItem(0.4f).Padding(5).Background(Colors.Red.Lighten3).Column(col =>
                    {
                        // Profilbild (Placeholder)
                        col.Item().AlignCenter().Column(imageCol =>
                        {
                            var imagePath = "wwwroot/Images/img1.jpg"; // Ange vägen till din bild
                            var imageBytes = File.ReadAllBytes(imagePath); // Läs in bilden som en byte-array

                            imageCol.Item().Image(imageBytes).FitWidth();
                            imageCol.Item().Height(10); // Skapa utrymme under bilden
                        });
                        // Namn & Titel
                        col.Item().AlignCenter().Text(cv.Name).FontSize(26).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().AlignCenter().Text("Systemutvecklare").FontSize(16).Italic();

                        col.Item().PaddingVertical(10).LineHorizontal(1); // Skiljelinje

                        // Om Mig
                        col.Item().PaddingLeft(5).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        col.Item().PaddingLeft(5).Text(cv.AboutMe).FontSize(12).LineHeight(1.5f);

                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        // Kompetenser
                        //col.Item().PaddingTop(15).Text("KOMPETENSER").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);

                        //foreach (var skill in cv.Skills)
                        //{
                        //    col.Item().Text($"• {skill}").FontSize(12);
                        //}

                        //col.Item().PaddingVertical(10).LineHorizontal(1);

                        // Kontakt

                        col.Spacing(10); // Skapar utrymme mellan sektionerna

                        col.Item().AlignBottom().Column(contactCol =>
                        {
                            contactCol.Item().PaddingLeft(5).PaddingBottom(5).Text("KONTAKTA MIG PÅ:").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                            contactCol.Item().PaddingLeft(5).Text($"📧 {cv.Email}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📞 {cv.PhoneNumber}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📍 {cv.Adress}").FontSize(12);
                            contactCol.Item().PaddingLeft(22).Text($" {cv.PostalCode}, {cv.City}").FontSize(12);
                        });
                    });

                    // 📌 Högerkolumn (Erfarenhet & Utbildning + Styrkor & Svagheter)
                    row.RelativeItem(0.6f).Column(col =>
                    {
                        // 🏢 Arbetslivserfarenhet
                        col.Item().Text("ARBETSLIVSERFARENHET").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        foreach (var exp in cv.Experiences)
                        {
                            col.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                .FontSize(14).Bold();
                            col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                        }

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        // 🎓 Utbildning
                        col.Item().Text("UTBILDNING").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        foreach (var edu in cv.Educations)
                        {
                            col.Item().PaddingTop(10).Text($"{edu.Degree} i {edu.Specialization}").FontSize(14).Bold();
                            col.Item().Text($"{edu.School} ({edu.StartYear} - {edu.EndYear})").FontSize(12);
                        }

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        col.Item().Text("Kompetenser").FontSize(20).Bold().FontColor(Colors.Red.Darken1);

                        foreach (var skill in cv.Skills)
                        {
                            col.Item().Text($"• {skill}").FontSize(12);
                        }

                        //// 💪 Styrkor & Svagheter
                        //col.Item().Text("STYRKOR & SVAGHETER").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        //col.Item().PaddingVertical(5).LineHorizontal(1);

                        //// Styrkor
                        //col.Item().PaddingTop(10).Text("STYRKOR").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        //col.Item().Text("• Snabblärd och anpassningsbar").FontSize(12);
                        //col.Item().Text("• Problemlösningsorienterad").FontSize(12);
                        //col.Item().Text("• Teamspelare med stark kommunikationsförmåga").FontSize(12);

                        //// Svagheter
                        //col.Item().PaddingTop(10).Text("SVAGHETER").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        //col.Item().Text("• Kan ibland vara för självkritisk").FontSize(12);
                        //col.Item().Text("• Har en tendens att fastna i detaljer").FontSize(12);
                        //col.Item().Text("• Jobbar på att bli bättre på att delegera arbetsuppgifter").FontSize(12);
                    });
                });

                // 📌 Footer
                page.Footer().Height(40).AlignCenter().Text("Genererad av ResuMate :)").FontSize(12).Italic();
            });
        }



        public byte[] GeneratePdf(CvTemplate templateType)
        {
            var document = Document.Create(container =>
            {
                switch (templateType)
                {
                    case CvTemplate.Classic:
                        Compose(container);
                        break;
                    case CvTemplate.Modern:
                        Compose2(container);
                        break;
                    default:
                        Compose(container); // Standardmall om inget annat anges
                        break;
                }
            });

            using (var memoryStream = new MemoryStream())
            {
                document.GeneratePdf(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
