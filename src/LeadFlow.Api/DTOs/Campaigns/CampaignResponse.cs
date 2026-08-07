using LeadFlow.Api.Models;

namespace LeadFlow.Api.DTOs.Campaigns;


public class CampaignResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public CampaignStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}