using SensApi.Models;

namespace SensApi.Services
{
    public interface ISensScrapper
    {
        string GetScrapper();
        Task<List<SensAnnouncement>> ScrapeData(string url);

    }
}
