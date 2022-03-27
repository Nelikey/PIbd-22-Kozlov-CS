using AtelierContracts.BindingModels;
using AtelierContracts.StoragesContracts;
using AtelierContracts.ViewModels;
using AtelierDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AtelierDatabaseImplement.Implements
{
    public class DressStorage : IDressStorage
    {
        public List<DressViewModel> GetFullList()
        {
            using var context = new AtelierDatabase();
            return context.Dresses
            .Include(rec => rec.DressComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<DressViewModel> GetFilteredList(DressBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AtelierDatabase();
            return context.Dresses
            .Include(rec => rec.DressComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.DressName.Contains(model.DressName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public DressViewModel GetElement(DressBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AtelierDatabase();
            var dress = context.Dresses
            .Include(rec => rec.DressComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.DressName == model.DressName || rec.Id == model.Id);
            return dress != null ? CreateModel(dress) : null;
        }
        public void Insert(DressBindingModel model)
        {
            using var context = new AtelierDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Dress dress = new Dress()
                {
                    DressName = model.DressName,
                    Price = model.Price
                };
                context.Dresses.Add(dress);
                context.SaveChanges();
                CreateModel(model, dress, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DressBindingModel model)
        {
            using var context = new AtelierDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Dresses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(DressBindingModel model)
        {
            using var context = new AtelierDatabase();
            Dress element = context.Dresses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Dresses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Dress CreateModel(DressBindingModel model, Dress dress, AtelierDatabase context)
        {
            dress.DressName = model.DressName;
            dress.Price = model.Price;
            if (model.Id.HasValue)
            {
                var dressComponents = context.DressComponents.Where(rec => rec.DressId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.DressComponents.RemoveRange(dressComponents.Where(rec => !model.DressComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in dressComponents)
                {
                    updateComponent.Count = model.DressComponents[updateComponent.ComponentId].Item2;
                    model.DressComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var dc in model.DressComponents)
            {
                context.DressComponents.Add(new DressComponent
                {
                    DressId = dress.Id,
                    ComponentId = dc.Key,
                    Count = dc.Value.Item2
                });
                context.SaveChanges();
            }
            return dress;
        }
        private static DressViewModel CreateModel(Dress dress)
        {
            return new DressViewModel
            {
                Id = dress.Id,
                DressName = dress.DressName,
                Price = dress.Price,
                DressComponents = dress.DressComponents.ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}