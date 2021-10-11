using System.Collections.Generic;
namespace todolistReactAsp.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public int Important { get; set; }
        public string File { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<TagJob> TagJob { get; set; }
    }
}