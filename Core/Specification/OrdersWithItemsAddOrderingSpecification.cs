using System;
using System.Linq.Expressions;
using Core.Entities.OrderAddregate;

namespace Core.Specification
{
    public class OrdersWithItemsAddOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAddOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsAddOrderingSpecification(int id, string email) 
        : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}