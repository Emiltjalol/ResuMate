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
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));
                page.Margin(0);

                page.Content().Row(row =>
                {
                    row.RelativeItem(0.4f).Background(Colors.Grey.Darken3).Padding(15).Column(col =>
                    {
                        if (!string.IsNullOrEmpty(cvDto.ProfileImage))
                        {
                            var imageBytes = Convert.FromBase64String(cvDto.ProfileImage.Split(',')[1]);
                            col.Item().PaddingBottom(10).PaddingTop(15).AlignCenter().Height(200).Image(imageBytes).FitWidth().FitHeight();
                        }                        

                        col.Item().Text(cvDto.Name).FontSize(22).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().PaddingBottom(40).Text(cvDto.ProfessionalTitle).FontSize(16).Italic().FontColor(Colors.White).AlignCenter();

                        col.Item().PaddingBottom(10).Text("KONTAKTINFO").FontSize(16).Bold().FontColor(Colors.White).Underline();
                        col.Item().Text($"{cvDto.PhoneNumber}").FontSize(12).FontColor(Colors.White);
                        col.Item().PaddingTop(3).Text($"{cvDto.Email}").FontSize(12).FontColor(Colors.White);
                        col.Item().PaddingTop(3).Text($"{cvDto.Address}, ").FontSize(12).FontColor(Colors.White);
                        col.Item().PaddingTop(3).Text($"{cvDto.PostalCode} {cvDto.City}").FontSize(12).FontColor(Colors.White);

                        col.Item().PaddingTop(20).PaddingBottom(8).Text("MIN BAKGRUND").FontSize(16).Bold().FontColor(Colors.White).Underline();
                        col.Item().Text(cvDto.AboutMe).FontSize(12).FontColor(Colors.White).LineHeight(1.2f);

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
                                expCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.2f).FontColor(Colors.Black);

                            }
                        });

                        col.Item().Column(eduCol =>
                        {
                            eduCol.Item().PaddingTop(15).Text("UTBILDNING").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var edu in educationList)
                            {
                                eduCol.Item().PaddingTop(8).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold().FontColor(Colors.Black);
                                eduCol.Item().Text(edu.Degree).FontSize(12).LineHeight(1.2f).FontColor(Colors.Black);
                            }
                        });

                        col.Item().Column(skillCol =>
                        {
                            skillCol.Item().PaddingTop(15).PaddingBottom(10).Text("ÖVRIGA KOMPETENSER").FontSize(16).Bold().FontColor(Colors.Black).Underline();
                            if (cvDto.Skills != null)
                            {
                                foreach (var skill in cvDto.Skills)
                                {
                                    skillCol.Item().PaddingTop(2).Text($"• {skill}").FontSize(12).FontColor(Colors.Black);
                                }
                            }

                        });

                        col.Item().Column(referencesCol =>
                        {
                            referencesCol.Item().PaddingTop(30).PaddingBottom(10).Text("MINA REFERENSER").FontSize(16).Bold().FontColor(Colors.Black).Underline();

                            foreach (var reference in referenceList)
                            {
                                referencesCol.Item().PaddingTop(5).Text(text =>
                                {

                                    text.Span(reference.Name).FontSize(12).FontColor(Colors.Black).Bold();
                                    text.Span($" - {reference.Relation} - {reference.PhoneNumber}").FontSize(12).FontColor(Colors.Black);

                                });
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
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));
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

                            if (!string.IsNullOrEmpty(cvDto.ProfileImage))
                            {
                                var imageBytes = Convert.FromBase64String(cvDto.ProfileImage.Split(',')[1]);
                                col.Item().PaddingTop(30).AlignCenter().Height(150).Image(imageBytes).FitWidth().FitHeight();
                            }
                            


                            col.Item().PaddingLeft(20).PaddingBottom(10).PaddingTop(30).Text("KONTAKTA MIG").FontSize(16).FontColor(Colors.Green.Darken2).Bold().Underline();

                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.PhoneNumber}").FontSize(12);
                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.Email}").FontSize(12);
                            col.Item().PaddingLeft(20).PaddingRight(10).Text($"{cvDto.Address},").FontSize(12);
                            col.Item().PaddingLeft(20).PaddingRight(10).PaddingBottom(10).Text($"{cvDto.PostalCode}, {cvDto.City}").FontSize(12);

                            col.Item().PaddingLeft(20).PaddingBottom(10).Text("KORT OM MIG").FontColor(Colors.Green.Darken2).FontSize(16).Bold().Underline();
                            col.Item().PaddingLeft(20).PaddingRight(10).PaddingBottom(10).Text(cvDto.AboutMe).FontSize(12).LineHeight(1.3f);

                            col.Item().PaddingLeft(20).Text("REFERENSER").FontColor(Colors.Green.Darken2).FontSize(16).Bold().Underline();

                            if (referenceList.Count == 0)
                            {
                                col.Item().PaddingLeft(20).PaddingTop(10).Text("Lämnas på begäran").FontSize(14).FontColor(Colors.Black);
                            }
                            else
                            {
                                foreach (var reference in referenceList)
                                {
                                    col.Item().PaddingLeft(20).PaddingRight(10).PaddingTop(10).Text($"{reference.Name}").FontSize(12).Bold();
                                    col.Item().PaddingLeft(20).PaddingRight(10).Text($"{reference.Relation}  {reference.PhoneNumber}").FontSize(12);
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
                                col.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.3f);
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
        public void Template3(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));

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
                    if (cvDto.Skills != null && cvDto.Skills.Count > 0)
                    {
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
                    }


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
      
        public void Template4(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));
                page.Margin(0);

                page.Content().Column(mainCol =>
                {
                    mainCol.Item().Height(100).Background(Colors.BlueGrey.Darken3).AlignCenter().AlignMiddle().Column(header =>
                    {
                        header.Item().Text(cvDto.Name).FontSize(28).Bold().FontColor(Colors.White);
                        header.Item().Text(cvDto.ProfessionalTitle).FontSize(14).FontColor(Colors.White);
                    });

                    mainCol.Item().ExtendVertical().Row(row =>
                    {
                        row.RelativeItem(0.35f).Extend().Background(Colors.Grey.Lighten3).Padding(20).Column(leftCol =>
                        {                            
                            if (!string.IsNullOrEmpty(cvDto.ProfileImage))
                            {
                                var imageBytes = Convert.FromBase64String(cvDto.ProfileImage.Split(',')[1]);
                                leftCol.Item().AlignCenter().Image(imageBytes).FitWidth();
                            }                            

                            leftCol.Item().PaddingTop(10).PaddingBottom(10).Text("Kontakt").FontSize(14).Bold().FontColor(Colors.Black);
                            leftCol.Item().Text($"📞 {cvDto.PhoneNumber}").FontSize(10);
                            leftCol.Item().Text($"📧 {cvDto.Email}").FontSize(10);
                            leftCol.Item().Text($"📍 {cvDto.Address}, {cvDto.PostalCode} {cvDto.City}").FontSize(10);

                            leftCol.Item().PaddingTop(10).LineHorizontal(1);

                            leftCol.Item().PaddingTop(10).Text("Om Mig").FontSize(14).Bold().FontColor(Colors.Black);
                            leftCol.Item().PaddingTop(10).Text(cvDto.AboutMe).FontSize(12).LineHeight(1.3f);

                            if (cvDto.Skills != null && cvDto.Skills.Count > 0)
                            {
                                leftCol.Item().PaddingTop(10).LineHorizontal(1);
                                leftCol.Item().PaddingTop(10).PaddingBottom(10).Text("Kompetenser").FontSize(14).Bold().FontColor(Colors.Black);
                                var skills = string.Join("\n", cvDto.Skills.Select(skill => $"• {skill}"));
                                leftCol.Item().Text(skills).FontSize(10);
                            }
                        });

                        row.RelativeItem(0.65f).Extend().Padding(20).Column(rightCol =>
                        {
                            rightCol.Item().Text("Erfarenhet").FontSize(18).Bold().FontColor(Colors.BlueGrey.Darken3);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            foreach (var exp in experienceList)
                            {
                                rightCol.Item().PaddingTop(10).Text($"{exp.JobTitle} - {exp.Company} ({exp.StartYear} - {exp.EndYear})")
                                    .FontSize(13).Bold();
                                rightCol.Item().Text(exp.JobDescription).FontSize(12).LineHeight(1.3f);
                            }

                            rightCol.Item().PaddingTop(20).Text("Utbildning").FontSize(18).Bold().FontColor(Colors.BlueGrey.Darken3);
                            rightCol.Item().PaddingVertical(10).LineHorizontal(1);
                            foreach (var edu in educationList)
                            {
                                rightCol.Item().PaddingTop(10).Text($"{edu.Specialization} - {edu.School} ({edu.StartYear} - {edu.EndYear})")
                                    .FontSize(13).Bold();
                                rightCol.Item().Text(edu.Degree).FontSize(12);
                            }

                            rightCol.Item().Column(referencesCol =>
                            {
                                referencesCol.Item().PaddingTop(20).Text("Referenser").FontSize(18).Bold().FontColor(Colors.BlueGrey.Darken3);
                                referencesCol.Item().PaddingVertical(10).LineHorizontal(1);

                                if (referenceList == null || !referenceList.Any())
                                {
                                    referencesCol.Item().PaddingTop(8).Text("Lämnas gärna vid förfrågan.").FontSize(12).FontColor(Colors.Black);
                                }
                                else
                                {
                                    foreach (var reference in referenceList)
                                    {
                                        referencesCol.Item().PaddingTop(8).Text(text =>
                                        {
                                            text.Span(reference.Name).FontSize(12).FontColor(Colors.Black).Bold();
                                            text.Span($" - {reference.Relation} - {reference.PhoneNumber}").FontSize(12).FontColor(Colors.Black);
                                        });
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
                page.Margin(25);
                page.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Grey.Darken4).FontFamily("Calibri"));

                page.Content().Element(e => e.Column(col =>
                {

                    col.Item().Row(row =>
                    {
                        row.RelativeItem(0.6f).Column(headerCol =>
                        {
                            headerCol.Item().Text(cvDto.Name).FontSize(28).Bold().FontColor(Colors.Black);
                            headerCol.Item().Text(cvDto.ProfessionalTitle).FontSize(14).Italic().FontColor(Colors.Grey.Darken2);
                        });

                    });


                    col.Item().Padding(12).Background(Colors.White).Column(contactCol =>
                    {
                        contactCol.Item().Text("Kontaktinformation").FontSize(12).Bold().FontColor(Colors.Black);
                        contactCol.Item().PaddingTop(5).Text($"📞 {cvDto.PhoneNumber}");
                        contactCol.Item().Text($"📧 {cvDto.Email}");
                        contactCol.Item().Text($"📍 {cvDto.Address}, {cvDto.PostalCode} {cvDto.City}");
                    });


                    col.Item().Padding(12).Background(Colors.White).Column(aboutCol =>
                    {
                        aboutCol.Item().Text("Om Mig").FontSize(12).Bold().FontColor(Colors.Black);
                        aboutCol.Item().PaddingTop(5).Text(cvDto.AboutMe).LineHeight(1.3f);
                    });


                    col.Item().Padding(12).Background(Colors.White).Column(expCol =>
                    {
                        expCol.Item().Text("Erfarenhet").FontSize(12).Bold().FontColor(Colors.Black);

                        foreach (var exp in experienceList)
                        {
                            expCol.Item().PaddingTop(6).Text($"{exp.JobTitle} – {exp.Company} ({exp.StartYear} - {exp.EndYear})").Bold();
                            expCol.Item().Text(exp.JobDescription).LineHeight(1.3f).FontSize(10).FontColor(Colors.Grey.Darken2);
                        }
                    });


                    col.Item().Padding(12).Background(Colors.White).Column(eduCol =>
                    {
                        eduCol.Item().Text("Utbildning").FontSize(12).Bold().FontColor(Colors.Black);

                        foreach (var edu in educationList)
                        {
                            eduCol.Item().PaddingTop(6).Text($"{edu.Specialization} – {edu.School} ({edu.StartYear} - {edu.EndYear})").Bold();
                            eduCol.Item().Text(edu.Degree).FontSize(10).FontColor(Colors.Grey.Darken2);
                        }
                    });


                    col.Item().Padding(12).Background(Colors.White).Column(refCol =>
                    {
                        refCol.Item().Text("Referenser").FontSize(12).Bold().FontColor(Colors.Black);

                        if (referenceList != null && referenceList.Any())
                        {
                            foreach (var reference in referenceList)
                            {
                                refCol.Item().PaddingTop(5).Text($"{reference.Name} – {reference.Relation} – {reference.PhoneNumber}");
                            }
                        }
                        else
                        {
                            refCol.Item().PaddingTop(5).Text("Lämnas gärna vid begäran.").FontColor(Colors.Grey.Darken3);
                        }
                    });


                    if (cvDto.Skills != null && cvDto.Skills.Any())
                    {
                        col.Item().Padding(12).Background(Colors.White).Column(skillCol =>
                        {
                            skillCol.Item().Text("Kompetenser").FontSize(12).Bold().FontColor(Colors.Black);
                            foreach (var skill in cvDto.Skills)
                            {
                                skillCol.Item().Text($"• {skill}").FontSize(10).FontColor(Colors.Grey.Darken2);
                            }
                        });
                    }
                }));
            });
        }
        public void Template6(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Grey.Darken3).FontFamily("Calibri"));


                page.Content().Element(e => e.Column(col =>
                {

                    col.Item().Row(row =>
                    {
                        row.RelativeItem(0.7f).PaddingBottom(10).Column(headerCol =>
                        {
                            headerCol.Item().Text(cvDto.Name).FontSize(30).Bold();
                            headerCol.Item().Text(cvDto.ProfessionalTitle).FontSize(16).Italic().FontColor(Colors.Grey.Darken2);
                        });
                    });


                    col.Item().PaddingVertical(5).Border(1).Background(Colors.White).Padding(10).Column(contactCol =>
                    {
                        contactCol.Item().Text("Kontakt").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);
                        contactCol.Item().PaddingTop(3).Text($"📞 {cvDto.PhoneNumber}");
                        contactCol.Item().Text($"📧 {cvDto.Email}");
                        contactCol.Item().Text($"📍 {cvDto.Address}, {cvDto.PostalCode} {cvDto.City}");
                    });


                    col.Item().PaddingTop(8).Border(1).Background(Colors.White).Padding(10).Column(aboutCol =>
                    {
                        aboutCol.Item().Text("Om Mig").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);
                        aboutCol.Item().PaddingTop(3).Text(cvDto.AboutMe).LineHeight(1.3f);
                    });


                    col.Item().PaddingTop(8).Border(1).Background(Colors.White).Padding(10).Column(expCol =>
                    {
                        expCol.Item().Text("Erfarenhet").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);

                        foreach (var exp in experienceList)
                        {
                            expCol.Item().PaddingTop(5).Text($"{exp.JobTitle} – {exp.Company} ({exp.StartYear} - {exp.EndYear})").Bold();
                            expCol.Item().Text(exp.JobDescription).LineHeight(1.3f);
                        }
                    });


                    col.Item().PaddingTop(8).Border(1).Background(Colors.White).Padding(10).Column(eduCol =>
                    {
                        eduCol.Item().Text("Utbildning").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);

                        foreach (var edu in educationList)
                        {
                            eduCol.Item().PaddingTop(5).Text($"{edu.Specialization} – {edu.School} ({edu.StartYear} - {edu.EndYear})").Bold();
                            eduCol.Item().Text(edu.Degree);
                        }
                    });


                    if (cvDto.Skills != null && cvDto.Skills.Any())
                    {
                        col.Item().PaddingTop(8).Border(1).Background(Colors.White).Padding(10).Column(skillCol =>
                        {
                            skillCol.Item().PaddingBottom(8).Text("Kompetenser").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);
                            foreach (var skill in cvDto.Skills)
                            {
                                skillCol.Item().PaddingBottom(1).Text($"• {skill}");
                            }
                        });
                    }

                    col.Item().PaddingTop(8).Border(1).Background(Colors.White).Padding(10).Column(refCol =>
                    {
                        refCol.Item().Text("Referenser").FontSize(12).Bold().FontColor(Colors.Blue.Darken2);

                        if (referenceList != null && referenceList.Any())
                        {
                            foreach (var reference in referenceList)
                            {
                                refCol.Item().PaddingTop(3).Text($"{reference.Name} – {reference.Relation} – {reference.PhoneNumber}");
                            }
                        }
                        else
                        {
                            refCol.Item().PaddingTop(3).Text("Lämnas gärna vid begäran.");
                        }
                    });
                }));
            });
        }
        public void Template7(IDocumentContainer container, CvModel cvDto, List<EducationModel> educationList, List<ExperienceModel> experienceList, List<ReferenceModel> referenceList)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));
                page.Content().Column(column =>
                {
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
                    column.Item().Text(cvDto.AboutMe).FontSize(12).LineHeight(1.3f);

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
                                midCol.Item().PaddingBottom(5).Text(exp.JobDescription).FontSize(12).LineHeight(1.3f);
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
                                midCol.Item().PaddingBottom(5).Text($"{edu.Degree}").FontSize(12).LineHeight(1.3f);
                            }
                        });

                    });

                    column.Item().PaddingVertical(15).LineHorizontal(1);


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

                                if (skills != null && skills.Count > 7)
                                {
                                    int mid = skills.Count / 2;

                                    midCol.Item().PaddingRight(5).Row(row =>
                                    {
                                        row.RelativeItem().Column(left =>
                                        {
                                            for (int i = 0; i < mid; i++)
                                            {
                                                left.Item().Text($"• {skills[i]}").FontSize(12).LineHeight(1.3f);
                                            }
                                        });

                                        row.RelativeItem().Column(right =>
                                        {
                                            for (int i = mid; i < skills.Count; i++)
                                            {
                                                right.Item().Text($"• {skills[i]}").FontSize(12).LineHeight(1.3f);
                                            }
                                        });
                                    });
                                }
                                else
                                {
                                    if (skills != null)
                                    {
                                        var skillText = string.Join("\n", skills.Select(skill => $"• {skill}"));
                                        midCol.Item().Text(skillText).FontSize(12).LineHeight(1.3f);
                                    }
                                }
                            });
                        });

                        column.Item().PaddingVertical(15).LineHorizontal(1);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Column(leftCol =>
                            {
                                leftCol.Item().PaddingTop(10).Text("Referenser").FontSize(16).Bold();
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
                                        midCol.Item().PaddingBottom(5).Text($"{reference.Relation}  {reference.PhoneNumber}").FontSize(12);
                                    }
                                }
                            });
                        });
                    });
                });
            });
        }


    }
}






