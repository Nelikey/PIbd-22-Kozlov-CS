using System;
using System.Collections.Generic;
using System.Text;
using AtelierContracts.BindingModels;
using AtelierContracts.ViewModels;

namespace AtelierContracts.BusinessLogicsContracts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);

        void CreateOrUpdate(ComponentBindingModel model);

        void Delete(ComponentBindingModel model);
    }
}
