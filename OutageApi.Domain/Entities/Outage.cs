namespace OutageApi.Domain.Entities;

public class Outage
{
    public Guid OutageId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public OutageType Type { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime ReportedAt { get; set; }
    public OutageStatus Status { get; set; }
}
