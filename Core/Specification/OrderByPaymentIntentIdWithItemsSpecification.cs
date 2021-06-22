using System;
using System.Linq.Expressions;
using Core.Entities.OrderAddregate;

namespace Core.Specification
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId)
         : base(o => o.PaymentIntenId == paymentIntentId)
        {
        }
    }
}