using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;

namespace WebSampleApplicationAPI.Respositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    
    }
}
