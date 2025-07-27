using System.ComponentModel.DataAnnotations;

namespace TaskManger.Api.Entities
{
    public class Member:BaseEntity
    {
        public required string Name { get; set; }
        //ensure email of team member is valid
        [EmailAddress]
        public string Email { get; set; }

        public int TeskId { get; set; }
        public List<Tasks>? tasks { get; set; }

        
    }
}
