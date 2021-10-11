using System.Collections.Generic;
namespace todolistReactAsp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<TagJob> TagJobs { get; set; }
    }
}