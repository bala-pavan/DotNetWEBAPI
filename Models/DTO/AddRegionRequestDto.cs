using System.ComponentModel.DataAnnotations;

namespace WebSampleApplicationAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has be min 3 characters")]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
