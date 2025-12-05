using System;
using System.Collections.Generic;

namespace Lab14
{
    class Customer
    {
        public uint CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }

        public Customer(uint customerId, string firstName, string lastName, string pesel)
        {
            this.CustomerId = customerId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PESEL = pesel;
        }
    }

    class LoyaltyCard
    {
        public uint CustomerId { get; set; }
        public double Discount { get; set; }

        public LoyaltyCard(uint customerId, double discount)
        {
            this.CustomerId = customerId;
            this.Discount = discount;
        }
    }

    class Product
    {
        public uint ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(uint productId, string name, double price)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Price = price;
        }
    }

    class Order
    {
        public uint OrderId { get; set; }
        public uint CustomerId { get; set; }
        public DateTime Date { get; set; }

        public Order(uint orderId, uint customerId, DateTime date)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.Date = date;
        }
    }

    class OrderProduct
    {
        public uint OrderId { get; set; }
        public uint ProductId { get; set; }
        public uint Quantity { get; set; }

        public OrderProduct(uint orderId, uint productId, uint quantity)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }



    class Database
    {
        public List<Customer> Customers { get; private set; }
        public List<LoyaltyCard> LoyaltyCards { get; private set; }
        public List<Product> Products { get; private set; }
        public List<Order> Orders { get; private set; }
        public List<OrderProduct> OrderProducts { get; private set; }


        public static Database GetInstance()
        {
            return new Database()
            {
                Customers = new List<Customer>()
                {
                    new Customer(1, "Jan", "Kowalski", "03112323433"),
                    new Customer(2, "Andrzej", "Nowak", "92060363493"),
                    new Customer(3, "Artur", "Wiśniewski", "04211263493"),
                    new Customer(4, "Alina", "Nowak", "96123012364"),
                    new Customer(5, "Magdalena", "Bąk", "00322123443"),
                    new Customer(6, "Justyna", "Czerwińska", "99010323443"),
                    new Customer(7, "Marek", "Bąk", "02260323433"),
                    new Customer(8, "Jerzy", "Marciniak", "85013023433"),
                    new Customer(9, "Aneta", "Kowalska", "96100923443"),
                    new Customer(10, "Michał", "Antoniewski", "88010123353")
                },

                LoyaltyCards = new List<LoyaltyCard>()
                {
                    new LoyaltyCard(1, 0.1),
                    new LoyaltyCard(2, 0.25),
                    new LoyaltyCard(4, 0.2),
                    new LoyaltyCard(6, 0.05),
                    new LoyaltyCard(8, 0.15),
                    new LoyaltyCard(9, 0.15),
                    new LoyaltyCard(10, 0.1),
                },

                Products = new List<Product>()
                {
                    new Product(1, "Telefon", 1000),
                    new Product(2, "Telewizor", 2000),
                    new Product(3, "Monitor", 1000),
                    new Product(4, "Myszka", 200),
                    new Product(5, "Suszarka do włosów", 100),
                    new Product(6, "Żelazko", 150),
                    new Product(7, "Krzesło gaminigowe", 800),
                    new Product(8, "Klawiatura", 300),
                    new Product(9, "Długopis", 2),
                    new Product(10, "Etui do telefonu", 50),
                    new Product(11, "Laptop", 2000),
                    new Product(12, "Baterie AA", 4),
                    new Product(13, "Tusz do drukarki", 40),
                    new Product(14, "Marker", 5),
                    new Product(15, "Płyta CD", 1),
                    new Product(16, "Kabel HDMI", 30),
                    new Product(17, "Pralka", 1000),
                    new Product(18, "Lodówka", 2000),
                    new Product(19, "Taśma klejąca", 5),
                    new Product(20, "Nożyczki", 5),
                    new Product(21, "Toster", 100),
                    new Product(22, "Czajnik elektryczny", 100)
                },

                Orders = new List<Order>()
                {
                    new Order(1, 1, new DateTime(2019, 12, 12)),
                    new Order(2, 2, new DateTime(2017, 1, 2)),
                    new Order(3, 2, new DateTime(2019, 12, 12)),
                    new Order(4, 3, new DateTime(2019, 6, 30)),
                    new Order(5, 3, new DateTime(2019, 7, 7)),
                    new Order(6, 3, new DateTime(2019, 7, 14)),
                    new Order(7, 4, new DateTime(2019, 12, 23)),
                    new Order(8, 4, new DateTime(2018, 10, 21)),
                    new Order(9, 5, new DateTime(2019, 10, 1)),
                    new Order(10, 5, new DateTime(2019, 10, 2)),
                    new Order(11, 6, new DateTime(2019, 11, 12)),
                    new Order(12, 7, new DateTime(2019, 2, 28)),
                    new Order(13, 8, new DateTime(2019, 5, 15)),
                    new Order(14, 9, new DateTime(2018, 3, 12)),
                    new Order(15, 10, new DateTime(2017, 3, 22)),
                    new Order(16, 10, new DateTime(2017, 3, 23)),
                    new Order(17, 10, new DateTime(2017, 3, 24)),
                    new Order(18, 10, new DateTime(2017, 3, 25)),
                    new Order(19, 2, new DateTime(2017, 8, 12)),
                },

                OrderProducts = new List<OrderProduct>()
                {
                    new OrderProduct(1, 2, 2),
                    new OrderProduct(1, 16, 2),
                    new OrderProduct(2, 21, 1),
                    new OrderProduct(2, 22, 1),
                    new OrderProduct(3, 1, 3),
                    new OrderProduct(3, 10, 3),
                    new OrderProduct(4, 15, 10),
                    new OrderProduct(4, 14, 1),
                    new OrderProduct(5, 20, 1),
                    new OrderProduct(5, 19, 1),
                    new OrderProduct(6, 11, 1),
                    new OrderProduct(7, 5, 1),
                    new OrderProduct(7, 6, 1),
                    new OrderProduct(7, 17, 1),
                    new OrderProduct(8, 4, 1),
                    new OrderProduct(8, 8, 1),
                    new OrderProduct(9, 3, 2),
                    new OrderProduct(9, 7, 1),
                    new OrderProduct(10, 9, 10),
                    new OrderProduct(10, 12, 3),
                    new OrderProduct(10, 13, 2),
                    new OrderProduct(11, 3, 2),
                    new OrderProduct(11, 4, 2),
                    new OrderProduct(11, 8, 1),
                    new OrderProduct(11, 16, 1),
                    new OrderProduct(12, 18, 1),
                    new OrderProduct(12, 17, 1),
                    new OrderProduct(13, 7, 20),
                    new OrderProduct(14, 5, 1),
                    new OrderProduct(14, 6, 1),
                    new OrderProduct(15, 22, 1),
                    new OrderProduct(16, 13, 1),
                    new OrderProduct(17, 16, 1),
                    new OrderProduct(18, 3, 1),
                    new OrderProduct(19, 14, 10),
                }
            };
        }
    }
}
