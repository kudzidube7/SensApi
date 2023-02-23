using SensApi.Models;

namespace SensApi.Services
{
    public interface ISummariserService
    {
        Task<string> Summarise(string documentText);
    }
}
