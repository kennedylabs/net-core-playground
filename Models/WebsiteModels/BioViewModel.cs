using System.Collections.Generic;

namespace KennedyLabsWebsite.Models
{
    public class BioViewModel
    {
        public ICollection<BioSectionModel> BioSections { get; set; }

        public class BioSectionModel
        {
            public string Title { get; set; }

            public string Company { get; set; }

            public string TimeSpan { get; set; }

            public ICollection<BioSectionModel> Children { get; set; }

            public ICollection<string> Items { get; set; }
        }
    }
}
