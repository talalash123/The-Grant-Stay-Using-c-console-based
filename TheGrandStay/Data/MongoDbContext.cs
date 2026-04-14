using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TheGrandStay.Models;

namespace TheGrandStay.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("HotelStayDB");

            // GLOBAL REGISTRATION: Ye sirf ek bar hona chahiye
            if (!BsonClassMap.IsClassMapRegistered(typeof(Room)))
            {
                BsonClassMap.RegisterClassMap<Room>(cm => {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                });
                BsonClassMap.RegisterClassMap<Suite>();
                BsonClassMap.RegisterClassMap<SingleRoom>();
            }
        }

        public IMongoCollection<Room> Rooms => _database.GetCollection<Room>("Rooms");
        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
    }
}