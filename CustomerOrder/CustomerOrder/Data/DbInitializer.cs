using CustomerOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CustomerOrderContext context)
        {
            context.Database.EnsureCreated();

          
        }
    }
}
