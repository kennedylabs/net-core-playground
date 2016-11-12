using System.Collections.Generic;

namespace KennedyLabsWebsite.Models
{
    public class ProjectsViewModel
    {
        public ICollection<ProjectModel> Projects { get; set; }

        public class ProjectModel
        {
            public string Name { get; set; }

            public string ImageUrl { get; set; }

            public string Context { get; set; }
        }
    }
}
