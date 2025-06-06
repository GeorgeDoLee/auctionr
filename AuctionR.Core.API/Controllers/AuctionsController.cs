using AuctionR.Core.API.Constants;
using AuctionR.Core.Application.Commands.Auctions.Cancel;
using AuctionR.Core.Application.Commands.Auctions.Create;
using AuctionR.Core.Application.Commands.Auctions.End;
using AuctionR.Core.Application.Commands.Auctions.Postpone;
using AuctionR.Core.Application.Commands.Auctions.Start;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Queries.Auctions.Get;
using AuctionR.Core.Application.Queries.Auctions.GetAll;
using AuctionR.Core.Application.Queries.Auctions.Search;
using AuctionR.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AuctionR.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("Fixed")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status429TooManyRequests)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.AuctionsRead)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAuctionsAsync(
        [FromQuery] GetAllAuctionsQuery query, CancellationToken ct)
    {
        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<IEnumerable<AuctionModel>>
            .SuccessResponse("All auctions fetched successfully", response));
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.AuctionsRead)]
    [ActionName(nameof(GetAuctionByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuctionByIdAsync(
        [FromRoute] int id, CancellationToken ct)
    {
        var query = new GetAuctionQuery(id);

        var response = await _mediator.Send(query, ct);
        
        return Ok(ApiResponse<AuctionModel>
            .SuccessResponse($"Auction with id: {id} fetched successfully.", response));
    }

    [HttpGet("search")]
    [Authorize(Policy = Permissions.AuctionsRead)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchAuctionAsync(
        [FromQuery] SearchAuctionsQuery query, CancellationToken ct)
    {
        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<IEnumerable<AuctionModel>>
            .SuccessResponse($"Auctions fetched successfully.", response));
    }

    [HttpPost]
    [Authorize(Policy = Permissions.AuctionsCreate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuctionAsync(
        [FromBody] CreateAuctionCommand command, CancellationToken ct)
    {
        var response = await _mediator.Send(command, ct);

        if (response == null)
        {
            return BadRequest(ApiResponse<object?>
                .FailResponse("Auction with this product already exists. You can add another auction if you cancel existing one"));
        }

        return CreatedAtAction(
                nameof(GetAuctionByIdAsync),
                new { id = response.Id },
                ApiResponse<AuctionModel>.SuccessResponse("Auction Created successfully.", response)
        );
    }

    [HttpPost("{id}/start")]
    [Authorize(Policy = Permissions.AuctionsUpdate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        _ = await _mediator.Send(new StartAuctionCommand(id), ct);

        return Ok(ApiResponse<object?>
            .SuccessResponse($"Auction with id: {id} started successfully."));
    }

    [HttpPost("{id}/end")]
    [Authorize(Policy = Permissions.AuctionsUpdate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EndAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        _ = await _mediator.Send(new EndAuctionCommand(id), ct);

        return Ok(ApiResponse<object?>
            .SuccessResponse($"Auction with id: {id} ended successfully."));
    }

    [HttpPost("{id}/cancel")]
    [Authorize(Policy = Permissions.AuctionsUpdate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        _ = await _mediator.Send(new CancelAuctionCommand(id), ct);

        return Ok(ApiResponse<object?>
            .SuccessResponse($"Auction with id: {id} cancelled successfully."));
    }

    

    [HttpPost("{id}/postpone")]
    [Authorize(Policy = Permissions.AuctionsUpdate)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostponeAuctionAsync(
        [FromRoute] int id,
        [FromBody] PostponeAuctionCommand command, 
        CancellationToken ct)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object?>
                .FailResponse("URL Id and body Id do not match."));
        }

        _ = await _mediator.Send(command, ct);

        return Ok(ApiResponse<object?>
            .SuccessResponse($"Auction with id: {id} postponed successfully."));
    }
}