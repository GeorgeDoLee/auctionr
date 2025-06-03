using AuctionR.Core.API.Hubs;
using AuctionR.Core.Application.Commands.Bids.Create;
using AuctionR.Core.Application.Commands.Bids.Retract;
using AuctionR.Core.Application.Contracts.HubClients;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Queries.Bids.Get;
using AuctionR.Core.Application.Queries.Bids.GetByAuction;
using AuctionR.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBidsByAuctionIdAsync(
        [FromQuery] GetBidsByAuctionQuery query, CancellationToken ct)
    {
        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<IEnumerable<BidModel>>
            .SuccessResponse($"Bids for auction  with Id: {query.AuctionId} fetched successfully.", response));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

        await _hubContext.Clients
            .Group($"auction-{response.Id}")
            .AuctionUpdated(response);

        return CreatedAtAction(
                nameof(GetBidByIdAsync),
                new { id = response.Id },
                ApiResponse<AuctionModel>.SuccessResponse("Bid placed successfully.", response)
        );
    }

    [HttpPost("{id}/retract")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RetractBidAsync(
        [FromRoute] int id, CancellationToken ct)
    {
        var response = await _mediator.Send(new RetractBidCommand(id), ct);

        if (response == null)
        {
            return BadRequest(ApiResponse<object?>
                .FailResponse("Bid coult not be retracted."));
        }

        await _hubContext.Clients
            .Group($"auction-{response.Id}")
            .AuctionUpdated(response);

        return Ok(ApiResponse<object?>
            .FailResponse($"Bid with id: {id} retracted successfully."));
    }
}
