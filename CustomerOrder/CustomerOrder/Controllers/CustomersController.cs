using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerOrder.Models;

namespace CustomerOrder.Controllers
{
    
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private readonly CustomerOrderContext _context;

        public CustomersController(CustomerOrderContext context)
        {
            _context = context;
        }

        // GET: /Customers
        [HttpGet]
        public IEnumerable<SummaryCustomer> GetCustomers()
        {
            List<SummaryCustomer> SummaryCustomers = new List<SummaryCustomer>();
            var Customers = _context.Customers.ToList();
            //Then filter only the necesary fields (summary)
            foreach (Customer ListValue in Customers)
            {
                SummaryCustomer SummaryTmp = new SummaryCustomer();
                SummaryTmp.CompanyName = ListValue.CompanyName;
                SummaryTmp.TelephoneNumber = ListValue.TelephoneNumber;
                SummaryCustomers.Add(SummaryTmp);
            }
            return SummaryCustomers;
        }

        // GET: /Customers/{id}
        [HttpGet("{customerid}")]
        public async Task<IActionResult> GetCustomer(int customerid)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerID == customerid);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        // PUT: /Customers/{id}
        [HttpPut("{customerid}")]
        public async Task<IActionResult> PutCustomer(int customerid, [FromBody] Customer customer)
        {
     
            if (customerid != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: /Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
      
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
        }

        // DELETE: /Customers/{id}
        [HttpDelete("{customerid}")]
        public async Task<IActionResult> DeleteCustomer(int customerid)
        {
        
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerID == customerid);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        //Here begins methos for that involves orders
        [HttpGet("{customerid}/orders")]
        public IEnumerable<SummaryOrder> GetOrdersByCustomerID(int customerid)
        {
            List<SummaryOrder> SummaryOrders = new List<SummaryOrder>();

            //First recover a full list
            var Orders= _context.Orders.Where(m => m.Customer.CustomerID == customerid).ToList();

            //Then filter only the necesary fields (summary)
            foreach (Orders ListValue in Orders)
            {
                SummaryOrder SummaryTmp = new SummaryOrder();
                SummaryTmp.Amount = ListValue.Amount;
                SummaryTmp.Description = ListValue.Description;
                SummaryOrders.Add(SummaryTmp);
            }

            return SummaryOrders;
        }


        // GET: /Customers/{id}
        [HttpGet("{customerid}/orders/{orderid}")]
        public async Task<IActionResult> GetOrderByCustomerAndOrder(int customerid, int orderid)
        {
            var Order = await _context.Orders.Include("Customer").SingleOrDefaultAsync(m => m.Customer.CustomerID == customerid && m.OrdersID == orderid);

            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // DELETE: /Order
        [HttpDelete("{customerid}/orders/{orderid}")]
        public async Task<IActionResult> DeleteOrder(int customerid, int orderid)
        {

            var Order = await _context.Orders.Include("Customer").SingleOrDefaultAsync(m => m.Customer.CustomerID == customerid && m.OrdersID == orderid);
            if (Order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();

            return Ok(Order);
        }

        // PUT: /Customers/{id}
        [HttpPut("{customerid}/orders/{orderid}")]
        public async Task<IActionResult> PutOrder(int customerid, int orderid, [FromBody] Orders order)
        {

            if (customerid != order.Customer.CustomerID)
            {
                return BadRequest();
            }

            if (orderid != order.OrdersID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: /orders
        [HttpPost("{customerid}/orders")]
        public async Task<IActionResult> PostOrder(int customerid, [FromBody] Orders Order)
        {
            //first verify if customerid its the same of the object Order
            var Customer = new Customer();

            //Just to be sure i recover the customer...
            if (customerid==Order.Customer.CustomerID)
                Customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerID == customerid);

            if (Customer != null)
            {
                if (Customer.CustomerID > 0)
                {
                    //And then asignate to Order Object
                    Order.Customer = Customer;
                }

            }
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}