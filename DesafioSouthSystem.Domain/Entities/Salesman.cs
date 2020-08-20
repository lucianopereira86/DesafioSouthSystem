namespace DesafioSouthSystem.Domain.Entities
{
    public class Salesman : Entity
    {
        public Salesman(long cPF, string name, float salary)
        {
            CPF = cPF;
            Name = name;
            Salary = salary;
        }

        public long CPF { get; private set; }
        public string Name { get; private set; }
        public float Salary { get; private set; }
    }
}
