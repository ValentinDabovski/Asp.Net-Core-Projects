namespace CarDealer.Services.Models.Parts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class PartModel
    {
        public int Id { get; set; } 

        [Required]
        [Display(Name = "Part Name")]
        [MaxLength(100)]
        public string PartName { get; set; }


        [Required]
        [Display(Name = "Part Price")]
        [Range(0,Double.MaxValue)]
        public double PartPrice { get; set; }
    }               
}
