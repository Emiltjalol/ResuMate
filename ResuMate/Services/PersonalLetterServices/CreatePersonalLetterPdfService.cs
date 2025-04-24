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
                        col.Item().Text("Personligt Brev").FontSize(20).Bold().Underline();
                        col.Item().PaddingTop(10).Text(generatedLetter);
                    });
            });
        }

    }
}
