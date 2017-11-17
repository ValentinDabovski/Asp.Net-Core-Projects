namespace CarDealer.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.Parts;

    public interface IPartControllerService
    {
        IEnumerable<AllPartsModel> All();

        EditPartModel Edit(int? id);

        DeletePartModel Delete(int? id);

        void Delete(int id);

        void CreatePart(string name, double price, int quantity, int suppplierId);

        void Edit(int id, EditPartModel model);






    }
}
