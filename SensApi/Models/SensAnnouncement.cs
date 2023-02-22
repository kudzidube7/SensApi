namespace SensApi.Models
{
    public class SensAnnouncement
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime Date { get; set; }
        public string Issuer { get; set; }
    }
}
