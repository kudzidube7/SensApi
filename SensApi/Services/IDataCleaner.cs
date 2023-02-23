namespace SensApi.Services
{
    public interface IDataCleaner
    {
        string GetReference(string jshandleObject);
        string GetIssuer(string cleanText);
        string GetDescription(string noIssuerText);
        string RemoveReferenceFromData(string reference, string jshandleObject);
        string RemoveIssuerNameFromData(string reference, string jshandleObject);

    }
}
