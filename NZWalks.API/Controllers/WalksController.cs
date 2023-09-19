using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            //Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Get all walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get from database
            var walksDomainModel = await walkRepository.GetAllAsync();

            //Map domain mobel to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //Get walks by Id
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get from database
            var walksDomainModel = await walkRepository.GetByIdAsync(id);

            if (walksDomainModel == null)
            {
                return NotFound();
            }

            // Map domain model to Dto
            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        //Update Walk
        //PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Delete walk
        // DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //return deleted walk back
            //map domain model to dto

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
