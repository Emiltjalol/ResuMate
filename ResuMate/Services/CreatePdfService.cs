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
        public void Compose(IDocumentContainer container, CvDto cvDto, List<EducationDto> educationList, List<ExperienceDto> experienceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Row(row =>
                {
                    
                    row.RelativeItem(0.4f).Background(Colors.Grey.Darken3).Padding(30).Column(col =>
                    {                        
                        var imagePath = "wwwroot/Images/GenericIMG.jpg";
                        var imageBytes = File.ReadAllBytes(imagePath);
                        col.Item().AlignCenter().Height(120).Width(120).Image(imageBytes).FitWidth().FitHeight();

                        col.Item().Text(cvDto.Name).FontSize(22).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().PaddingBottom(15).Text(cvDto.Yrkestitel).FontSize(14).Italic().FontColor(Colors.White).AlignCenter();

                        col.Item().PaddingLeft(5).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.White);
                        col.Item().PaddingTop(10).Text(cvDto.AboutMe).FontSize(12).FontColor(Colors.White).LineHeight(1.5f);

                        col.Item().PaddingTop(20).Text("KONTAKTINFO").FontSize(14).Bold().FontColor(Colors.White);
                        col.Item().Text($"📞 {cvDto.PhoneNumber}").FontSize(12).FontColor(Colors.White);
                        col.Item().Text($"📧 {cvDto.Email}").FontSize(12).FontColor(Colors.White);
                        col.Item().Text($"📍 {cvDto.Adress}, {cvDto.PostalCode} {cvDto.City}").FontSize(12).FontColor(Colors.White);

                        col.Item().PaddingTop(20).Text("KOMPETENSER").FontSize(14).Bold().FontColor(Colors.White);
                        var skillText = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                        col.Item().Text(skillText).FontSize(12).FontColor(Colors.White);
                    });
                    
                    row.RelativeItem(0.6f).Column(col =>
                    {
                        col.Item().Background(Colors.Grey.Lighten4).Padding(20).Column(expCol =>
                        {
                            expCol.Item().Text("ARBETSLIVSERFARENHET").FontSize(16).Bold().FontColor(Colors.Black);

                            foreach (var exp in experienceList)
                            {
                                expCol.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                    .FontSize(13).Bold().FontColor(Colors.Black);
                                expCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f).FontColor(Colors.Black);
                            }
                        });

                        col.Item().Background(Colors.Grey.Lighten3).Padding(20).Column(eduCol =>
                        {
                            eduCol.Item().Text("UTBILDNING").FontSize(16).Bold().FontColor(Colors.Black);

                            foreach (var edu in educationList)
                            {
                                eduCol.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold().FontColor(Colors.Black);
                                eduCol.Item().Text(edu.Degree).FontSize(12).FontColor(Colors.Black);
                            }
                        });
                    });
                });                
            });
        }


        public void Compose2(IDocumentContainer container, CvDto cvDto, List<EducationDto> educationList, List<ExperienceDto> experienceList)
        {

            container.Page(page =>
            {
               
                page.Size(PageSizes.A4);
                
                page.Margin(40);

                page.Content().Row(row =>
                {
                    
                    row.RelativeItem(0.4f).Padding(5).Background(Colors.Grey.Lighten3).Column(col =>
                    {
                        
                        col.Item().AlignCenter().Column(imageCol =>
                        {
                            var imagePath = "wwwroot/Images/GenericIMG.jpg";
                            var imageBytes = File.ReadAllBytes(imagePath);

                            imageCol.Item().Height(200).PaddingTop(20).Container().AlignCenter().AlignMiddle().Image(imageBytes)
                                .FitWidth()
                                .FitHeight();


                            imageCol.Item().Height(10); 
                        });

                        
                        col.Item().AlignCenter().Text(cvDto.Name).FontSize(26).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().AlignCenter().Text(cvDto.Yrkestitel).FontSize(16).Italic();

                        
                        col.Item().PaddingLeft(5).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        col.Item().PaddingLeft(5).Text(cvDto.AboutMe).FontSize(12).LineHeight(1.5f);

                        col.Item().PaddingVertical(10).LineHorizontal(1);
                        
                        col.Spacing(10); 

                        col.Item().AlignBottom().Column(contactCol =>
                        {
                            contactCol.Item().PaddingLeft(5).PaddingBottom(5).Text("KONTAKTA MIG PÅ:").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                            contactCol.Item().PaddingLeft(5).Text($"📧 {cvDto.Email}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📞 {cvDto.PhoneNumber}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📍 {cvDto.Adress}").FontSize(12);
                            contactCol.Item().PaddingLeft(22).Text($" {cvDto.PostalCode}, {cvDto.City}").FontSize(12);
                        });

                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        col.Item().PaddingLeft(5).Text("KOMPETENSER").FontSize(15).Bold().FontColor(Colors.Grey.Darken2);

                        var skillText = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                        col.Item().PaddingLeft(5).Text(skillText).FontSize(10);                        

                    });

                    
                    row.RelativeItem(0.6f).Column(col =>
                    {
                        
                        col.Item().Text("ARBETSLIVSERFARENHET").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);
                        
                        foreach (var exp in experienceList)
                        {
                            col.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                .FontSize(13).Bold();
                            col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                        }

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        
                        col.Item().Text("UTBILDNING").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().PaddingVertical(5).LineHorizontal(1);

                        
                        foreach (var edu in educationList)
                        {
                            col.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear}) ").FontSize(13).Bold();
                            col.Item().Text($"{edu.Degree}").FontSize(12);
                        }
                    });
                });

                
            });
        }



        public void Compose3(IDocumentContainer container, CvDto cvDto, List<EducationDto> educationList, List<ExperienceDto> experienceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Column(mainCol =>
                {              
                    mainCol.Item().Height(100).Background(Colors.Green.Darken2).AlignCenter().AlignMiddle().Text(cvDto.Name)
                        .FontSize(28).Bold().FontColor(Colors.White);

                    mainCol.Item().Height(30).Background(Colors.Green.Lighten1).AlignCenter().AlignMiddle().Text(cvDto.Yrkestitel)
                        .FontSize(14).FontColor(Colors.White);

                    mainCol.Item().Row(row =>
                    {                        
                        row.RelativeItem(0.35f).Padding(20).Background(Colors.Grey.Lighten3).Column(col =>
                        {
                          
                            col.Item().AlignCenter().Image("wwwroot/Images/GenericIMG.jpg").FitWidth();
                            
                            col.Item().PaddingTop(10).Text("OM MIG").FontSize(14).Bold();
                            col.Item().Text(cvDto.AboutMe).FontSize(12).LineHeight(1.4f);

                            col.Item().PaddingTop(15).LineHorizontal(1);

                            
                            col.Item().PaddingTop(10).Text("KONTAKT").FontSize(14).Bold();
                            col.Item().Text($"📞 {cvDto.PhoneNumber}").FontSize(10);
                            col.Item().Text($"📧 {cvDto.Email}").FontSize(10);
                            col.Item().Text($"📍 {cvDto.Adress}").FontSize(10);

                            col.Item().PaddingLeft(5).Text("KOMPETENSER").FontSize(15).Bold().FontColor(Colors.Grey.Darken2);

                            var skillText = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                            col.Item().PaddingLeft(5).Text(skillText).FontSize(10);

                        });

                        
                        row.RelativeItem(0.65f).Padding(20).Column(col =>
                        {
                            
                            col.Item().Text("ERFARENHET").FontSize(18).Bold().FontColor(Colors.Green.Darken2);
                            col.Item().PaddingBottom(5).LineHorizontal(1);

                            foreach (var exp in experienceList)
                            {
                                col.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                    .FontSize(13).Bold();
                                col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                            }

                            col.Item().PaddingVertical(15).LineHorizontal(1);
                            
                            col.Item().Text("UTBILDNING").FontSize(18).Bold().FontColor(Colors.Green.Darken2);
                            col.Item().PaddingBottom(5).LineHorizontal(1);

                            foreach (var edu in educationList)
                            {
                                col.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold();
                                col.Item().Text(edu.Degree).FontSize(12);
                            }
                        });
                    });
                });
            });
        }

    }
}

        



//public byte[] GeneratePdf(CvTemplate templateType)
//{
//    var document = Document.Create(container =>
//    {
//        switch (templateType)
//        {
//            case CvTemplate.Classic:
//                Compose(container);
//                break;
//            case CvTemplate.Modern:
//                Compose2(container);
//                break;
//            default:
//                Compose(container); // Standardmall om inget annat anges
//                break;
//        }
//    });

//    using (var memoryStream = new MemoryStream())
//    {
//        document.GeneratePdf(memoryStream);
//        return memoryStream.ToArray();
//    }
//}




