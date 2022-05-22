﻿using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using AtelierContracts.Enums;
using AtelierContracts.StorageContracts;
using AtelierContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                DressId = model.DressId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals(Enum.GetName(typeof(OrderStatus), 2)))
            {
                throw new Exception("Заказ не готов");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DressId = order.DressId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан,
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals(Enum.GetName(typeof(OrderStatus), 1)))
            {
                throw new Exception("Заказ не выполняется");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DressId = order.DressId,
                ImplementerId = order.ImplementerId,
                ClientId = order.ClientId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId,
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals(Enum.GetName(typeof(OrderStatus),0)))
            {
                throw new Exception("Заказ не принят");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                DressId = order.DressId,
                ImplementerId = model.ImplementerId,
                ClientId = order.ClientId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }
    }
}
