using NZWalks.API.Domain;
using NZWalks.API.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllWalksAsync();

        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walkDomainModel);

        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
