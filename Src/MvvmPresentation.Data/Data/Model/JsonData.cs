using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmPresentation.Data.Data.Model
{
    public class JsonData
    {
        public JsonData(string ordersJson, string customersJson)
        {
            OrdersJson = ordersJson;
            CustomersJson = customersJson;
        }

        public string OrdersJson { get; }

        public string CustomersJson { get; }
    }
}
