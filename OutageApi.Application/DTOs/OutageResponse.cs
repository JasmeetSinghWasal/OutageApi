using OutageApi.Domain.Entities;
namespace OutageApi.Application.DTOs;

public class OutageResponse
{
    public Guid Id { get; set; }
    public string Area { get; set; } = string.Empty;
    public OutageType Type { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime ReportedAt { get; set; }
    public OutageStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}