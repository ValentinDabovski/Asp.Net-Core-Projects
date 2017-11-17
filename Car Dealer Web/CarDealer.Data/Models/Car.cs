﻿namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public Car()
        {
            this.Sales = new List<Sale>();
            this.PartCars = new List<PartCar>();
        }

        [Key]
        public int Id  { get; set; }    

        [Required]
        public string Make  { get; set; }    

        [Required]
        public string Model  { get; set; }    
        
        [Range(0,long.MaxValue)]
        public long TravelledDistance  { get; set; }
        
        public  ICollection<Sale> Sales { get; set; }       

        public  ICollection<PartCar> PartCars { get; set; }

        
    }
}
