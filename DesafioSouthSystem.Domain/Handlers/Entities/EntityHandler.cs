using DesafioSouthSystem.Domain.Entities;

namespace DesafioSouthSystem.Domain.Handlers.Entities
{
    public class EntityHandler<T> where T : Entity
    {
        public string GetString(string[] fields, int index)
        {
            return fields.Length > index ? fields[index] : null;
        }
    }
}
