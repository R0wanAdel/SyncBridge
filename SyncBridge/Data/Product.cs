namespace SyncBridge.Data
{
    public class Product
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }                 // owner
        public string ExternalId { get; set; } = default!;  // id from the external API
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = default!;
    }
}
