using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Domain;
using NZWalks.API.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        //Create Walk
        //POST: api/walks

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map Dto to Domain
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //GET Walks
        //GET: api/walks

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkList = await walkRepository.GetAllWalksAsync();
            return Ok(mapper.Map<List<WalkDto>>(walkList));
        }

        //Get by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound("Walk Id not found.... Please try Again");
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update Walk 
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound("Walk Id Not found please try Again..... ");
            }

            //Map domain to DTO

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //Delete walk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteWalkAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound("Id not found Please try Again....");
            }

            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
