using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Core.Utilities.PaymentSystemAdapter
{
    public class Payment
    {
        public static IResult Pay(Card card)
        {
            return new SuccessResult("The payment has been made successfully.");
        }
    }
}