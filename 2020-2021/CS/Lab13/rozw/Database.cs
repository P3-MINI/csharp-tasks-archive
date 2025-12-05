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
        public void Add<TEntity>(TEntity entity)
            where TEntity : IEntity
        {
            var entityTypeName = typeof(TEntity).Name;

            if (entity.Id == 0)
            {
                var files = Directory.GetFiles(_baseDir);
                if (files.Length == 0)
                {
                    entity.Id = 1;
                }
                else
                {
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        if (fileName.Contains(entityTypeName))
                        {
                            var parts = fileName.Split("_");
                            var id = int.Parse(parts[1]);

                            if (id >= entity.Id)
                            {
                                entity.Id = id + 1;
                            }
                        }
                    }
                }
            }

            var entityFileName = $"{entityTypeName}_{entity.Id}.xml";
            var entityFilePath = Path.Combine(_baseDir, entityFileName);

            if (File.Exists(entityFilePath))
                throw new Exception("File with entity already exists!");

            var xmlSerializer = new XmlSerializer(typeof(TEntity));
            using (var fs = new FileStream(entityFilePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        /// <summary>
        /// Retrieves an object from the database. 
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        public TEntity Get<TEntity>(int id)
            where TEntity : IEntity
        {
            var entityTypeName = typeof(TEntity).Name;
            var entityFileName = $"{entityTypeName}_{id}.xml";
            var entityFilePath = Path.Combine(_baseDir, entityFileName);

            if (!File.Exists(entityFilePath))
                throw new Exception("File with entity does not exists!");

            var xmlSerializer = new XmlSerializer(typeof(TEntity));
            using (var fs = new FileStream(entityFilePath, FileMode.Open))
            {
                return (TEntity)xmlSerializer.Deserialize(fs);				
            }
        }

        /// <summary>
        /// Updates object in the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        public void Update<TEntity>(TEntity entity)
            where TEntity : IEntity
        {
            var entityTypeName = typeof(TEntity).Name;
            var entityFileName = $"{entityTypeName}_{entity.Id}.xml";
            var entityFilePath = Path.Combine(_baseDir, entityFileName);

            if (!File.Exists(entityFilePath))
                throw new Exception("File with entity does not exists!");

            var xmlSerializer = new XmlSerializer(typeof(TEntity));
            using (var fs = new FileStream(entityFilePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        /// <summary>
        /// Deletes object from the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>
        public void Delete<TEntity>(int id)
            where TEntity : IEntity
        {
            var entityTypeName = typeof(TEntity).Name;
            var entityFileName = $"{entityTypeName}_{id}.xml";
            var entityFilePath = Path.Combine(_baseDir, entityFileName);

            if (!File.Exists(entityFilePath))
                throw new Exception("File with entity does not exists!");

            File.Delete(entityFilePath);
        }
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
        /// <summary>
        /// For serialization purposes
        /// </summary>
        private Order() { }

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