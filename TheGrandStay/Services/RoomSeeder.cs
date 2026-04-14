using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TheGrandStay.Data;
using TheGrandStay.Models;

namespace TheGrandStay.Services
{
    public static class RoomSeeder
    {
        public static void SeedRooms(MongoDbContext db)
        {
            // Check if database is already populated
            if (db.Rooms.CountDocuments(_ => true) == 0)
            {
                var rooms = new List<Room>();

                // --- SEEDING 20 SUITES ---
                for (int i = 1; i <= 20; i++)
                {
                    var s = new Suite
                    {
                        RoomNo = $"S-{i:00}",
                        BasePrice = 10000,
                        IsOccupied = false,
                        HasKitchen = true,
                        BookingDays = 1 // Default
                    };

                    // Variety: 10 Luxury, 10 Basic
                    s.Category = (i <= 10) ? "Luxury" : "Basic";
                    // Variety: Mix of 2 and 3 rooms
                    s.NumberOfRooms = (i % 2 == 0) ? 2 : 3;

                    rooms.Add(s);
                }

                // --- SEEDING 30 ROOMS ---
                for (int i = 1; i <= 30; i++)
                {
                    var r = new SingleRoom
                    {
                        RoomNo = $"R-{i:00}",
                        BasePrice = 5000,
                        IsOccupied = false,
                        BookingDays = 1 // Default
                    };

                    // Variety: 15 Luxury, 15 Basic
                    if (i <= 15)
                    {
                        r.Category = "Luxury";
                        r.BedType = (i % 2 == 0) ? "Double Bed" : "Single Bed";
                    }
                    else
                    {
                        r.Category = "Basic";
                        r.BedType = (i % 3 == 0) ? "Double Bed" : "Single Bed";
                    }

                    rooms.Add(r);
                }

                // Insert all into MongoDB
                db.Rooms.InsertMany(rooms);
                Console.WriteLine(">>> DATABASE SEEDED: 50 Units (Suites & Rooms) created with full variety.");
            }
        }
    }
}