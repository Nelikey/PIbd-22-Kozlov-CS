using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtelierBusinessLogic.OfficePackage;
using AtelierBusinessLogic.OfficePackage.HelperModels;
using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using AtelierContracts.StoragesContracts;
using AtelierContracts.ViewModels;

namespace AtelierBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IDressStorage _dressStorage;
        private readonly IOrderStorage _orderStorage;

        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IDressStorage dressStorage, IComponentStorage componentStorage, IOrderStorage orderStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _dressStorage = dressStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        // Получение списка изделий с указанием компонентов в них
        public List<ReportDressComponentViewModel> GetDressComponent()
        {
            var dresses = _dressStorage.GetFullList();
            var list = new List<ReportDressComponentViewModel>();

            foreach (var dress in dresses)
            {
                var record = new ReportDressComponentViewModel
                {
                    DressName = dress.DressName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in dress.DressComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        // Получение списка заказов за определенный период
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                DressName = x.DressName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
        }

        // Сохранение изделий в файл-Word
        public void SaveDressesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список платьев",
                Dresses = _dressStorage.GetFullList()
            });
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Dresses = _dressStorage.GetFullList()
            });
        }

        // Сохранение изделий с указаеним компонентов в файл-Excel
        public void SaveDressComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список платьев",
                DressComponents = GetDressComponent()
            });
        }

        // Сохранение заказов в файл-Pdf
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}