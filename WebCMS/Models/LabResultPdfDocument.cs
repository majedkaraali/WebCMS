
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace WebCMS.Models;

public class LabResultPdfDocument : IDocument
{
    private readonly string _patientName;
    private readonly string _testName;
    private readonly string _gender;
    private readonly DateTime _updatedDate;
    private readonly List<LabTestResult> _results;

    public LabResultPdfDocument(string patientName, string testName, string gender, DateTime updatedDate, List<LabTestResult> results)
    {
        _patientName = patientName;
        _testName = testName;
        _gender = gender;
        _updatedDate = updatedDate;
        _results = results;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);

            page.Header().Text("Lab Test Results").FontSize(20).Bold();

            page.Content().Column(col =>
            {
                col.Item().Text($"Patient: {_patientName}");
                col.Item().Text($"Test: {_testName}");
                col.Item().Text($"Gender: {_gender}");
                col.Item().Text($"Updated Date: {_updatedDate:yyyy-MM-dd}");
                col.Item().Text(" ");

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Test Name").Bold();
                        header.Cell().Text("Result").Bold();
                        header.Cell().Text("Normal").Bold();
                        header.Cell().Text("Unit").Bold();
                        header.Cell().Text("Min").Bold();
                        header.Cell().Text("Max").Bold();
                        header.Cell().Text("Remark").Bold();
                    });

                    foreach (var result in _results)
                    {
                        table.Cell().Text(result.LabTest.TestName);
                        table.Cell().Text(result.Result.ToString());
                        table.Cell().Text(result.LabTest.NormalValue);
                        table.Cell().Text(result.LabTest.Unit);
                        table.Cell().Text(result.LabTest.MinRange.ToString());
                        table.Cell().Text(result.LabTest.MaxRange.ToString());
                        table.Cell().Text(result.Remark ?? "-");
                    }
                });
            });

            page.Footer().AlignCenter().Text(x =>
            {
                x.Span("Generated on ");
                x.Span($"{DateTime.Now:yyyy-MM-dd HH:mm}");
            });
        });
    }
}
