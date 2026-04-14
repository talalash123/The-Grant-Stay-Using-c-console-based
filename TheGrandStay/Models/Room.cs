using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheGrandStay.Models
{
    public abstract class Room
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string RoomNo { get; set; }
        public bool IsOccupied { get; set; }
        public double BasePrice { get; set; }
        public string Category { get; set; }
        public int BookingDays { get; set; }
        public bool HasBreakfast { get; set; }
        public bool HasLunch { get; set; }
        public bool HasDinner { get; set; }

        public abstract double CalculateTotalBill();
    }
}