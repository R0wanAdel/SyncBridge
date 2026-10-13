using SyncBridge.Data.Enums;

namespace SyncBridge.Data
{
    public class SyncJob
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }                 // owner: every query filters on this
        public SyncJobStatus Status { get; set; } = SyncJobStatus.Queued;

        public DateTime? DateFrom { get; set; }          // sync parameters
        public DateTime? DateTo { get; set; }

        public int TotalFetched { get; set; }            // progress counters
        public int TotalProcessed { get; set; }
        public int FailedRows { get; set; }

        public int LastProcessedPage { get; set; }       // checkpoint: resume from here after a crash
        public DateTime? LastHeartbeat { get; set; }     // watchdog looks at this
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        public User User { get; set; } = default!;
    }
}
