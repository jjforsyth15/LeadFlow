using LeadFlow.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace LeadFlow.Api.DTOs.Campaigns;

public class UpdateCampaignRequest
{
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public CampaignStatus? Status { get; set; }
}