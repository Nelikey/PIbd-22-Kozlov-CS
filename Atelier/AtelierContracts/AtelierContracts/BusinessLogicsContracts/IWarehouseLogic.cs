using System;
using System.Collections.Generic;
using System.Text;
using AtelierContracts.BindingModels;
using AtelierContracts.ViewModels;

namespace AtelierContracts.BusinessLogicsContracts
{
    public interface IWarehouseLogic
    {
        public List<WarehouseViewModel> Read(WarehouseBindingModel model);

        public void CreateOrUpdate(WarehouseBindingModel model);

        public void Delete(WarehouseBindingModel model);

        public void AddComponents(ReplenishmentBindingModel model);
    }
}
