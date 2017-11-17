namespace CarDealer.Services.Models.Customers
{
    using Cars;
    using Parts;
    using System.Collections.Generic;

    public class CustomerDetailedModel : CustomerModel
    {
        public int TotalCarsBought { get; set; }

        public double MoneySpentOnParts { get; set; }

        public ICollection<CarModel> Cars { get; set; }
        
        public ICollection<PartModel> Parts { get; set; }

    }
}
