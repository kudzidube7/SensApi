using Microsoft.AspNetCore.Http;
using PuppeteerSharp;
using SensApi.Models;

namespace SensApi.Services
{
    public class SensScrapper : ISensScrapper
    {
        private readonly IDataCleaner _dataCleaner;
        public string GetScrapper()
        {
            Console.WriteLine("Scrapping ......");
            var data = "Scrappping";
            return data;
        }

        public SensScrapper(IDataCleaner dataCleaner)
        {
            _dataCleaner = dataCleaner;
        }

        public async Task<List<SensAnnouncement>> ScrapeData(string url)
        {
            try
            {
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = false,
                });

                var page = await browser.NewPageAsync();

                await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);

                //await page.WaitForNavigationAsync();

                //var ulElement = await page.WaitForSelectorAsync("li");
                var ulElement = await page.QuerySelectorAsync("ul#announcements");

                var liElements = await ulElement.QuerySelectorAllAsync("li");

                var liContents = new List<SensAnnouncement>();

                foreach (var li in liElements)
                {
                    SensAnnouncement sensAnnouncement = new SensAnnouncement();
                   
                    var text = await li.GetPropertyAsync("textContent");
                    if(text.ToString != null)
                    {
                        //Get reference number then remove it from string
                        var reference = _dataCleaner.GetReference(text.ToString());
                        sensAnnouncement.ReferenceNumber = reference;

                        var cleanText = _dataCleaner.RemoveReferenceFromData(reference, text.ToString());

                        //Get Company name then remove it from string
                        var issuerName = _dataCleaner.GetIssuer(cleanText);
                        sensAnnouncement.Issuer = issuerName;

                        var noIssuerText = _dataCleaner.RemoveIssuerNameFromData(issuerName, cleanText);

                       // cleanText = _dataCleaner.RemoveIssuerNameFromData(issueName, cleanText);
                    }
                   
                    liContents.Add(sensAnnouncement);
                    //CreateSensAnnouncement(text.ToString());
                }


                //var data = await page.EvaluateExpressionAsync("");

                var data = liContents;

                await browser.CloseAsync();

                return data;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error " + ex);
                return null;
            }

        }

        #region Private Methods
        private SensAnnouncement CreateSensAnnouncement(string sensString)
        {
            string[] parts = sensString.Split(new char[] { '-', '|' });

            SensAnnouncement sensAnnouncement = new SensAnnouncement()
            {
                Type = parts[0],
                Description = parts[1].Trim(),
                ReferenceNumber = parts[2].Trim(),
                //Date = DateTime.Parse(parts[3].Trim()),
                Issuer = parts[4].Trim()
            };

            return sensAnnouncement;
        } 
        #endregion
    }
}
