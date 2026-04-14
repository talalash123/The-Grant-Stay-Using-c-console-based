namespace TheGrandStay.Models
{
    public class Suite : Room
    {
        public int NumberOfRooms { get; set; } // 2 or 3
        public bool HasKitchen { get; set; } = true;

        public override double CalculateTotalBill()
        {
            double total = BasePrice;

            // Extra Charges Logic
            if (Category == "Luxury") total += 4000;
            if (NumberOfRooms == 3) total += 2500;

            // Meal Charges (Per Day)
            if (HasBreakfast) total += 500;
            if (HasLunch) total += 800;
            if (HasDinner) total += 1000;

            // Final calculation based on days
            return total * BookingDays;
        }
    }
}