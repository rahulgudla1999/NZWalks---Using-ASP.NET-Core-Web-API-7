using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 Characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 Characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage ="Name has to be a maximium of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
