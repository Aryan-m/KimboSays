using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorUI.Models
{
    public class KimboTask
    {
        public int Id { get; set; }
        public string Task { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Effort { get; set; } = string.Empty;
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
