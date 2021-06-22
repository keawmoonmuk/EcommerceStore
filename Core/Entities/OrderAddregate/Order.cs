using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAddregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order( IReadOnlyList<OrderItem> orderItems,  string buyerEmail, Address shipToAddress,
         DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal; 
            PaymentIntenId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntenId { get; set; }

        public decimal Gettotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}