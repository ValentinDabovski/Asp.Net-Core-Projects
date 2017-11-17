namespace CarDealer.Web.ViewModels.Customers
{
    using Services.Enums;
    using Services.Models.Customers;
    using System.Collections.Generic;


    public class AllCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
