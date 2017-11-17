namespace CarDealer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        [Key]
        public int Id { get; set; }
        
        [Range(0,double.MaxValue)]
        public double Discount { get; set; }

        public int CarId { get; set; }

        public int CustomerId { get; set; }

        public Car Car { get; set; }

        public Customer Customer { get; set; }
    }
}
