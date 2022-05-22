using AtelierContracts.BindingModels;
using AtelierContracts.BusinessLogicsContracts;
using AtelierContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AtelierRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IDressLogic _dress;
        public MainController(IOrderLogic order, IDressLogic dress)
        {
            _order = order;
            _dress = dress;
        }
        [HttpGet]
        public List<DressViewModel> GetDressList() => _dress.Read(null)?.ToList();
        [HttpGet]
        public DressViewModel GetDress(int dressId) => _dress.Read(new DressBindingModel
        { Id = dressId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}