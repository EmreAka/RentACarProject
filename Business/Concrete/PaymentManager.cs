using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private ICarService _carService;

        public PaymentManager(ICarService carService)
        {
            _carService = carService;
        }

        public IResult Pay(Card card, int carId)
        {
            return new SuccessResult();
        }
    }
}