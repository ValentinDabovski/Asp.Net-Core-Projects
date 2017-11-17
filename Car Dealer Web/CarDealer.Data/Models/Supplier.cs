namespace CarDealer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.Collections.Generic;

    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public bool IsImporterType { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
