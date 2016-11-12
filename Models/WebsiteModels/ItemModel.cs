using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennedyLabsWebsite.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }

        public int RootPageId { get; set; }

        public int SectionId { get; set; }

        [Editable(false)]
        public int Ordinal { get; set; }

        [Required]
        public string Text { get; set; }

        [MaxLength(64)]
        public string Context { get; set; }

        [MaxLength(64)]
        public string Type { get; set; }

        [ForeignKey("RootPageId")]
        public PageModel Page { get; set; }

        [ForeignKey("SectionId")]
        public SectionModel Section { get; set; }
    }
}
