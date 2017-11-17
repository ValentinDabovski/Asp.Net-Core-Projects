namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public  class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }

        [Range(0,int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        public int SupplierId { get; set; }

        public Supplier  Supplier { get; set; }

        public ICollection<PartCar> PartCars { get; set; } = new List<PartCar>();
    }
}
