using System.Collections.Generic;
namespace todolistReactAsp.Models

{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }
    }
}