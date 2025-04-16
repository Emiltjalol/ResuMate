using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResuMate.Shared.Models;

namespace ResuMate.Services
{
    public class CreateCvPdfService
    {
        public CvModel CvModel { get; set; }
        public EducationModel EducationModel { get; set; }
        public ExperienceModel ExperienceModel { get; set; }
        public ReferenceModel ReferenceModel { get; set; }

        public CreateCvPdfService(CvModel cvDto, EducationModel educationDto, ExperienceModel experienceDto, ReferenceModel referenceDto)
        {
            CvModel = cvDto;
            EducationModel = educationDto;
            ExperienceModel = experienceDto;
            ReferenceModel = referenceDto;
        }

        public void Template1(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Row(row =>
                {
                    row.RelativeItem(0.4f).Background(Colors.Grey.Darken3).Padding(30).Column(col =>
                    {
                        var imagePath = "wwwroot/Images/img1.jpg";
                        var imageBytes = File.ReadAllBytes(imagePath);
                        col.Item().AlignCenter().Height(150).Image(imageBytes).FitWidth().FitHeight();

                        col.Item().Text(cvDto.Name).FontSize(22).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().PaddingBottom(15).Text(cvDto.ProfessionalTitle).FontSize(14).Italic().FontColor(Colors.White).AlignCenter();

                        col.Item().Text("KONTAKTINFO").FontSize(16).Bold().FontColor(Colors.White).Underline();
                        col.Item().PaddingTop(8).Text($"{cvDto.PhoneNumber}").FontSize(14).FontColor(Colors.White);
                        col.Item().Text($"{cvDto.Email}").FontSize(14).FontColor(Colors.White);
                        col.Item().Text($"{cvDto.Address}, ").FontSize(14).FontColor(Colors.White);
                        col.Item().Text($"{cvDto.PostalCode} {cvDto.City}").FontSize(12).FontColor(Colors.White);

                        col.Item().PaddingTop(20).PaddingBottom(8).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.White).Underline();
                        col.Item().Text(cvDto.AboutMe).FontSize(14).FontColor(Colors.White).LineHeight(1.4f);

                    });

                    row.RelativeItem(0.6f).ExtendVertical().Background(Colors.White).Padding(20).Column(col =>
                    {
                        col.Item().Column(expCol =>
                        {
                            expCol.Item().Text("ARBETSLIVSERFARENHET").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var exp in experienceList)
                            {
                                expCol.Item().PaddingTop(8).Text($"{exp.JobTitle} - {exp.Company}").FontSize(13).Bold().FontColor(Colors.Black);
                                expCol.Item().Text($"({exp.StartYear} - {exp.EndYear})").FontSize(13).Bold().FontColor(Colors.Black);
                                expCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f).FontColor(Colors.Black);
                            
                            }
                        });

                        col.Item().Column(eduCol =>
                        {
                            eduCol.Item().PaddingTop(30).Text("UTBILDNING").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var edu in educationList)
                            {
                                eduCol.Item().PaddingTop(8).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold().FontColor(Colors.Black);
                                eduCol.Item().Text(edu.Degree).FontSize(12).FontColor(Colors.Black);
                            }
                        });

                        col.Item().Column(skillCol =>
                        {
                            skillCol.Item().PaddingTop(30).PaddingBottom(10).Text("ÖVRIGA KOMPETENSER").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var skill in cvDto.Skills)
                            {
                                skillCol.Item().Text($"• {skill}").FontSize(12).FontColor(Colors.Black);
                            }
                        });

