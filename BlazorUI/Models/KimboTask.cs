namespace BlazorUI.Models
{
    public class KimboTask
    {
        public int Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EffortId { get; set; }
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
