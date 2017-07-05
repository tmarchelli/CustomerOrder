using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Models
{
    public class Orders
    {
        public int OrdersID { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public int Quantity { get; set; }
        public Customer Customer { get; set; }
    }
}
