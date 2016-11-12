using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennedyLabsWebsite.Models
{
    public class SectionModel
    {
        [Key]
        public int Id { get; set; }

        public int RootPageId { get; set; }

        public int? PageId { get; set; }

        public int? SectionId { get; set; }

        [Editable(false)]
        public int Ordinal { get; set; }
        
        [Required]
        public string Title { get; set; }

        [MaxLength(64)]
        public string SecondaryText { get; set; }

        [MaxLength(64)]
        public string TertiaryText { get; set; }

        [ForeignKey("ParentPageId")]
        public PageModel RootPage { get; set; }

        [ForeignKey("PageId")]
        public PageModel Page { get; set; }

        [ForeignKey("SectionId")]
        public SectionModel Section { get; set; }

        [InverseProperty("Section")]
        public ICollection<SectionModel> Sections { get; set; }

        [InverseProperty("Section")]
        public ICollection<ItemModel> Items { get; set; }
    }
}
