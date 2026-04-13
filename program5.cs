using System;
using System.Collections.Generic;
using System.Linq;

namespace SydneyCoffee
{
    // CHANGE 1: Encapsulation using a Customer Class
    public class Customer
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string IsReseller { get; set; }
        public double TotalCharge { get; set; }
    }

    class Program
    {
        // CHANGE 4: Using Constants instead of "Magic Numbers"
        const double RESELLER_DISCOUNT_RATE = 0.20;
        const int MAX_BAGS = 200;
        const int MIN_BAGS = 1;

        static void Main(string[] args)
        {
            // CHANGE 2: Using a dynamic List instead of fixed-size arrays
            List<Customer> customers = new List<Customer>();
            int n = 2;

            Console.WriteLine("\t\t\t\tWelcome to use Sydney Coffee Program\n");

            for (int i = 0; i < n; i++)
            {
                Customer cust = new Customer();

                Console.Write("Enter customer name: ");
                cust.Name = Console.ReadLine();

                // Input Validation Loop
                do
                {
                    Console.Write($"Please Enter the number of coffee beans bags ({MIN_BAGS}-{MAX_BAGS}kg): ");
                    if (int.TryParse(Console.ReadLine(), out int qty))
                    {
                        cust.Quantity = qty;
                    }

                    if (cust.Quantity < MIN_BAGS || cust.Quantity > MAX_BAGS)
                    {
                        Console.WriteLine($"Invalid Input!\nCoffee bags between {MIN_BAGS} and {MAX_BAGS} can be ordered.");
                    }
                } while (cust.Quantity < MIN_BAGS || cust.Quantity > MAX_BAGS);

                Console.Write("Enter yes/no to indicate whether you are a reseller: ");
                cust.IsReseller = Console.ReadLine().ToLower();

                // CHANGE 3: Logic Extraction into a Modular Function
                cust.TotalCharge = CalculateCharge(cust.Quantity, cust.IsReseller);

                customers.Add(cust);

                Console.WriteLine($"The total sales value from {cust.Name} is ${cust.TotalCharge}");
                Console.WriteLine("-----------------------------------------------------------------------------");
            }

            DisplaySummary(customers);
        }

        // CHANGE 3: Modular Function for calculations
        static double CalculateCharge(int qty, string resellerStatus)
        {
            double pricePerBag;

            if (qty <= 5) pricePerBag = 36;
            else if (qty <= 15) pricePerBag = 34.5;
            else pricePerBag = 32.7;

            double subtotal = pricePerBag * qty;

            if (resellerStatus == "yes")
            {
                return subtotal * (1 - RESELLER_DISCOUNT_RATE);
            }
            return subtotal;
        }

        static void DisplaySummary(List<Customer> customers)
        {
            Console.WriteLine("\t\t\t\t\tSummary of sales\n");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine(String.Format("{0,15}{1,10}{2,12}{3,12}", "Name", "Quantity", "Reseller", "Charge"));

            foreach (var cust in customers)
            {
                Console.WriteLine(String.Format("{0,15}{1,10}{2,12}{3,12}",
                    cust.Name, cust.Quantity, cust.IsReseller, cust.TotalCharge.ToString("F2")));
            }

            // Using LINQ for HD-level data analysis
            var topCustomer = customers.OrderByDescending(c => c.TotalCharge).First();
            var bottomCustomer = customers.OrderBy(c => c.TotalCharge).First();

            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"The customer spending most is {topCustomer.Name} ${topCustomer.TotalCharge:F2}");
            Console.WriteLine($"The customer spending least is {bottomCustomer.Name} ${bottomCustomer.TotalCharge:F2}");
        }
    }
}