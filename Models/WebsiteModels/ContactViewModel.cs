using System.Collections.Generic;

namespace KennedyLabsWebsite.Models
{
    public class ContactViewModel
    {
        public ICollection<AddressBlockModel> AddressBlockModels { get; set; }

        public class AddressBlockModel
        {
            public string Name { get; set; }

            public ICollection<AddressLineModel> AddressLines { get; set; }

        }

        public class AddressLineModel
        {
            public string Type { get; set; }

            public string Label { get; set; }

            public string Value { get; set; }
        }
    }
}
