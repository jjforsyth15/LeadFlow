using LeadFlow.Api.DTOs.Campaigns;
using LeadFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeadFlow.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly CampaignService _campaignService;

    public CampaignsController(CampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    // POST - /api/Campaigns - Create Campaign
    [HttpPost]
    public async Task<ActionResult<CampaignResponse>> CreateCampaign(
        CreateCampaignRequest request)
    {
        var campaign = await _campaignService.CreateCampaignAsync(request);

        return CreatedAtAction(
            nameof(GetCampaignById),
            new { id = campaign.Id },
            campaign
            );
    }

    // GET - /api/Campaigns/{id} - Get Campaign by Id
    [HttpGet("{id}")]
    public async Task<ActionResult<CampaignResponse>> GetCampaignById(int id)
    {
        var campaign = await _campaignService.GetCampaignByIdAsync(id);

        if (campaign == null)
            return NotFound();

        return Ok(campaign);
    }

    // GET - /api/Campaigns - Get all campaigns
    [HttpGet]
    public async Task<ActionResult<List<CampaignResponse>>> GetCampaigns()
    {
        var campaigns = await _campaignService.GetCampaignsAsync();

        return Ok(campaigns);
    }

    // PATCH - /api/Campiagns/{id} - Update campaign 
    [HttpPatch("{id}")]
    public async Task<ActionResult<CampaignResponse>> UpdateCampaign(int id, UpdateCampaignRequest request)
    {
        var campaign = await _campaignService.UpdateCampaign(id, request);

        if (campaign == null)
            return NotFound();

        return Ok(campaign);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaign = await _campaignService.DeleteCampaignAsync(id);

        if (!campaign)
            return NotFound();

        return NoContent();
    }
}