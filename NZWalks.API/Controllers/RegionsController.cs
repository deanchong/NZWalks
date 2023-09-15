using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database
            var regionsDomain = await regionRepository.GetAllAsync();

            //map domain model to DTO
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}

            //map domain model to DTO
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTO
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        // GET SINGLE REGION (Get Region by ID)
        // GET: https://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            // Get Region Domain Model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map region domain model to region dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

            //Map region domain model to region dto


            // return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST To create new region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to domain model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};

            //Map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


            //Use domain model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model back to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            //Map domain model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update region
        //PUT: https://localhost:1234/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to domain region model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel );

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert domain model to DTO

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // Delete region
        // DELETE: https://localhost:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return deleted region back
            //map domain model to dto

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
