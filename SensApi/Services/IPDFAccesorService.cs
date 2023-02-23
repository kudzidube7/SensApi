namespace SensApi.Services
{
    public interface IPDFAccesorService
    {
        Task<string> GetPdfStringContents(string pdfUrl);
    }
}
