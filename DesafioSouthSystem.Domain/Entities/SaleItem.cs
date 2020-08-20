namespace DesafioSouthSystem.Domain.Entities
{
    public class SaleItem
    {
        public SaleItem(int itemID, int quatity, decimal price)
        {
            ItemID = itemID;
            Quatity = quatity;
            Price = price;
        }

        public int ItemID { get; private set; }
        public int Quatity { get; private set; }
        public decimal Price { get; private set; }
    }
}
