using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgorusApi.Model
{
    [Index(nameof(Name), IsUnique = true, Name = "idxName")]
    public class FileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? FileModelId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public string? ContentType { get; set; }

        public ICollection<FileHistoryModel> FileHistoryModels { get; set; }

        public FileModel()
        {
            FileHistoryModels = new List<FileHistoryModel>();
        }
    }
}
