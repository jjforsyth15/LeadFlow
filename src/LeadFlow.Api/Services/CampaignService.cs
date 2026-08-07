using LeadFlow.Api.Data;
using LeadFlow.Api.DTOs.Campaigns;
using LeadFlow.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadFlow.Api.Services;

public class CampaignService
{
    private readonly ApplicationDbContext _dbContext;

    public CampaignService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static CampaignResponse ToResponse(Campaign campaign)
    {
        return new CampaignResponse
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            Status = campaign.Status,
            CreatedAt = campaign.CreatedAt,
            UpdatedAt = campaign.UpdatedAt
        };
    }

    public async Task<CampaignResponse> CreateCampaignAsync(
        CreateCampaignRequest request)
    {
        var campaign = new Campaign
        {
            Name = request.Name,
            Description = request.Description,
        };

        _dbContext.Campaigns.Add(campaign);
        await _dbContext.SaveChangesAsync();

        return ToResponse(campaign);
    }

    public async Task<CampaignResponse?> GetCampaignByIdAsync(int id)
    {
        var campaign = await _dbContext.Campaigns.FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
            return null;

        return ToResponse(campaign);
    }

    public async Task<List<CampaignResponse>> GetCampaignsAsync()
    {
        var campaigns = await _dbContext.Campaigns
            .Select(campaign => new CampaignResponse
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                Status = campaign.Status,
                CreatedAt = campaign.CreatedAt,
                UpdatedAt = campaign.UpdatedAt
            })
            .ToListAsync();

        return campaigns;
    }

    public async Task<CampaignResponse?> UpdateCampaign(int id, UpdateCampaignRequest request)
    {
        var campaign = await _dbContext.Campaigns.FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
            return null;

        if (request.Name != null)
            campaign.Name = request.Name;

        if (request.Description != null) // review later, cannot distinguish if null is intentional or not
            campaign.Description = request.Description;

        if (request.Status.HasValue)
            campaign.Status = request.Status.Value;

        campaign.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(campaign);
    }

    public async Task<bool> DeleteCampaignAsync(int id)
    {
        var campaign = await _dbContext.Campaigns.FirstOrDefaultAsync( c => c.Id == id);

        if (campaign == null)
            return false;

        _dbContext.Campaigns.Remove(campaign);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}