using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UrlDatasource([FromBody]DataManagerRequest dm){
            IEnumerable<OrderDetails> DataSource = OrderDetails.GetOrderDetails();
            int count = DataSource.Count();
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        public IActionResult Insert([FromBody]CRUDModel<OrderDetails> value){
            OrderDetails.GetOrderDetails().Insert(0,value.Value);
            return Json(value.Value);
        }

        public IActionResult Update([FromBody]CRUDModel<OrderDetails> value){
            var ord = value.Value;
            OrderDetails val = OrderDetails.GetOrderDetails().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.CustomerID = ord.CustomerID;
            val.Freight = ord.Freight;
            val.ShipCity = ord.ShipCity;
            val.ShipCountry = ord.ShipCountry;
            return Json(value.Value);
        }

        public void Delete([FromBody]CRUDModel<OrderDetails> value){
            OrderDetails.GetOrderDetails().Remove(OrderDetails.GetOrderDetails()
            .Where(or => or.OrderID == Convert.ToUInt32(value.Key))
            .FirstOrDefault());
        }

        // public void CrudAction([FromBody]CRUDModel<OrderDetails> value){
        //     if(value.Action == "insert"){
        //         OrderDetails.GetOrderDetails().Insert(0,value.Value);
        //     }
        //     else if(value.Action == "update"){
        //         var ord = value.Value;
        //         OrderDetails val = OrderDetails.GetOrderDetails().Where(or => or.OrderID == ord.OrderID)
        //         .FirstOrDefault();
        //         val.OrderID = ord.OrderID;
        //         val.CustomerID = ord.CustomerID;
        //         val.Freight = ord.Freight;
        //         val.ShipCity = ord.ShipCity;
        //         val.ShipCountry = ord.ShipCountry;   
        //     }
        //     else if(value.Action == "remove"){
        //          OrderDetails.GetOrderDetails().Remove(OrderDetails.GetOrderDetails()
        //         .Where(or => or.OrderID == Convert.ToUInt32(value.Key))
        //         .FirstOrDefault());   
        //     }
        // }

        // public void BatchUpdate([FromBody]CRUDModel<OrderDetails> value){
        //     if(value.Added != null){
        //         for (int i = 0; i < value.Added.Count(); i++)
        //         {
        //             OrderDetails.GetOrderDetails().Insert(0,value.Added[i]);
        //         }
        //     }
        //     if(value.Changed != null){
        //         for (int i = 0; i < value.Changed.Count(); i++)
        //         {
        //             var ord = value.Changed[i];
        //             OrderDetails val = OrderDetails.GetOrderDetails().Where(or => or.OrderID == ord.OrderID)
        //             .FirstOrDefault();
        //             val.OrderID = ord.OrderID;
        //             val.CustomerID = ord.CustomerID;
        //             val.Freight = ord.Freight;
        //             val.ShipCity = ord.ShipCity;
        //             val.ShipCountry = ord.ShipCountry;   
        //         }
        //     }
        //     if(value.Deleted != null){
        //         for (int i = 0; i < value.Deleted.Count(); i++)
        //         {
        //          OrderDetails.GetOrderDetails().Remove(OrderDetails.GetOrderDetails()
        //         .Where(or => or.OrderID == Convert.ToUInt32(value.Deleted[i].OrderID))
        //         .FirstOrDefault());   
        //         }
        //     }
        // }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }

    public class OrderDetails
    {
        public static List<OrderDetails> order = new List<OrderDetails>();
        public int OrderID { get; set; }
        public string CustomerID { get; set; }

        public double Freight { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }

        public OrderDetails(){

        }

        public OrderDetails(int OrderID, string CustomerID, double Freight, string ShipCity, string ShipCountry){
            this.OrderID = OrderID;
            this.CustomerID = CustomerID;
            this.Freight = Freight;
            this.ShipCity = ShipCity;
            this.ShipCountry = ShipCountry;
        }

        public static List<OrderDetails> GetOrderDetails(){
            if (order.Count() == 0){
                int code = 1000;
                for(int i = 1; i < 3; i++){
                    order.Add(new OrderDetails(code + 1, "ALFKI", 2.3 * i, "Berlin", "Denmark"));
                    order.Add(new OrderDetails(code + 2, "ANATR", 3.3 * i, "Madrid", "Brazil"));
                    order.Add(new OrderDetails(code + 3, "ANTON", 4.3 * i, "Cholchester", "Germany"));
                    order.Add(new OrderDetails(code + 4, "BLONP", 5.3 * i, "Marseille", "Austria"));
                    order.Add(new OrderDetails(code + 5, "BOLID", 6.3 * i, "Tsawassen", "Switzerland"));
                    code += 5;
                }
            }
            return order;
        }
    }
}
