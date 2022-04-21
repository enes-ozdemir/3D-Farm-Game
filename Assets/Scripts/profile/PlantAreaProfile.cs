using System.Collections;
using System.Collections.Generic;

namespace Farm.Profile
{
    public class PlantAreaProfile : IProfile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<ProductJob> ProductJobs { get; set; }

        public PlantAreaProfile(string Name, string Type, List<ProductJob> ProductJobs)
        {
            this.Name = Name;
            this.Type = Type;
            this.ProductJobs = ProductJobs;
        }
    }
}

