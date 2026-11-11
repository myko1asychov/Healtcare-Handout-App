namespace HandoverApi.Models;

public class Handover
{
    public int Id { get; set; }
    public string PatientCode { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Priority { get; set; } = "Normal";
    
    // 4 SBAR
    public string Situation { get; set; } = string.Empty;
    public string Background { get; set; } = string.Empty;
    public string Assessment { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}