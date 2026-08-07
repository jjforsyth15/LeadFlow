using System.ComponentModel.DataAnnotations;

namespace LeadFlow.Api.DTOs.Campaigns;

public class CreateCampaignRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}