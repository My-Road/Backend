using NReco.PdfGenerator;

namespace MyRoad.Domain.Reports
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        public async Task<byte[]> GeneratePdfFromHtml(string htmlContent)
        {
            return await Task.Run(() =>
            {
                var htmlToPdf = new HtmlToPdfConverter
                {
                    Orientation = PageOrientation.Portrait,
                    Size = PageSize.A4,
                    Margins = new PageMargins { Top = 10, Bottom = 20, Left = 10, Right = 10 },
                    CustomWkHtmlArgs = "--encoding UTF-8 --footer-center \"صفحة [page] من [topage]\" --footer-font-size 10"
                };

                return htmlToPdf.GeneratePdf(htmlContent);
            });
        }
    }
}
