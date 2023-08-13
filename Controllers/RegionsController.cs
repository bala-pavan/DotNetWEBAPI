using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using WebSampleApplicationAPI.CustomActionFilters;
using WebSampleApplicationAPI.Data;
using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;
using WebSampleApplicationAPI.Respositories;

namespace WebSampleApplicationAPI.Controllers
{
    // http://localhost:1234/api/regions
    [Route("api/[controller]")] // call to region controlller
    [ApiController] //api use
    //[Authorize]//without jwt
    public class RegionsController : ControllerBase
    {

        private readonly WalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(WalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, 
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper=mapper;
            this.logger = logger;
        }

        

        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllRegions() 
        {
            try
            {
                logger.LogInformation("GetAllRegions Action Method was invoked");

                logger.LogWarning("This is a warning log");

                logger.LogError("This is a error log");

                //Result from database -doMAIN MODELS
                //var regions =await dbContext.Regions.ToListAsync();
                var regions = await regionRepository.GetAllRegionsAsync();

                /* //Map Domain Models to DTOs and return
                 var regionsDto = new List<RegionDto>();
                 foreach (var regionDomain in regions)
                 {
                     regionsDto.Add(new RegionDto()
                     {
                         Id = regionDomain.Id,
                         Code = regionDomain.Code,
                         Name = regionDomain.Name,
                         RegionImageUrl = regionDomain.RegionImageUrl
                     });
                 }*/
                var regionsDto = mapper.Map<List<RegionDto>>(regions);

                //Hard coded list
                /* var regions = new List<Region>
                 {
                     new Region
                     {
                         Id=Guid.NewGuid(),
                         Name="Europe Region",
                         Code="ER",
                         RegionImageUrl =""
                     },
                     new Region
                     {
                         Id=Guid.NewGuid(),
                         Name="Asia Region",
                         Code="AR",
                         RegionImageUrl =""
                     }
                 };*/
                logger.LogInformation($"Finished getallregions request with data: {JsonSerializer.Serialize(regionsDto)}");
                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }


        }

        //Get single region (Get region by ID)
        //GET : https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async  Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);    //only take primary key

            //Get Region Domain Model From Database
            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region DOmain Model to Region DTO  
           /* var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl

            };*/
           var regionDto=mapper.Map<RegionDto>(regionDomain);   
            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //if(ModelState.IsValid)
            //{
                /*//Map or COnvert DTO to DomainModel
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };*/
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Domain Model to Create Region
                // await dbContext.Regions.AddAsync(regionDomainModel);
                //   await dbContext.SaveChangesAsync();
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                /* //Map Domain model back to DTO
                 var regionDto = new RegionDto
                 {
                     Id = regionDomainModel.Id,
                     Code = regionDomainModel.Code,
                     Name = regionDomainModel.Name,
                     RegionImageUrl = regionDomainModel.RegionImageUrl
                 };
     */
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
           /* }
            else
            {
                return BadRequest(ModelState);
            }*/
            
        }
        //Update region
        //put
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
           /* if (ModelState.IsValid)
            {*/
                /*//Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };*/
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);


                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                //CHeck id=f region exists
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                /*
                //Map DTO to DomainModel
                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.Name=updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;
                await dbContext.SaveChangesAsync();
                */
                //COnvert DomainModel to DTO
                /*var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl

                };   */
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
           /* }
            else { return BadRequest(ModelState); }*/
            
        }
        //Delete
        
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           var regionDomainModel=await regionRepository.DeleteAsync(id);
            //CHeck id=f region exists
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            /*
            //Delete region
             dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();
            */
            //return delete region back
            /*//map domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };*/
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

    }


}

