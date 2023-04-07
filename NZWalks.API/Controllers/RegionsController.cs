using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Domain;
using NZWalks.API.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();
            var regionDTOs = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionDTOs.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageURL = regionDomain.RegionImageURL
                });
            }

            return Ok(regionDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain == null)
            {
                return NotFound("Id not Found.. Please Check");
            }

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageURL = regionDomain.RegionImageURL
            };


            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageURL = addRegionRequestDto.RegionImageURL
            };


            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return CreatedAtAction(nameof(GetById), new { regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)

        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound("Region not found.... Please check Again");
            }

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageURL = updateRegionRequestDto.RegionImageURL;

            dbContext.SaveChanges();

            var regiondto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageURL= regionDomainModel.RegionImageURL
            };

            return Ok(regiondto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x=>x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound("Id not Found ........ Please check");
            }

            dbContext.Remove(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageURL = regionDomainModel.RegionImageURL
            
            };


            return Ok(regionDto);
        }
    }
}
