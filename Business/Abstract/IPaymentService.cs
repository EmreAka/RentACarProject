using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Pay(Card card, int carId);
    }
}