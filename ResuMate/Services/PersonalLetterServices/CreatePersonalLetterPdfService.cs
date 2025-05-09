using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResuMate.Components.Models;

namespace ResuMate.Services
{
    public class CreatePersonalLetterPdfService
    {
        public void Compose(IDocumentContainer container, string generatedLetter, PersonalLetterModel personalLetter)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Content()
                    .PaddingVertical(10)
                    .Column(col =>
                    {
                        // Add user details at the top
                        col.Item().Text($"{personalLetter.Name}").FontSize(14).Bold();
                        col.Item().Text($"{personalLetter.Address},").FontSize(12);
                        col.Item().Text($"{personalLetter.PostalCode} {personalLetter.City}").FontSize(12);
                        col.Item().Text($"{personalLetter.Email}").FontSize(12);
                        col.Item().Text($"{personalLetter.PhoneNumber}").FontSize(12);

                        col.Item().PaddingTop(20).Text("Personligt Brev").FontSize(20).Bold().Underline();
                        col.Item().PaddingTop(10).Text(generatedLetter);
                    });
            });
        }

        //public void Compose(IDocumentContainer container, string generatedLetter, PersonalLetterModel personalLetter)
        //{
        //    container.Page(page =>
        //    {
        //        page.Size(PageSizes.A4);
        //        page.DefaultTextStyle(TextStyle.Default.FontFamily("Calibri"));
        //        page.Margin(0);

        //        page.Content().Row(row =>
        //        {

        //            row.RelativeItem(0.4f).Background(Colors.Grey.Darken3).Padding(15).Column(col =>
        //            {
        //                col.Item().Text(personalLetter.Name).FontSize(25).Bold().FontColor(Colors.White).AlignCenter();
        //                col.Item().PaddingBottom(40).Text(personalLetter.ProfessionalTitle).FontSize(16).Italic().FontColor(Colors.White).AlignCenter();

        //                col.Item().PaddingBottom(10).Text("KONTAKTINFO").FontSize(16).Bold().FontColor(Colors.White).Underline();
        //                col.Item().Text($"{personalLetter.PhoneNumber}").FontSize(12).FontColor(Colors.White);
        //                col.Item().PaddingTop(3).Text($"{personalLetter.Email}").FontSize(12).FontColor(Colors.White);
        //                col.Item().PaddingTop(3).Text($"{personalLetter.Address}, ").FontSize(12).FontColor(Colors.White);
        //                col.Item().PaddingTop(3).Text($"{personalLetter.PostalCode} {personalLetter.City}").FontSize(12).FontColor(Colors.White);


        //            });

        //            // Right column: Personal letter content
        //            row.RelativeItem(0.6f).ExtendVertical().Background(Colors.White).Padding(20).Column(col =>
        //            {
        //                col.Item().Text("PERSONLIGT BREV").FontSize(20).Bold().FontColor(Colors.Black).Underline();
        //                col.Item().PaddingTop(10).Text(generatedLetter).FontSize(12).LineHeight(1.3f).FontColor(Colors.Black);
        //            });
        //        });
        //    });
        //}

    }
}