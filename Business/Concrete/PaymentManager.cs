using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.PaymentSystemAdapter;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        public IResult Pay(Card card)
        {
            var result = Payment.Pay(card);
            if (result.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}