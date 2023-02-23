using OpenAI_API;
using OpenAI_API.Completions;

namespace SensApi.Services
{
    public class SummariserService : ISummariserService
    {
		
        public async Task<string> Summarise(string documentText)
        {
            var summarisedText = await OpenAiSummariser(documentText);
            return summarisedText;
        }

        private async Task<string> OpenAiSummariser(string textToSummarise)
        {
           var openai = new OpenAIAPI("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");
            try
            {
                var prompt = "Please summarize this text in simple, conversational english";
                var model = "text-davinci-002";
                
                var request = new CompletionRequest
                {
                    Prompt = prompt + " " + textToSummarise,
                    Model = model,
                    MaxTokens = 100,
                    Temperature = 0.5f,
                };

                var result = await openai.Completions.CreateCompletionAsync(request);

                var summary = result.Completions.FirstOrDefault();

                return textToSummarise;
            }
            catch (Exception)
            {
                return "";

            }
        }
    }
}
