# The-Grant-Stay-Using-c-console-based

Markdown
# 🏨 The Grand Stay - Hotel Management System v4.0

**The Grand Stay** is a professional-grade console-based Hotel Management System built using **C# (.NET Core)** and **MongoDB**. It implements advanced Object-Oriented Programming (OOP) concepts to manage room bookings, guest registrations, and detailed billing in a seamless, automated environment.

---

## 🚀 Key Features

* **Dynamic Room Discovery:** Automatically filters and displays only available (non-occupied) rooms.
* **Dual-Category Booking:** Support for both **Suites** and **Single Rooms** with specific sub-categories (Luxury/Basic).
* **Detailed POS Billing:** Generates a professional invoice with breakdowns for Base Price, Luxury Surcharges, and Meal Plans.
* **Meal Configuration:** Optional Breakfast, Lunch, and Dinner plans per room.
* **MongoDB Integration:** Uses a NoSQL database for real-time data persistence and room status tracking.
* **Professional UI:** Features a centered console interface, ASCII art headers, and a clean page-by-page flow.

---

## 🛠️ Technical Stack

* **Language:** C# (.NET 8.0/Core)
* **Database:** MongoDB (NoSQL)
* **Architecture:** N-Tier Architecture (Models, Data, Services, UI)
* **Concepts:** Abstraction, Inheritance, Polymorphism, Encapsulation, Repository Pattern.

---

## 📂 Project Structure

```text
TheGrandStay/
├── Data/
│   ├── MongoDbContext.cs     # MongoDB Connection & Mapping
│   └── HotelRepository.cs    # Database CRUD Operations
├── Models/
│   ├── Room.cs               # Abstract Base Class
│   ├── Suite.cs              # Inherited Suite Class
│   ├── SingleRoom.cs         # Inherited Room Class
│   └── Customer.cs           # Guest Data Model
├── Services/
│   ├── BookingEngine.cs      # Core Business Logic
│   └── RoomSeeder.cs         # Initial Database Population
└── Program.cs                # Main UI & Application Flow
🧩 OOP Concepts Applied
Inheritance: Suite and SingleRoom inherit core properties from the Room base class.

Polymorphism: Method overriding is used in CalculateTotalBill() to handle different pricing logic for Suites and Rooms.

Abstraction: The Room class is marked as abstract to prevent direct instantiation of a generic room.

Encapsulation: Used private fields and public properties with access modifiers to protect data integrity.

⚙️ Setup & Installation
Prerequisites:

Install .NET SDK

Install MongoDB Community Server

Install MongoDB Compass (Optional for GUI)

Database Configuration:
Ensure MongoDB is running locally on mongodb://localhost:27017. The system will automatically create a database named HotelStayDB upon the first run.

Run the Project:

Bash
git clone [https://github.com/YourUsername/TheGrandStay.git](https://github.com/YourUsername/TheGrandStay.git)
cd TheGrandStay
dotnet run
📸 Screenshots (Concept UI)
Plaintext
    ╔══════════════════════════════════════════════╗
    ║             THE GRAND STAY LUXURY            ║
    ╚══════════════════════════════════════════════╝

     [1] NEW GUEST CHECK-IN
     [2] SECURE GUEST CHECKOUT
     [3] EXIT SYSTEM
📄 License
This project is licensed under the MIT License - see the LICENSE file for details.

Developed with ❤️ by a Student of KICSIT


---

### Tips for your GitHub:
1.  **LICENSE file:** Apni repo mein ek `LICENSE` file zaroor add karein (MIT select karlein).
2.  **.gitignore:** Aik `.gitignore` file add karein taake `bin/`, `obj/`, aur `user settings` files GitHub par upload na hon.
3.  **About Section:** GitHub repository ke right side par jo "About" section hota hai, wahan likhein: *"A Full-Stack Hotel Management System using C# and MongoDB with clean OOP design."*

Aapka README ab tayyar hai! Isko copy karke apni `README.md` file mein paste kar dei
