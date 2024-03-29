﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSampleApplicationAPI.CustomActionFilters;
using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;
using WebSampleApplicationAPI.Respositories;

namespace WebSampleApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private IMapper mapper;
        private IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper=mapper;
            this.walkRepository = walkRepository;
        }
        //Create walk
        //POST:/api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if(ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain Model to DTO
                var walkDto = mapper.Map<AddWalkRequestDto>(walkDomainModel);
                return Ok(walkDto);
            }
            else { return BadRequest(ModelState); }
            

        }

        //Get Walks
        //Get : /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&IsASC=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {


            var walksDomainModel=await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy, isAscending ?? true,pageNumber,pageSize );

            //Create an exceptionn

            //Map Domain Model to DTO
            var walkdto=mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walkdto);

        }

        //Get Walk by Id
        //Get: /api/walks
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel=await walkRepository.GetByIdAsync(id);
            
            if(walkDomainModel==null)
            {
                return NotFound();
            }

            //Map Domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        //Update walk by id
        //Put :/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            /*if(ModelState.IsValid)
            {*/
                //Map DTO to DOmainModel
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //Map Domain Model to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                return Ok(walkDto);
/*            }
            else { return BadRequest(ModelState); }*/
            
        }

        //Delete walk by id
        //Delete  :/api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete=await walkRepository.DeleteAsync(id);
            if(delete==null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(delete));
        }
    }
}
