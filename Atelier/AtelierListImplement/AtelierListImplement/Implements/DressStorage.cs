using AtelierContracts.BindingModels;
using AtelierContracts.StoragesContracts;
using AtelierContracts.ViewModels;
using AtelierListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtelierListImplement.Implements
{
    public class DressStorage : IDressStorage
    {
        private readonly DataListSingleton source;
        public DressStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<DressViewModel> GetFullList()
        {
            var result = new List<DressViewModel>();
            foreach (var dress in source.Dresses)
            {
                result.Add(CreateModel(dress));
            }
            return result;
        }
        public List<DressViewModel> GetFilteredList(DressBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<DressViewModel>();
            foreach (var product in source.Dresses)
            {
                if (product.DressName.Contains(model.DressName))
                {
                    result.Add(CreateModel(product));
                }
            }
            return result;
        }
        public DressViewModel GetElement(DressBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var product in source.Dresses)
            {
                if (product.Id == model.Id || product.DressName == model.DressName)
                {
                    return CreateModel(product);
                }
            }
            return null;
        }
        public void Insert(DressBindingModel model)
        {
            var tempProduct = new Dress
            {
                Id = 1,
                DressComponents = new
            Dictionary<int, int>()
            };
            foreach (var product in source.Dresses)
            {
                if (product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
            }
            source.Dresses.Add(CreateModel(model, tempProduct));
        }
        public void Update(DressBindingModel model)
        {
            Dress tempDress = null;
            foreach (var product in source.Dresses)
            {
                if (product.Id == model.Id)
                {
                    tempDress = product;
                }
            }
            if (tempDress == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempDress);
        }
        public void Delete(DressBindingModel model)
        {
            for (int i = 0; i < source.Dresses.Count; ++i)
            {
                if (source.Dresses[i].Id == model.Id)
                {
                    source.Dresses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Dress CreateModel(DressBindingModel model, Dress dress)
        {
            dress.DressName = model.DressName;
            dress.Price = model.Price;
            // удаляем убранные
            foreach (var key in dress.DressComponents.Keys.ToList())
            {
                if (!model.DressComponents.ContainsKey(key))
                {
                    dress.DressComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.DressComponents)
            {
                if (dress.DressComponents.ContainsKey(component.Key))
                {
                    dress.DressComponents[component.Key] =
                    model.DressComponents[component.Key].Item2;
                }
                else
                {
                    dress.DressComponents.Add(component.Key,
                    model.DressComponents[component.Key].Item2);
                }
            }
            return dress;
        }
        private DressViewModel CreateModel(Dress dress)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            var dressComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in dress.DressComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                dressComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new DressViewModel
            {
                Id = dress.Id,
                DressName = dress.DressName,
                Price = dress.Price,
                DressComponents = dressComponents
            };
        }
    }
}
