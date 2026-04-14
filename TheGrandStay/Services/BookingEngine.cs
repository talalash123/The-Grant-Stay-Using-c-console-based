using System;
using System.Collections.Generic;
using System.Linq;
using TheGrandStay.Models;
using TheGrandStay.Data;

namespace TheGrandStay.Services
{
    public class BookingEngine
    {
        private readonly HotelRepository _repo;

        public BookingEngine(HotelRepository repo)
        {
            _repo = repo;
        }

        public List<Room> StartBookingProcess()
        {
            List<Room> selectedRooms = new List<Room>();
            // Sirf wahi rooms jo database mein 'IsOccupied: false' hain
            var allAvailable = _repo.GetAvailableRooms().Where(r => !r.IsOccupied).ToList();

            if (allAvailable.Count == 0)
            {
                Console.WriteLine("\n[!] SORRY, NO ROOMS ARE CURRENTLY AVAILABLE.");
                return selectedRooms;
            }

            Console.WriteLine("\n--- SELECT ACCOMMODATION TYPE ---");
            Console.WriteLine($"[1] Suites Only (Available: {allAvailable.Count(r => r is Suite)})");
            Console.WriteLine($"[2] Rooms Only  (Available: {allAvailable.Count(r => r is SingleRoom)})");
            Console.WriteLine($"[3] Both (Book Suites & Rooms Together)");
            Console.WriteLine("[4] Cancel & Go Back");
            Console.Write("\nSelection > ");
            string typeChoice = Console.ReadLine();

            if (typeChoice == "4") return selectedRooms;

            Console.Write("Enter number of days for stay: ");
            if (!int.TryParse(Console.ReadLine(), out int days)) days = 1;

            // --- SUITE SELECTION LOGIC ---
            if (typeChoice == "1" || typeChoice == "3")
            {
                var suites = allAvailable.OfType<Suite>().ToList();
                if (suites.Count > 0)
                {
                    Console.WriteLine("\n--- AVAILABLE SUITE NUMBERS ---");
                    Console.WriteLine(string.Join(", ", suites.Select(s => s.RoomNo)));

                    Console.WriteLine("\nSelect Category: [1] Luxury (+4,000) | [2] Basic (+0)");
                    string cat = Console.ReadLine() == "1" ? "Luxury" : "Basic";

                    var filteredByCat = suites.Where(s => s.Category == cat).ToList();
                    Console.WriteLine($"Available {cat} Nos: " + string.Join(", ", filteredByCat.Select(s => s.RoomNo)));

                    Console.WriteLine("Select Size: [1] 2-Room | [2] 3-Room (+2,500)");
                    int rCount = Console.ReadLine() == "1" ? 2 : 3;

                    var finalSuites = filteredByCat.Where(s => s.NumberOfRooms == rCount).ToList();
                    if (finalSuites.Count > 0)
                    {
                        Console.Write($"How many {cat} {rCount}-Room Suites? (Max {finalSuites.Count}): ");
                        int qty = int.Parse(Console.ReadLine());
                        for (int i = 0; i < qty && i < finalSuites.Count; i++)
                        {
                            var s = new Suite { RoomNo = finalSuites[i].RoomNo, Category = cat, NumberOfRooms = rCount, BasePrice = 10000, BookingDays = days };
                            ConfigureMeals(s);
                            selectedRooms.Add(s);
                        }
                    }
                    else { Console.WriteLine("No suites available for this specific choice."); }
                }
            }

            // --- ROOM SELECTION LOGIC ---
            if (typeChoice == "2" || typeChoice == "3")
            {
                var rooms = allAvailable.OfType<SingleRoom>().ToList();
                if (rooms.Count > 0)
                {
                    Console.WriteLine("\n--- AVAILABLE ROOM NUMBERS ---");
                    Console.WriteLine(string.Join(", ", rooms.Select(r => r.RoomNo)));

                    Console.WriteLine("\nSelect Category: [1] Luxury (+1,500) | [2] Basic (+0)");
                    string cat = Console.ReadLine() == "1" ? "Luxury" : "Basic";

                    var filteredByCat = rooms.Where(r => r.Category == cat).ToList();
                    Console.WriteLine($"Available {cat} Nos: " + string.Join(", ", filteredByCat.Select(r => r.RoomNo)));

                    Console.WriteLine("Select Bed: [1] Single Bed | [2] Double Bed (+1,000)");
                    string bed = Console.ReadLine() == "1" ? "Single Bed" : "Double Bed";

                    var finalRooms = filteredByCat.Where(r => r.BedType == bed).ToList();
                    if (finalRooms.Count > 0)
                    {
                        Console.Write($"How many {cat} {bed} Rooms? (Max {finalRooms.Count}): ");
                        int qty = int.Parse(Console.ReadLine());
                        for (int i = 0; i < qty && i < finalRooms.Count; i++)
                        {
                            var r = new SingleRoom { RoomNo = finalRooms[i].RoomNo, Category = cat, BedType = bed, BasePrice = 5000, BookingDays = days };
                            ConfigureMeals(r);
                            selectedRooms.Add(r);
                        }
                    }
                    else { Console.WriteLine("No rooms available for this specific choice."); }
                }
            }

            return selectedRooms;
        }

        private void ConfigureMeals(Room room)
        {
            Console.WriteLine($"\nMeals for {room.RoomNo} (1:Yes / 2:No):");
            Console.Write("- Breakfast: "); room.HasBreakfast = Console.ReadLine() == "1";
            Console.Write("- Lunch: "); room.HasLunch = Console.ReadLine() == "1";
            Console.Write("- Dinner: "); room.HasDinner = Console.ReadLine() == "1";
        }
    }
}