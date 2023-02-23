using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PuppeteerSharp;
using System.Text;

namespace SensApi.Services
{
    public class PDFAccessorService : IPDFAccesorService
    {

        public async Task<string> GetPdfStringContents(string pdfUrl)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
            });

            var page = await browser.NewPageAsync();

            await page.GoToAsync(pdfUrl ,WaitUntilNavigation.Networkidle0);

            StringBuilder sb = new StringBuilder();

            using (PdfReader reader = new PdfReader(pdfUrl))
            {
                for(int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, pageNo,strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
                }
            }

            var pdfDocText = sb.ToString();

            return pdfDocText;
        }
    }
}
