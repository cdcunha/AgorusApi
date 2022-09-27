using System.ComponentModel.DataAnnotations;

namespace AgorusApi.Dto
{
    public class FileHistoryDto
    {
        public int? FileHistoryModelId { get; set; }

        public int? FileModelId { get; set; }

        public long Length { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public Byte[] FileContent { get; set; }
    }
}
