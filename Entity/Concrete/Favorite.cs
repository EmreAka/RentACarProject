using Core.Entities;

namespace Entity.Concrete
{
    public class Favorite: IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
    }
}