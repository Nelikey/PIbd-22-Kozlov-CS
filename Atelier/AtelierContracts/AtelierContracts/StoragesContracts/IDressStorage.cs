using AtelierContracts.BindingModels;
using AtelierContracts.ViewModels;
using System.Collections.Generic;

namespace AtelierContracts.StoragesContracts
{
    public interface IDressStorage
    {
        List<DressViewModel> GetFullList();

        List<DressViewModel> GetFilteredList(DressBindingModel model);

        DressViewModel GetElement(DressBindingModel model);

        void Insert(DressBindingModel model);

        void Update(DressBindingModel model);

        void Delete(DressBindingModel model);
    }
}
