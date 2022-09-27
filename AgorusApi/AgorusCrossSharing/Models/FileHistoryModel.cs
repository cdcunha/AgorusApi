using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace AgorusApi.Model
{
    [Index(nameof(Date), Name = "idxDate")]
    public class FileHistoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? FileHistoryModelId { get; set; }

        public long Length { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        [Required]
        public Byte[] FileContent { get; set; }

        [ForeignKey("FK_FileModel")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }
    }
}
