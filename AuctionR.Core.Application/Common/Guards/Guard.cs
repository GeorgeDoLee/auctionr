using AuctionR.Core.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Common.Guards;

public static class Guard
{
    public static void EnsureUserOwnsResource(int resourceOwnerId, int currentUserId, string resourceName, ILogger logger)
    {
        if (resourceOwnerId != currentUserId)
        {
            logger.LogWarning(
                "{ResourceName} owner Id: {OwnerId} did not match with incoming request user Id: {UserId}.",
                resourceName, resourceOwnerId, currentUserId);

            throw new ForbiddenException($"You are not authorized to modify this {resourceName.ToLower()}.");
        }
    }
}