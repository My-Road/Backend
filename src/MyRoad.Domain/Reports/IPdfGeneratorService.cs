namespace MyRoad.Domain.Reports
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GeneratePdfFromHtml(string htmlContent);
    }
}
