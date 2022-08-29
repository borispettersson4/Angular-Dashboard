using Core.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore
{
    public class DataSeed
    {
        private readonly ApiContext _context;

        public DataSeed(ApiContext context)
        {
            _context = context;
        }

        public void SeedData(int nCustomers, int nOrders, int nServers)
        {
            if (!_context.Customers.Any())
                SeedCustomers(nCustomers);
            if (!_context.Orders.Any())
                SeedOrders(nOrders);
            if (!_context.Servers.Any())
                SeedServers(nServers);
            if(_context.ChangeTracker.HasChanges())
                _context.SaveChanges();
        }

        public void SeedCustomers(int n)
        {
            var customers = BuildCustomerList(n);

            foreach(var customer in customers)
            {
                _context.Add(customer);
            }
        }

        private List<Customer> BuildCustomerList(int n)
        {
            var customers = new List<Customer>();

            for (int i = 1; i <= n; i++)
            {
                var name = Helpers.GenerateCustomerName();

                customers.Add(new Customer { 
                    Id=i,
                    Name=name,
                    Email=Helpers.GenerateCustomerEmail(name),
                    State=Helpers.GenerateCustomerState()
                });
            }

            return customers;
        }

        public void SeedOrders(int n)
        {
            var orders = BuildOrdersList(n);

            foreach (var order in orders)
            {
                _context.Add(order);
            }
        }

        private List<Order> BuildOrdersList(int n)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for (int i = 1; i <= n; i++)
            {
                var randCustomerid = rand.Next(_context.Customers.Count());
                var placed = Helpers.GenerateRandomOrderPlaced();
                var customer = _context.Customers.FirstOrDefault(x => x.Id == randCustomerid);

                if (customer == null && _context.Customers.Count() > 0)
                    customer = _context.Customers.First();

                orders.Add(new Order
                {
                    Id = i,
                    Customer = customer,
                    Total = Helpers.GenerateRandomOrderTotal(),
                    Placed = Helpers.GenerateRandomOrderPlaced(),
                    Fulfilled = Helpers.GetRandomOrderCompleted(placed)
                });
            }

            return orders;
        }

        public void SeedServers(int n)
        {
            var servers = BuildServersList(n);

            foreach (var server in servers)
            {
                _context.Add(server);
            }
        }

        private List<Server> BuildServersList(int n)
        {
            var servers = new List<Server>();
            var rand = new Random();

            for (int i = 1; i <= n; i++)
            {
                var randCustomerid = rand.Next(_context.Customers.Count());
                var placed = Helpers.GenerateRandomOrderPlaced();

                servers.Add(new Server
                {
                    Id = i,
                    Name = Helpers.GenerateRandomServerName() + " # " + i,
                    isOnline = Helpers.GenerateRandomBool()
                });
            }

            return servers;
        }
    }
}
