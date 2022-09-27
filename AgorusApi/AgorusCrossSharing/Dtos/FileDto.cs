using System.ComponentModel.DataAnnotations;

namespace AgorusApi.Dto
{
    public class FileDto
    {
        public int? FileModelId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        public List<FileHistoryDto> FileHistoryModels { get; set; }
    }
}
