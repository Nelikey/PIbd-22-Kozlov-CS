using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using AtelierContracts.StoragesContracts;
using AtelierContracts.ViewModels;

namespace AtelierBusinessLogic.BusinessLogics
{
    public class DressLogic : IDressLogic
    {
        private readonly IDressStorage _dressStorage;

        public DressLogic(IDressStorage dressStorage)
        {
            _dressStorage = dressStorage;
        }

        public List<DressViewModel> Read(DressBindingModel model)
        {
            if (model == null)
            {
                return _dressStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DressViewModel> { _dressStorage.GetElement(model) };
            }
            return _dressStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DressBindingModel model)
        {
            var element = _dressStorage.GetElement(new DressBindingModel
            {
                DressName = model.DressName,
                Price = model.Price,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть путевка с таким названием");
            }
            if (model.Id.HasValue)
            {
                _dressStorage.Update(model);
            }
            else
            {
                _dressStorage.Insert(model);
            }
        }

        public void Delete(DressBindingModel model)
        {
            var element = _dressStorage.GetElement(new DressBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Путевка не найдена");
            }
            _dressStorage.Delete(model);
        }
    }
}