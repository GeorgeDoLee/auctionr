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

    public static void EnsureFound<T>(T? resource, string resourceName, int resourceId, ILogger logger)
    {
        if (resource == null)
        {
            logger.LogWarning("{resource} with id: {resourceId} could not be found.", resourceName, resourceId);
            throw new NotFoundException($"{resourceName} with id: {resourceId} could not be found.");
        }
    }
}