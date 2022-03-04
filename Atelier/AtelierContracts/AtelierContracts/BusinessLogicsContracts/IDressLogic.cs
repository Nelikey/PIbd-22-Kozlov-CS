using System.Collections.Generic;
using AtelierContracts.BindingModels;
using AtelierContracts.ViewModels;

namespace AtelierContracts.BusinessLogicsContracts
{
    public interface IDressLogic
    {
        List<DressViewModel> Read(DressBindingModel model);

        void CreateOrUpdate(DressBindingModel model);

        void Delete(DressBindingModel model);
    }
}
