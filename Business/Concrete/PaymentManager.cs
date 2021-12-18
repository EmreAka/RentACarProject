using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.FindeksAdapter;
using Core.Utilities.PaymentSystemAdapter;
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
            IResult rules = BusinessRules.Run(CheckIfUserHasEnoughFindeksScore(carId));
            var result = Payment.Pay(card);
            if (result.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        
        public IResult CheckIfUserHasEnoughFindeksScore(int carId)
        {
            var requiredFindeksScore = _carService.GetById(carId).Data.RequiredFindeksScore;
            var findeksScoreOfCustomer = FindexAdapter.CalculateFindeksScore();
            if (findeksScoreOfCustomer < requiredFindeksScore)
            {
                return new ErrorResult("Findeks score is too low.");
            } else
            {
                return new SuccessResult("Findeks score is higher than required.");
            }
        }
    }
}