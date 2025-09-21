namespace CQRSRentACar.Entities
{
    public class ContactMessage
    {
        public int ContactMessageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public string? AiResponse { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}
