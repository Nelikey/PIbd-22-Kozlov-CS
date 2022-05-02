using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using AtelierContracts.Enums;
using AtelierFileImplement.Models;

namespace AtelierFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string DressFileName = "Dress.xml";
        private readonly string ClientFileName = "Client.xml";

        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dress> Dresses { get; set; }
        public List<Client> Clients { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Dresses = LoadDresses();
            Clients = LoadClients();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null) instance = new FileDataListSingleton();
            return instance;
        }

        public static void SaveData()
        {
            GetInstance().SaveComponents();
            GetInstance().SaveOrders();
            GetInstance().SaveDresses();
            GetInstance().SaveClients();
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();

            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();

            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();

                foreach (var elem in xElements)
                {
                    if (!Enum.TryParse(elem.Element("Status").Value, out OrderStatus orderStatus))
                    {
                        orderStatus = OrderStatus.Принят;
                    }

                    DateTime? orderDateImplement;
                    if (elem.Element("DateImplement").Value == null || elem.Element("DateImplement").Value == "")
                    {
                        orderDateImplement = null;
                    }
                    else
                    {
                        orderDateImplement = Convert.ToDateTime(elem.Element("DateImplement").Value);
                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DressId = Convert.ToInt32(elem.Element("DressId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = orderStatus,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = orderDateImplement
                    });
                }
            }
            return list;
        }

        private List<Dress> LoadDresses()
        {
            var list = new List<Dress>();

            if (File.Exists(DressFileName))
            {
                var xDocument = XDocument.Load(DressFileName);
                var xElements = xDocument.Root.Elements("Dress").ToList();

                foreach (var elem in xElements)
                {
                    var dressComp = new Dictionary<int, int>();
                    foreach (var component in
                        elem.Element("DressComponents").Elements("DressComponent").ToList())
                    {
                        dressComp.Add(Convert.ToInt32(component.Element("Key").Value),
                            Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Dress
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DressName = elem.Element("DressName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        DressComponents = dressComp
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                        new XAttribute("Id", component.Id),
                        new XElement("ComponentName", component.ComponentName)));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("DressId", order.DressId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveDresses()
        {
            if (Dresses != null)
            {
                var xElement = new XElement("Dresses");
                foreach (var dress in Dresses)
                {
                    var compElement = new XElement("DressComponents");
                    foreach (var component in dress.DressComponents)
                    {
                        compElement.Add(new XElement("DressComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Dress",
                        new XAttribute("Id", dress.Id),
                        new XElement("DressName", dress.DressName),
                        new XElement("Price", dress.Price),
                        compElement));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(DressFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}