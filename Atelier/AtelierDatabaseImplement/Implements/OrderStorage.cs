using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtelierContracts.BindingModels;
using AtelierContracts.StoragesContracts;
using AtelierContracts.ViewModels;
using AtelierDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AtelierDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new AtelierDatabase();
            return context.Orders.Include(rec => rec.Dress).Select(CreateModel).ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new AtelierDatabase();
            return context.Orders.Include(rec => rec.Dress).
                Where(rec => rec.DressId == model.DressId).Select(CreateModel).ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new AtelierDatabase();
            var order = context.Orders.Include(rec => rec.Dress).
                FirstOrDefault(rec => rec.DressId == model.DressId || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new AtelierDatabase();
            Order order = new Order();
            CreateModel(model, order, context);
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new AtelierDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element, context);
            context.SaveChanges();
        }

        public void Delete(OrderBindingModel model)
        {
            using var context = new AtelierDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Order CreateModel(OrderBindingModel model, Order order, AtelierDatabase context)
        {
            order.DressId = model.DressId;
            order.Dress = context.Dresses.FirstOrDefault(rec => rec.Id == model.DressId);
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.CreationDate = model.DateCreate;
            order.ImplementDate = model.DateImplement;
            return order;
        }

        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                DressId = order.DressId,
                DressName = order.Dress.DressName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.CreationDate,
                DateImplement = order.ImplementDate
            };
        }
    }
}