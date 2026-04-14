using System;
using System.Collections.Generic;
using System.Linq;
using TheGrandStay.Data;
using TheGrandStay.Models;
using TheGrandStay.Services;

namespace TheGrandStay
{
    class Program
    {
        static void CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int spaces = (screenWidth / 2) - (text.Length / 2);
            Console.WriteLine(new string(' ', Math.Max(0, spaces)) + text);
        }

        static void DrawHeaderLine() => Console.WriteLine(new string('═', Console.WindowWidth - 1));
        static void DrawSubLine() => Console.WriteLine(new string('─', Console.WindowWidth - 1));

        static void Main(string[] args)
        {
            var dbContext = new MongoDbContext();
            var repository = new HotelRepository(dbContext);
            RoomSeeder.SeedRooms(dbContext);

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                // --- BIG BOLD LOGO: THE GRAND STAY ---
                Console.WriteLine("\n");
                DrawHeaderLine();
                CenterText("████████╗██╗  ██╗███████╗    ██████╗ ██████╗  █████╗ ███╗   ██╗██████╗ ");
                CenterText("╚══██╔══╝██║  ██║██╔════╝   ██╔════╝ ██╔══██╗██╔══██╗████╗  ██║██╔══██╗");
                CenterText("   ██║   ███████║█████╗     ██║  ███╗██████╔╝███████║██╔██╗ ██║██║  ██║");
                CenterText("   ██║   ██╔══██║██╔══╝     ██║   ██║██╔══██╗██╔══██║██║╚██╗██║██║  ██║");
                CenterText("   ██║   ██║  ██║███████╗    ╚██████╔╝██║  ██║██║  ██║██║ ╚████║██████╔╝");
                CenterText("   ╚═╝   ╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ");
                Console.WriteLine();
                CenterText("                    ███████╗████████╗ █████╗ ██╗   ██╗                  ");
                CenterText("                    ██╔════╝╚══██╔══╝██╔══██╗╚██╗ ██╔╝                  ");
                CenterText("                    ███████╗   ██║   ███████║ ╚████╔╝                   ");
                CenterText("                    ╚════██║   ██║   ██╔══██║  ╚██╔╝                    ");
                CenterText("                    ███████║   ██║   ██║  ██║   ██║                     ");
                CenterText("                    ╚══════╝   ╚═╝   ╚═╝  ╚═╝   ╚═╝                     ");
                DrawHeaderLine();
                Console.ResetColor();

                Console.WriteLine("\n\n");
                CenterText(" [1]  NEW GUEST CHECK-IN   ");
                CenterText(" [2]  GUEST CHECKOUT       ");
                CenterText(" [3]  EXIT SYSTEM          ");
                Console.WriteLine("\n");
                DrawHeaderLine();
                Console.Write("\n Selection > ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    BookingEngine engine = new BookingEngine(repository);
                    List<Room> myBookings = engine.StartBookingProcess();

                    if (myBookings.Count > 0)
                    {
                        // --- DETAILED POS BILL ---
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        DrawHeaderLine();
                        CenterText("T H E   G R A N D   S T A Y   -   I N V O I C E");
                        CenterText(DateTime.Now.ToString("dddd, dd MMMM yyyy | HH:mm"));
                        DrawHeaderLine();

                        double grandTotal = 0;
                        int stayDays = myBookings[0].BookingDays;

                        foreach (var item in myBookings)
                        {
                            string type = item is Suite ? "Suite" : "Room";
                            double baseRate = item.BasePrice;
                            double luxuryFee = (item.Category == "Luxury") ? (item is Suite ? 4000 : 1500) : 0;
                            double mealDaily = (item.HasBreakfast ? 500 : 0) + (item.HasLunch ? 800 : 0) + (item.HasDinner ? 1000 : 0);
                            double dailyTotal = baseRate + luxuryFee + mealDaily;
                            double totalForStay = dailyTotal * stayDays;

                            Console.WriteLine($"\n  UNIT ID: {item.RoomNo} | TYPE: {type} | CAT: {item.Category}");
                            DrawSubLine();
                            Console.WriteLine($"  > Base Rate:               {baseRate,12:N0} PKR");
                            if (luxuryFee > 0) Console.WriteLine($"  > Luxury Surcharge:        {luxuryFee,12:N0} PKR");
                            if (mealDaily > 0) Console.WriteLine($"  > Daily Meal Plan:         {mealDaily,12:N0} PKR");
                            Console.WriteLine($"  > Total Per Day:           {dailyTotal,12:N0} PKR");
                            Console.WriteLine($"  > TOTAL ({stayDays} DAYS):        {totalForStay,12:N0} PKR");

                            grandTotal += totalForStay;
                        }

                        Console.WriteLine("\n");
                        DrawHeaderLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterText($"GRAND TOTAL PAYABLE: {grandTotal:N0} PKR");
                        Console.ResetColor();
                        DrawHeaderLine();

                        Console.Write("\nConfirm Invoice & Proceed to Registration? (1: Yes / 2: No): ");
                        if (Console.ReadLine() == "1")
                        {
                            Console.Clear();
                            DrawHeaderLine();
                            CenterText("GUEST REGISTRATION");
                            DrawHeaderLine();

                            Customer cust = new Customer();
                            Console.WriteLine("\n  Please enter guest details:");
                            Console.Write("  Name:  "); cust.Name = Console.ReadLine();
                            Console.Write("  Email: "); cust.Email = Console.ReadLine();

                            repository.SaveCustomer(cust);

                            foreach (var b in myBookings)
                            {
                                repository.UpdateRoomStatus(b.RoomNo, true);
                            }

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            CenterText("RESERVATION SUCCESSFUL!");
                            CenterText($"{cust.Name}, welcome to The Grand Stay.");
                            CenterText($"Room(s): " + string.Join(", ", myBookings.Select(b => b.RoomNo)));
                            Console.ResetColor();
                            DrawHeaderLine();
                        }
                    }
                }
                else if (mainChoice == "2")
                {
                    Console.Clear();
                    DrawHeaderLine();
                    CenterText("SECURE CHECKOUT");
                    DrawHeaderLine();

                    Console.Write("\n  Guest Name:  "); string n = Console.ReadLine();
                    Console.Write("  Room Number: "); string r = Console.ReadLine().ToUpper();

                    Console.Write($"\n  Confirm checkout for {n}? (1: Yes / 2: No): ");
                    if (Console.ReadLine() == "1")
                    {
                        repository.UpdateRoomStatus(r, false);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n  Room {r} is now available.");
                        Console.ResetColor();
                    }
                }
                else if (mainChoice == "3") break;

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}