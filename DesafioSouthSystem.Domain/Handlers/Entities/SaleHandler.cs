using DesafioSouthSystem.Domain.Entities;
using DesafioSouthSystem.Domain.Validations;
using DesafioSouthSystem.Shared.CommandQuery;

namespace DesafioSouthSystem.Domain.Handlers.Entities
{
    public class SaleHandler : EntityHandler<Sale>
    {
        public Sale Collect(string[] fields, CommandQueryResult commandResult)
        {
            int.TryParse(GetString(fields, 1), out int saleID);
            string saleItems = GetString(fields, 2);
            string salesmanName = GetString(fields, 3);

            var sale = new Sale(saleID, salesmanName);
            commandResult.Validate(sale, new SaleValidation());

            if (commandResult.Valid)
            {
                CollectSaleItems(sale, saleItems, commandResult);
                if (!commandResult.Valid)
                    return null;
            }
            return sale;
        }

        private void CollectSaleItems(Sale sale, string saleItems, CommandQueryResult commandResult)
        {
            string[] items = saleItems[1..^1].Split(",");
            foreach (string item in items)
            {
                string[] fields = item.Split("-");

                int.TryParse(GetString(fields, 0), out int itemID);
                int.TryParse(GetString(fields, 1), out int quantity);
                string sPrice = GetString(fields, 2);
                decimal.TryParse(sPrice?.Replace(".", ","), out decimal price);

                var saleItem = new SaleItem(itemID, quantity, price);
                commandResult.Validate(saleItem, new SaleItemValidation());

                if (!commandResult.Valid)
                    return;

                sale.AddSaleItem(saleItem);
            }
        }
    }
}
