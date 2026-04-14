using System.Collections.Generic;
using MongoDB.Driver;
using TheGrandStay.Models;

namespace TheGrandStay.Data
{
    public class HotelRepository
    {
        private readonly MongoDbContext _context;

        public HotelRepository(MongoDbContext context)
        {
            _context = context;
        }

        public void SaveCustomer(Customer customer)
        {
            _context.Customers.InsertOne(customer);
        }

        public List<Room> GetAvailableRooms()
        {
            // Fetch all rooms where IsOccupied is false
            return _context.Rooms.Find(r => r.IsOccupied == false).ToList();
        }

        public void UpdateRoomStatus(string roomNo, bool status)
        {
            var filter = Builders<Room>.Filter.Eq(r => r.RoomNo, roomNo);
            var update = Builders<Room>.Update.Set(r => r.IsOccupied, status);
            _context.Rooms.UpdateOne(filter, update);
        }
    }
}