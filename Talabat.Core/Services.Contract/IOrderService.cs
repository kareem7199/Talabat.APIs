﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Services.Contract
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string basketId, string deliveryMethodId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
		Task<Order> GetOrderByIdForUserAsyncAsync(string buyerEmail, int orderId);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}
