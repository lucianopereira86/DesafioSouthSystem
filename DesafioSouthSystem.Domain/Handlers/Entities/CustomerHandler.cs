using DesafioSouthSystem.Domain.Entities;
using DesafioSouthSystem.Domain.Validations;
using DesafioSouthSystem.Shared.CommandQuery;

namespace DesafioSouthSystem.Domain.Handlers.Entities
{
    public class CustomerHandler : EntityHandler<Customer>
    {
        public Customer Collect(string[] fields, CommandQueryResult commandResult)
        {
            long.TryParse(GetString(fields, 1), out long cnpj);
            string name = GetString(fields, 2);
            string businessArea = GetString(fields, 3);

            var customer = new Customer(cnpj, name, businessArea);
            commandResult.Validate(customer, new CustomerValidation());

            return customer;
        }
    }
}
