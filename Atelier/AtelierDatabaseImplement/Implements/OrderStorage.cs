﻿using AtelierContracts.BindingModels;
using AtelierContracts.StorageContracts;
using AtelierContracts.ViewModels;
using AtelierDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            
            using var context = new AtelierDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AtelierDatabase();
            var order = context.Orders
            .Include(rec => rec.Dress)
            .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .FirstOrDefault(rec => rec.Id == model.Id ||
            rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AtelierDatabase();
            return context.Orders
               .Include(rec => rec.Dress)
               .Include(rec => rec.Client)
               .Include(rec => rec.Implementer)
               .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue &&
                    rec.DateCreate.Date == model.DateCreate.Date) ||
                    (model.DateFrom.HasValue && model.DateTo.HasValue &&
                    rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <=
                    model.DateTo.Value.Date) ||
                    (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                    (model.SearchStatus.HasValue && model.SearchStatus.Value ==
                    rec.Status) ||
                    (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && model.Status == rec.Status))
               .Select(CreateModel)
               .ToList();
        }

        public List<OrderViewModel> GetFullList()
        {
            using var context = new AtelierDatabase();
            return context.Orders
            .Include(rec => rec.Dress)
            .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new AtelierDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new AtelierDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.DressId = model.DressId;
            order.ClientId = (int)model.ClientId;
            order.ImplementerId = model.ImplementerId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            
            return new OrderViewModel
            {
                Id = order.Id,
                DressId = order.DressId,
                DressName = order.Dress.DressName,
                ClientFIO = order.Client.ClientFIO,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = order.ImplementerId.HasValue ? order.Implementer.ImplementerFIO : string.Empty,
                Count = order.Count,
                Sum = order.Sum,
                Status = Enum.GetName(order.Status),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
