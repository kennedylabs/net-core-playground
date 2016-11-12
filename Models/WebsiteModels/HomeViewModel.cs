using System.Collections.Generic;

namespace KennedyLabsWebsite.Models
{
    public class HomeViewModel
    {
        public ICollection<HomeSectionViewModel> HomeSections { get; set; }

        public class HomeSectionViewModel
        {
            public ICollection<string> TextValues { get; set; }
        }
    }
}
