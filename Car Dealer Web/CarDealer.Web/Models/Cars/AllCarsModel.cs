namespace CarDealer.Web.Models.Cars
{
    using System.Collections.Generic;
    using Services.Models;


    public class AllCarsModel
    {
        public IEnumerable<CarModel> Cars { get; set; }
    }
}
