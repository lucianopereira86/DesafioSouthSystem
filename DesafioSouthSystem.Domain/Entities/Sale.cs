using System.Collections.Generic;
using System.Linq;

namespace DesafioSouthSystem.Domain.Entities
{
    public class Sale : Entity
    {
        private readonly IList<SaleItem> _saleItems;
        public Sale(int saleID, string salesmanName)
        {
            SaleID = saleID;
            SalesmanName = salesmanName;
            _saleItems = new List<SaleItem>();
        }

        public int SaleID { get; private set; }
        public string SalesmanName { get; private set; }
        public IReadOnlyCollection<SaleItem> SaleItems { get { return _saleItems.ToArray(); } }

        public void AddSaleItem(SaleItem saleItem)
        {
            _saleItems.Add(saleItem);
        }
    }
}