                        col.Item().Column(referencesCol =>
                        {
                            referencesCol.Item().PaddingTop(30).PaddingBottom(10).Text("MINA REFERENSER").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var reference in referenceList)
                            {
                                referencesCol.Item().Text($"• {reference.Name} - {reference.Relation} - {reference.PhoneNumber}").FontSize(12).FontColor(Colors.Black);
                            }

                        });
                    });
                });
            });

        }
        public void Template2(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {

            container.Page(page =>
            {

                page.Size(PageSizes.A4);

                page.Margin(20);

                page.Content().Row(row =>
                {

                    row.RelativeItem(0.4f).Padding(5).Background(Colors.Grey.Lighten3).Column(col =>
                    {

                        col.Item().AlignCenter().Column(imageCol =>
                        {
                            var imagePath = "wwwroot/Images/img1.jpg";
                            var imageBytes = File.ReadAllBytes(imagePath);

                            imageCol.Item().Height(200).PaddingTop(20).Container().AlignCenter().AlignMiddle().Image(imageBytes)
                                .FitWidth()
                                .FitHeight();


                            imageCol.Item().Height(10);
                        });


                        col.Item().AlignCenter().Text(cvDto.Name).FontSize(26).Bold().FontColor(Colors.Red.Darken1);
                        col.Item().AlignCenter().Text(cvDto.ProfessionalTitle).FontSize(16).Italic();


                        col.Item().PaddingLeft(5).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                        col.Item().PaddingLeft(5).Text(cvDto.AboutMe).FontSize(12).LineHeight(1.5f);

                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        col.Spacing(10);

                        col.Item().AlignBottom().Column(contactCol =>
                        {
                            contactCol.Item().PaddingLeft(5).PaddingBottom(5).Text("KONTAKTA MIG PÅ:").FontSize(16).Bold().FontColor(Colors.Grey.Darken2);
                            contactCol.Item().PaddingLeft(5).Text($"📧 {cvDto.Email}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📞 {cvDto.PhoneNumber}").FontSize(12);
                            contactCol.Item().PaddingLeft(5).Text($"📍 {cvDto.Address}").FontSize(12);
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

                        col.Item().PaddingTop(20).Text("UTBILDNING").FontSize(20).Bold().FontColor(Colors.Red.Darken1);
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
        public void Template3(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Column(mainCol =>
                {
                    mainCol.Item().Height(80).Background(Colors.Green.Darken2).AlignCenter().AlignMiddle().Text(cvDto.Name)
                        .FontSize(28).Bold().FontColor(Colors.White);

                    mainCol.Item().Height(40).Background(Colors.Green.Lighten1).AlignCenter().AlignMiddle().Text(cvDto.ProfessionalTitle)
                        .FontSize(18).FontColor(Colors.White);

                    mainCol.Item().Row(row =>
                    {
                        row.RelativeItem(0.4f).ExtendVertical().Background(Colors.Grey.Lighten3).Column(col =>
                        {

                            var imagePath = "wwwroot/Images/img1.jpg";
                            var imageBytes = File.ReadAllBytes(imagePath);
                            col.Item().PaddingTop(30).AlignCenter().Height(150).Image(imageBytes).FitWidth().FitHeight();


                            col.Item().PaddingLeft(20).PaddingBottom(10).PaddingTop(30).Text("KONTAKTA MIG").FontSize(16).FontColor(Colors.Green.Darken2).Bold().Underline();

                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.PhoneNumber}").FontSize(14);
                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.Email}").FontSize(14);
                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.Address},").FontSize(14);
                            col.Item().PaddingLeft(20).PaddingRight(10).PaddingBottom(10).Text($"{cvDto.PostalCode}, {cvDto.City}").FontSize(14);

                            col.Item().PaddingLeft(20).PaddingBottom(10).Text("KORT OM MIG").FontColor(Colors.Green.Darken2).FontSize(16).Bold().Underline();
                            col.Item().PaddingLeft(20).PaddingRight(10).PaddingBottom(10).Text(cvDto.AboutMe).FontSize(14).LineHeight(1.4f);

                            col.Item().PaddingLeft(20).Text("REFERENSER").FontColor(Colors.Green.Darken2).FontSize(16).Bold().Underline();

                            if (referenceList.Count == 0)
                            {
                                col.Item().PaddingLeft(20).PaddingTop(10).Text("Lämnas på begäran").FontSize(14).FontColor(Colors.Black);
                            }
                            else
                            {
                                foreach (var reference in referenceList)
                                {
                                    col.Item().PaddingLeft(20).PaddingRight(10).PaddingTop(10).Text($"{reference.Name}").FontSize(14).Bold();
                                    col.Item().PaddingLeft(20).PaddingRight(10).Text($"{reference.Relation}  {reference.PhoneNumber}").FontSize(14);
                                }
                            }



                        });

                        row.RelativeItem(0.65f).Padding(20).Column(col =>
                        {
                            col.Item().Text("ERFARENHET").FontSize(18).Bold().FontColor(Colors.Green.Darken2).Underline();

                            foreach (var exp in experienceList)
                            {
                                col.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                    .FontSize(13).Bold();
                                col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                            }

                            col.Item().PaddingTop(10).Text("UTBILDNING").FontSize(18).Bold().FontColor(Colors.Green.Darken2).Underline();

                            foreach (var edu in educationList)
                            {
                                col.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold();
                                col.Item().Text(edu.Degree).FontSize(12);
                            }

                            col.Item().PaddingTop(10).PaddingBottom(10).Text("ÖVRIGA KOMPETENSER").FontSize(18).Bold().FontColor(Colors.Green.Darken2).Underline();

                            if (cvDto.Skills != null && cvDto.Skills.Any())
                            {
                                if (cvDto.Skills.Count <= 6)
                                {
                                    var skillText = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                                    col.Item().PaddingLeft(5).Text(skillText).FontSize(12);
                                }
                                else
                                {
                                    var mid = (int)Math.Ceiling(cvDto.Skills.Count / 2.0);
                                    var leftSkills = cvDto.Skills.Take(mid);
                                    var rightSkills = cvDto.Skills.Skip(mid);

                                    col.Item().Row(row =>
                                    {
                                        row.RelativeItem().Column(leftCol =>
                                        {
                                            foreach (var skill in leftSkills)
                                                leftCol.Item().Text($"• {skill}").FontSize(12);
                                        });

                                        row.RelativeItem().Column(rightCol =>
                                        {
                                            foreach (var skill in rightSkills)
                                                rightCol.Item().Text($"• {skill}").FontSize(12);
                                        });
                                    });
                                }
                            }
                        });
                    });
                });
            });
        }
        public void Template4(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Times new Roman"));
                page.Content().Column(column =>
                {
                    //column.Item().PaddingBottom(30).Text("CV").FontColor(Colors.Red.Darken1).FontSize(30).AlignCenter();
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(leftCol =>
                        {
                            leftCol.Item().AlignLeft().Text($"{cvDto.Name}").FontSize(12);
                            leftCol.Item().AlignLeft().Text($"{cvDto.Address}").FontSize(12);
                            leftCol.Item().AlignLeft().Text($"{cvDto.PostalCode} {cvDto.City}").FontSize(12);
                        });

                        row.RelativeItem().Column(rightCol =>
                        {
                            rightCol.Item().AlignRight().Text($"{cvDto.Email}").FontSize(12);
                            rightCol.Item().AlignRight().Text($"{cvDto.PhoneNumber}").FontSize(12);
                        });
                    });
                    column.Item().PaddingTop(20).Text("Profil").FontSize(16).Bold();
                    column.Item().Text(cvDto.AboutMe).FontSize(12).LineHeight(1.4f);

                    column.Item().PaddingVertical(15).LineHorizontal(1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem(1).Column(leftCol =>
                        {
                            leftCol.Item().Text("Arbetslivserfarenheter").FontSize(16).Bold();
                        });
                        row.RelativeItem(2).Column(midCol =>
                        {
                            foreach (var exp in experienceList)
                            {
                                midCol.Item().Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})").FontSize(14).Bold();
                                midCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                            }
                        });

                    });

                    column.Item().PaddingVertical(15).LineHorizontal(1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem(1).Column(leftCol =>
                        {
                            leftCol.Item().Text("Utbildningar").FontSize(16).Bold();
                        });
                        row.RelativeItem(2).Column(midCol =>
                        {
                            foreach (var edu in educationList)
                            {
                                midCol.Item().Text($"{edu.School} ({edu.StartYear} - {edu.EndYear})").FontSize(14).Bold();
                                midCol.Item().Text($"{edu.Degree}").FontSize(12).LineHeight(1.4f);
                            }
                        });

                    });

                    column.Item().PaddingVertical(15).LineHorizontal(1);

                    // TODO kolla hur du vill göra med detta 

                    //column.Item().Row(row =>
                    //{
                    //    row.RelativeItem(1).Column(leftCol =>
                    //    {
                    //        leftCol.Item().Text("Språkkunskaper").FontSize(16).Bold();
                    //    });
                    //    row.RelativeItem(2).Column(midCol =>
                    //    {
                    //        midCol.Item().Text("Svenska:    Modersmål.").FontSize(12).LineHeight(1.4f);
                    //        midCol.Item().Text("Engelska:   Flytande i tal, skrift och förståelse.").FontSize(12).LineHeight(1.4f);

                    //    });

                    //});

                    //column.Item().PaddingVertical(15).LineHorizontal(1);

                    column.Item().Row(row =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Column(leftCol =>
                            {
                                leftCol.Item().Text("Övriga meriter").FontSize(16).Bold();
                            });

                            row.RelativeItem(2).Column(midCol =>
                            {
                                var skills = cvDto.Skills;

                                if (skills.Count > 6)
                                {
                                    int mid = skills.Count / 2;

                                    midCol.Item().Row(row =>
                                    {
                                        row.RelativeItem().Column(left =>
                                        {
                                            for (int i = 0; i < mid; i++)
                                            {
                                                left.Item().Text($"• {skills[i]}").FontSize(12).LineHeight(1.4f);
                                            }
                                        });

                                        row.RelativeItem().Column(right =>
                                        {
                                            for (int i = mid; i < skills.Count; i++)
                                            {
                                                right.Item().Text($"• {skills[i]}").FontSize(12).LineHeight(1.4f);
                                            }
                                        });
                                    });
                                }
                                else
                                {
                                    var skillText = string.Join("\n", skills.Select(skill => $"• {skill}"));
                                    midCol.Item().Text(skillText).FontSize(12).LineHeight(1.4f);
                                }
                            });
                        });

                        column.Item().PaddingVertical(15).LineHorizontal(1);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Column(leftCol =>
                            {
                                leftCol.Item().PaddingLeft(20).PaddingTop(10).Text("Referenser").FontSize(16).Bold();
                            });

                            row.RelativeItem(2).Column(midCol =>
                            {
                                if (referenceList.Count == 0)
                                {
                                    midCol.Item().PaddingTop(10).Text("Lämnas på begäran").FontSize(14).FontColor(Colors.Black);
                                }
                                else
                                {
                                    foreach (var reference in referenceList)
                                    {
                                        midCol.Item().Text($"{reference.Name}").FontSize(14).Bold();
                                        midCol.Item().Text($"{reference.Relation}  {reference.PhoneNumber}").FontSize(12);
                                    }
                                }
                            });
                        });
                    });
                });
            });
        }
        public void Template5(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Column(mainCol =>
                {                  
                    mainCol.Item().Height(100).Background(Colors.Blue.Darken2).AlignCenter().AlignMiddle().Column(header =>
                    {
                        header.Item().Text(cvDto.Name).FontSize(28).Bold().FontColor(Colors.White);
                        header.Item().Text(cvDto.ProfessionalTitle).FontSize(14).FontColor(Colors.White);
                    });

                    mainCol.Item().Row(row =>
                    {
                        
                        row.RelativeItem(0.35f).Padding(20).Background(Colors.Grey.Lighten3).Column(leftCol =>
                        {
                            leftCol.Item().AlignCenter().Image("wwwroot/Images/img1.jpg").FitWidth();
                            leftCol.Item().PaddingTop(10).Text("Kontakt").FontSize(14).Bold();
                            leftCol.Item().Text($"📞 {cvDto.PhoneNumber}").FontSize(10);
                            leftCol.Item().Text($"📧 {cvDto.Email}").FontSize(10);
                            leftCol.Item().Text($"📍 {cvDto.Address}, {cvDto.PostalCode} {cvDto.City}").FontSize(10);

                            leftCol.Item().PaddingTop(10).LineHorizontal(1);

                            leftCol.Item().PaddingTop(10).Text("Om Mig").FontSize(14).Bold();
                            leftCol.Item().Text(cvDto.AboutMe).FontSize(12).LineHeight(1.4f);

                            leftCol.Item().PaddingTop(10).LineHorizontal(1);

                            leftCol.Item().PaddingTop(10).Text("Kompetenser").FontSize(14).Bold();
                            var skills = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                            leftCol.Item().Text(skills).FontSize(10);
                        });

                        
                        row.RelativeItem(0.65f).Padding(20).Column(rightCol =>
                        {
                            
                            rightCol.Item().Text("Erfarenhet").FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            foreach (var exp in experienceList)
                            {
                                rightCol.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                    .FontSize(13).Bold();
                                rightCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                            }

                            
                            rightCol.Item().PaddingTop(20).Text("Utbildning").FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            foreach (var edu in educationList)
                            {
                                rightCol.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold();
                                rightCol.Item().Text(edu.Degree).FontSize(12);
                            }

                            
                            rightCol.Item().PaddingTop(20).Text("Språkkunskaper").FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            rightCol.Item().PaddingTop(10).Text(cvDto.Languages).FontSize(12);

                            
                            rightCol.Item().PaddingTop(20).Text("Referenser").FontSize(18).Bold().FontColor(Colors.Blue.Darken2);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            rightCol.Item().PaddingTop(10).Text("Lämnas gärna vid ett personligt möte.").FontSize(12);
                        });
                    });
                });
            });
        }
        public void Template6(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);

                page.Content().Row(row =>
                {                    
                    row.ConstantItem(180).Background(Colors.Black).Padding(20).Column(leftCol =>
                    {
                        leftCol.Item().AlignCenter().Image("wwwroot/Images/img1.jpg").FitWidth();

                        leftCol.Item().PaddingTop(15).Text(cvDto.Name)
                            .FontSize(18).Bold().FontColor(Colors.White).AlignCenter();
                        leftCol.Item().Text(cvDto.ProfessionalTitle)
                            .FontSize(12).FontColor(Colors.White).AlignCenter();

                        leftCol.Item().PaddingTop(10).LineHorizontal(1);

                        leftCol.Item().PaddingTop(10).PaddingBottom(8).Text("Kontakt").FontSize(14).Bold().FontColor(Colors.White).Underline();
                        leftCol.Item().Text($"{cvDto.PhoneNumber}").FontSize(12).FontColor(Colors.White);
                        leftCol.Item().Text($"{cvDto.Email}").FontSize(12).FontColor(Colors.White);
                        leftCol.Item().Text($"{cvDto.Address}, {cvDto.PostalCode} {cvDto.City}").FontSize(12).FontColor(Colors.White);

                        leftCol.Item().PaddingTop(15).LineHorizontal(1);

                        leftCol.Item().PaddingTop(10).PaddingBottom(8).Text("Övriga meriter").FontSize(14).Bold().FontColor(Colors.White).Underline();
                        var skills = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                        leftCol.Item().Text(skills).FontSize(12).FontColor(Colors.White);
                    });

                   
                    row.RelativeItem().Padding(30).Column(rightCol =>
                    {
                        rightCol.Item().Text("Profil").FontSize(18).Bold().FontColor(Colors.Black).Underline();
                        rightCol.Item().PaddingTop(5).Text(cvDto.AboutMe).FontSize(12).LineHeight(1.4f);

                        //rightCol.Item().PaddingTop(15).LineHorizontal(1);

                        rightCol.Item().PaddingTop(10).Text("Erfarenhet").FontSize(18).Bold().FontColor(Colors.Black).Underline();
                        //rightCol.Item().PaddingVertical(5).LineHorizontal(1);
                        foreach (var exp in experienceList)
                        {
                            rightCol.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                .FontSize(14).Bold();
                            rightCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.4f);
                        }

                        //rightCol.Item().PaddingTop(15).LineHorizontal(1);

                        rightCol.Item().PaddingTop(10).Text("Utbildning").FontSize(18).Bold().FontColor(Colors.Black).Underline();
                        //rightCol.Item().PaddingVertical(5).LineHorizontal(1);
                        foreach (var edu in educationList)
                        {
                            rightCol.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                .FontSize(14).Bold();
                            rightCol.Item().Text(edu.Degree).FontSize(12);
                        }

                        //rightCol.Item().PaddingTop(15).LineHorizontal(1);

                        rightCol.Item().PaddingTop(10).Text("Referenser").FontSize(18).Bold().FontColor(Colors.Black).Underline();

                        // Om det finns referenser
                        if (referenceList != null && referenceList.Any())
                        {
                            foreach (var reference in referenceList)
                            {
                                rightCol.Item().PaddingTop(8).Text($"{reference.Name}").FontSize(14).Bold();
                                rightCol.Item().Text($"{reference.Relation} - {reference.PhoneNumber}").FontSize(12);
                            }
                        }
                        
                        else
                        {
                            rightCol.Item().PaddingTop(10).Text("Lämnas gärna vid ett personligt möte.").FontSize(12);
                        }

                    });
                });
            });
        }
        public void Template7(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                page.Content().Column(column =>
                {
                    
                    column.Item().Height(100).Background(Colors.Blue.Medium)
                        .AlignCenter().AlignMiddle().Column(header =>
                        {
                            header.Item().Text(cvDto.Name)
                                .FontSize(28).Bold().FontColor(Colors.White).AlignCenter();
                            header.Item().Text(cvDto.ProfessionalTitle)
                                .FontSize(18).FontColor(Colors.White).AlignCenter();
                        });

                    column.Item().PaddingTop(10).Row(row =>
                    {
                        
                        row.RelativeItem(0.4f).PaddingRight(10).Column(leftCol =>
                        {
                            leftCol.Item().PaddingBottom(5).Text("Kontakt").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                            leftCol.Item().Text($"📞 {cvDto.PhoneNumber}").FontSize(10);
                            leftCol.Item().Text($"📧 {cvDto.Email}").FontSize(10);
                            leftCol.Item().Text($"📍 {cvDto.Address}, {cvDto.PostalCode} {cvDto.City}").FontSize(10);
                        });

                        // Höger sektion - Om mig
                        row.RelativeItem(0.6f).Column(rightCol =>
                        {
                            rightCol.Item().PaddingBottom(5).Text("Kort om mig").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                            rightCol.Item().Text(cvDto.AboutMe).FontSize(10).LineHeight(1.3f);
                        });
                    });



                    column.Item().PaddingTop(10).LineHorizontal(1);

                    
                    column.Item().PaddingTop(10).Text("Tidigare erfarenheter").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().Column(expCol =>
                    {
                        foreach (var exp in experienceList)
                        {
                            expCol.Item().PaddingTop(5).Row(row =>
                            {
                                row.ConstantItem(8).Background(Colors.Blue.Medium).Height(8).Width(8).AlignMiddle();
                                row.RelativeItem().PaddingLeft(8).Column(col =>
                                {
                                    col.Item().Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                        .FontSize(12).Bold();
                                    col.Item().Text(exp.JobDescription).FontSize(10).LineHeight(1.3f);
                                });
                            });
                        }
                    });

                    column.Item().PaddingTop(10).LineHorizontal(1);

                    column.Item().PaddingTop(10).Text("Utbildningar").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().Column(eduCol =>
                    {
                        foreach (var edu in educationList)
                        {
                            eduCol.Item().PaddingTop(5).Row(row =>
                            {
                                row.ConstantItem(8).Background(Colors.Blue.Medium).Height(8).Width(8).AlignMiddle();
                                row.RelativeItem().PaddingLeft(8).Column(col =>
                                {
                                    col.Item().Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                        .FontSize(12).Bold();
                                    col.Item().Text(edu.Degree).FontSize(10);
                                });
                            });
                        }
                    });

                    column.Item().PaddingTop(10).LineHorizontal(1);



                    
                    column.Item().PaddingTop(10).Text("Övriga kompetenser och meriter").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().PaddingTop(5).Row(skillRow =>
                    {
                        int columns = 2;
                        int skillsPerColumn = (int)Math.Ceiling(cvDto.Skills.Count / (double)columns);

                        for (int i = 0; i < columns; i++)
                        {
                            skillRow.RelativeItem(1f / columns).Column(col =>
                            {
                                foreach (var skill in cvDto.Skills.Skip(i * skillsPerColumn).Take(skillsPerColumn))
                                {
                                    col.Item().Text($"•  {skill}").FontSize(10);
                                }
                            });
                        }
                    });

                    column.Item().PaddingTop(10).LineHorizontal(1);

                             

                    column.Item().PaddingTop(10).Text("Referenser").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().Column(expCol =>
                    {
                        foreach (var reference in referenceList)
                        {
                            expCol.Item().PaddingTop(5).Row(row =>
                            {
                                row.ConstantItem(8).Background(Colors.Blue.Medium).Height(8).Width(8).AlignMiddle();
                                row.RelativeItem().PaddingLeft(8).Column(col =>
                                {
                                    col.Item().Text(text =>
                                    {
                                        text.Span(reference.Name).Bold(); // Namnet i bold
                                        text.Span($" - {reference.Relation} - {reference.PhoneNumber}").FontSize(10); // Resten i vanlig stil
                                    });
                                });
                            });
                        }
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




