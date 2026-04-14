namespace TheGrandStay.Models
{
    public class SingleRoom : Room
    {
        public string BedType { get; set; } // Single Bed / Double Bed

        public override double CalculateTotalBill()
        {
            double total = BasePrice;

            // Extra Charges Logic
            if (Category == "Luxury") total += 1500;
            if (BedType == "Double Bed") total += 1000;

            // Meal Charges (Per Day)
            if (HasBreakfast) total += 500;
            if (HasLunch) total += 800;
            if (HasDinner) total += 1000;

            // Final calculation based on days
            return total * BookingDays;
        }
    }
}