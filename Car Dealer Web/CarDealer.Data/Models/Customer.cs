namespace CarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        
        public bool  IsYoungDriver { get; set; }

        //public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        

    }
}
