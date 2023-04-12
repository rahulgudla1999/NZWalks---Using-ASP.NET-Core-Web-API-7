using NZWalks.API.Domain;
using NZWalks.API.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walkDomainModel);

        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
