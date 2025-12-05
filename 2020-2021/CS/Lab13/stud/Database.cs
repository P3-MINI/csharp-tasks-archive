using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab13
{
    public class Database
    {
        private readonly string _baseDir;

        public Database(string baseDir)
        {
            _baseDir = baseDir;

            if (!Directory.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }
        }

        /// <summary>
        /// Adds new object to the database.
        /// If the object with the given Id and the same type exists, the exception should be thrown.
        /// If Id == 0 the value of Id should be replaced by te max Id value for TEntity object type + 1
        /// </summary>
        
        // TODO: Implement Add method
        

        /// <summary>
        /// Get retrieves an object from the database. 
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        
        // TODO: Implement Get method

        /// <summary>
        /// Updates object in the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        
        // TODO: Implement Update method

        /// <summary>
        /// Deletes object from the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        
        // TODO: Implement Delete method
    }

    public class User : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Property computed from FirstName and LastName.
        /// It should not be serialized to the file.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"USER: Id {Id}, FullName: {FullName}";
        }
    }

    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"PRODUCT: Id: {Id}, Name {Name}";
        }
    }

    public class Order : IEntity
    {
        public Order(User user, Product product, int amount)
        {
            UserId = user.Id;
            ProductId = product.Id;
            Amount = amount;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"ORDER: Id {Id}, UserId: {UserId}, ProductId: {ProductId}, Amount: {Amount}";
        }
    }
}