namespace DesafioSouthSystem.Domain.Entities
{
    public class Customer: Entity
    {
        public Customer(long cNPJ, string name, string businessArea)
        {
            CNPJ = cNPJ;
            Name = name;
            BusinessArea = businessArea;
        }

        public long CNPJ { get; private set; }
        public string Name { get; private set; }
        public string BusinessArea { get; private set; }
    }
}
