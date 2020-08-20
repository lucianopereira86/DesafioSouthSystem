using DesafioSouthSystem.Domain.Entities;
using DesafioSouthSystem.Domain.Validations;
using DesafioSouthSystem.Shared.CommandQuery;

namespace DesafioSouthSystem.Domain.Handlers.Entities
{
    public class SalesmanHandler: EntityHandler<Salesman>
    {
        public Salesman Collect(string[] fields, CommandQueryResult commandResult)
        {
            long.TryParse(GetString(fields, 1), out long cpf);
            string name = GetString(fields, 2);
            float.TryParse(GetString(fields, 3), out float salary);

            var salesman = new Salesman(cpf, name, salary);
            commandResult.Validate(salesman, new SalesmanValidation());

            return salesman;
        }
    }
}
