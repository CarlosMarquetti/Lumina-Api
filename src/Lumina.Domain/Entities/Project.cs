namespace Lumina.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public int DesignerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Array? Midia { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public Designer Designer { get; set; }
    }
}