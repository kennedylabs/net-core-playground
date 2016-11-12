using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennedyLabsWebsite.Models
{
    public class PageModel
    {
        [Key]
        public int Id { get; set; }

        [Editable(false)]
        public int Ordinal { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [InverseProperty("Page")]
        public ICollection<SectionModel> Sections { get; set; }

        [InverseProperty("RootPage")]
        public ICollection<SectionModel> AllSections { get; set; }

        [InverseProperty("RootPage")]
        public ICollection<ItemModel> AllItems { get; set; }
    }
}
