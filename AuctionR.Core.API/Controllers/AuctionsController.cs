using AuctionR.Core.Application.Commands.Auctions.Create;
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

    [HttpGet("{id}")]
    [ActionName(nameof(GetAuctionByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuctionByIdAsync(
        [FromRoute] int id, CancellationToken ct)
    {
        throw new NotImplementedException();
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
}