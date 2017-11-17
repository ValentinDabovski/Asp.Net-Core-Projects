namespace CarDealer.Services.Interfaces
{
    using Enums;
    using Models.Customers;
    using System.Collections.Generic;

    public interface ICustomerControllerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection orderDirection);

        CustomerDetailedModel DetailedCustomerModel(int id);

        void AddCustomer(CustomerModel customer);

        CustomerModel Edit(int? id);

        void Edit(int id, CustomerEditModel model);

        CustomerDeleteModel GetCustomerByIdToDelete(int id);

        void ConfirmCustomerDelete(int id);
    }
}
