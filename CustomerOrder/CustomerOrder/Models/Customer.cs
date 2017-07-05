using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public String CompanyName { get; set; }
        public String Country { get; set; }
        public String Address { get; set; }
        public String ContactName { get; set; }
        public String TelephoneNumber { get; set; }
        
    }
}
