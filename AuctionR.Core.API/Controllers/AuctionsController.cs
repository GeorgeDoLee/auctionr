using AuctionR.Core.Application.Commands.Auctions.Cancel;
using AuctionR.Core.Application.Commands.Auctions.Create;
using AuctionR.Core.Application.Commands.Auctions.Delete;
using AuctionR.Core.Application.Commands.Auctions.End;
using AuctionR.Core.Application.Commands.Auctions.Start;
using AuctionR.Core.Application.Commands.Auctions.Update;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Queries.Auctions.Get;
using AuctionR.Core.Application.Queries.Auctions.GetAll;
using AuctionR.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionR.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAuctionsAsync(
        [FromQuery] GetAllAuctionsQuery query, CancellationToken ct)
    {
        var response = await _mediator.Send(query, ct);

        return Ok(ApiResponse<IEnumerable<AuctionModel>>
            .SuccessResponse("All auctions fetched successfully", response));
    }

    [HttpGet("{id}")]
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

    [HttpPost]
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAuctionAsync(
        [FromBody] UpdateAuctionCommand command, CancellationToken ct)
    {
        _ = await _mediator.Send(command, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        _ = await _mediator.Send(new DeleteAuctionCommand(id), ct);

        return NoContent();
    }

    [HttpPost("{id}/start")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        _ = await _mediator.Send(new CancelAuctionCommand(id), ct);

        return Ok(ApiResponse<object?>
            .SuccessResponse($"Auction with id: {id} cancelled successfully."));
    }
}