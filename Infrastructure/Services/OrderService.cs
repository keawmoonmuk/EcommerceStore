using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAddregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;       // uinit of work
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basjet from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            //get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);

            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create ordre
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            //  save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;        // check reuslt

            // delete basket
            await _basketRepo.DeleteBasketAsync(basketId);


            //return order
            return order;
        }

        //get delivery all
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        //get order id
        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAddOrderingSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        //get order user
        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAddOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}