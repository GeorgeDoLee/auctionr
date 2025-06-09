using AuctionR.Core.API.Constants;
using AuctionR.Core.API.Hubs;
using AuctionR.Core.Application.Contracts.HubClients;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Features.Bids.Commands.PlaceBid;
using AuctionR.Core.Application.Features.Bids.Queries.Get;
using AuctionR.Core.Application.Features.Bids.Queries.Search;
using AuctionR.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("Fixed")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status429TooManyRequests)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BidsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<AuctionHub, IAuctionClient> _hubContext;

    public BidsController(
        IMediator mediator, 
        IHubContext<AuctionHub, IAuctionClient> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.BidsRead)]
    [ActionName(nameof(GetBidByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBidByIdAsync(
        [FromRoute] int id, CancellationToken ct)
    {
        var query = new GetBidQuery(id);

        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<BidModel>
            .SuccessResponse($"Bid with id: {id} fetched successfully.", response));
    }

    [HttpGet("search")]
    [Authorize(Policy = Permissions.BidsRead)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBidsByAuctionIdAsync(
        [FromQuery] SearchBidsQuery query, CancellationToken ct)
    {
        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<IEnumerable<BidModel>>
            .SuccessResponse($"Bids fetched successfully.", response));
    }

    [HttpPost]
    [Authorize(Policy = Permissions.BidsCreate)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PlaceBidAsync(
        [FromBody] PlaceBidCommand command, CancellationToken ct)
    {
        var response = await _mediator.Send(command, ct);

        if (response == null)
        {
            return BadRequest(ApiResponse<object?>
                .FailResponse("Bid coult not be placed."));
        }

        return CreatedAtAction(
                nameof(GetBidByIdAsync),
                new { id = response.Id },
                ApiResponse<BidModel>.SuccessResponse("Bid placed successfully.", response)
        );
    }
}
