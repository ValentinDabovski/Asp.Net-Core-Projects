namespace CarDealer.Web.ViewModels.Cars
{
    using System.Collections.Generic;
    using Services.Models.Cars;

    public class AllCarsModel
    {
        public IEnumerable<CarModel> Cars { get; set; }

        public string CarMake { get; set; }
    }
}
