using Microsoft.EntityFrameworkCore;
using WebSampleApplicationAPI.Data;
using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;

namespace WebSampleApplicationAPI.Respositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext dbContext;

        public SQLRegionRepository(WalksDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public Task<RegionDto> CreateAsync(RegionDto regionDomainModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion=await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingRegion==null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion= await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingRegion==null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;  
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;

        }

        public Task<Region> UpdateAsync(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}               
