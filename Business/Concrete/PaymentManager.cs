using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly ICarService _carService;

        public PaymentManager(ICarService carService)
        {
            _carService = carService;
        }
        
        [SecuredOperation]
        public IResult Pay(Card card, int carId)
        {
            return new SuccessResult();
        }
    }
}