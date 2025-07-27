namespace TaskManger.Api.Entities
{
    public class Tasks: BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

       


    }
    
}
