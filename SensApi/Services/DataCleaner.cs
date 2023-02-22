using PuppeteerSharp;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

namespace SensApi.Services
{
    public class DataCleaner : IDataCleaner
    {
        public string GetIssuer(string cleanText)
        {
            int separatorIndex = cleanText.IndexOf("|");

            var issuerName  = cleanText.Substring(separatorIndex+1).Trim();

            return issuerName;

        }

        public string GetReference(string jshandleObject)
        {
            try
            {
                string pattern = @"S\d{6}\b";
                Match match = Regex.Match(jshandleObject, pattern);

                if (match.Success)
                {
                    Console.WriteLine(match.Value.ToString());
                }

                return match.Value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
            
        }

        public string RemoveIssuerNameFromData(string issuerName, string cleanText)
        {
            try
            {
                var noIssuerText = cleanText.Replace(issuerName, "");

                return noIssuerText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string RemoveReferenceFromData(string reference, string jshandleObject)
        {
            try
            {
                var jshandle = "JSHandle:";
                var cleanedJsHandleObject = jshandleObject.Replace(jshandle, "").Replace(reference, "");

                return cleanedJsHandleObject;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
