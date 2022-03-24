using Core.Entities;

namespace Entity.Concrete
{
    public class Engine : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}