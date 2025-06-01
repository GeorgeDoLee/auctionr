using AuctionR.Core.API.Extensions;
using AuctionR.Core.API.Hubs;
using AuctionR.Core.API.Middlewares;
using AuctionR.Core.Application.Extensions;
using AuctionR.Core.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<AuctionHub>("/auction");

app.Run();