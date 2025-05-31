using AuctionR.Core.Application.Commands.Auctions.Create;
using AuctionR.Core.Application.Commands.Auctions.Delete;
using AuctionR.Core.Application.Models;
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

        var response = await _mediator.Send(query);
        
        return Ok(ApiResponse<AuctionModel>
            .SuccessResponse($"Product with id: {id} fetched successfully.", response));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuctionAsync(
        [FromBody] CreateAuctionCommand command, CancellationToken ct)
    {
        var response = await _mediator.Send(command);

        if (response == null)
        {
            return BadRequest(ApiResponse<string>
                .FailResponse("Auction with this product already exists"));
        }

        return CreatedAtAction(
                nameof(GetAuctionByIdAsync),
                new { id = response.Id },
                response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAuctionAsync([FromRoute] int id, CancellationToken ct)
    {
        var response = await _mediator.Send(new DeleteAuctionCommand(id), ct);

        return NoContent();
    }
}