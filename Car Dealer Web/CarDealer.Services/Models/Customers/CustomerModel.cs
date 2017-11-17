namespace CarDealer.Services.Models.Customers
{
    using System.ComponentModel.DataAnnotations;

    using System;

    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200,ErrorMessage = "Maximum 200 symbols"),MinLength(3,ErrorMessage = "Minimum 3 symbols")]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
