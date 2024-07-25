using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public class Customer
        {
            public string Name { get; set; }
            public double Type { get; set; }
            public double Kind { get; set; }
            public double Water { get; set; }
            public double PriceWithTax { get; set; }
            public double Members { get; set; }
            public double WaterLastMonth { get; set; }
            public double WaterThisMonth { get; set; }

        }
      
        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>();
            while (true)
            {
                Menu();
                string menuchoice = getchoice();
                if (menuchoice == "5") break;
                switch (menuchoice)
                {

                    case "1":
                        Customer infoCustomer = GetCustomerInfo(customers);
                        Console.WriteLine("This month's water bill is: " + infoCustomer.PriceWithTax + " VNĐ");
                        break;
                    case "2":
                        Console.Write("Enter the name of the customer you want to search for: ");
                        string searchName = Console.ReadLine();

                        // function find customer
                        Customer foundCustomer = SearchCustomer(customers, searchName);
                        if (foundCustomer != null)
                        {
                            Console.WriteLine("Customer found:");

                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine("|                        WATER BILL                         |");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"Name customer: {foundCustomer.Name}");
                            if (foundCustomer.Kind == 1)
                            {
                                Console.WriteLine("Type of customer: Household");
                                Console.WriteLine($"Number of members: {foundCustomer.Members}");
                            }
                            else if (foundCustomer.Kind == 2) Console.WriteLine("Type of customer: Administrative agency, public services");
                            else if (foundCustomer.Kind == 3) Console.WriteLine("Type of customer: Production units");
                            else Console.WriteLine("Type of customer: Business services");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"| Last month’s water meter  (m3) | {foundCustomer.WaterLastMonth,-18} |");
                            Console.WriteLine($"| This month’s water meter  (m3) | {foundCustomer.WaterThisMonth,-18} |");
                            Console.WriteLine($"| Total water consumptio    (m3) | {foundCustomer.WaterThisMonth - foundCustomer.WaterLastMonth, -18} |");
                            Console.WriteLine($"| Tax                      (VND) | {foundCustomer.PriceWithTax - (foundCustomer.PriceWithTax / 1.21)  ,-18} |");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"Total payment: {foundCustomer.PriceWithTax,-10} VNĐ");
                            Console.WriteLine("-------------------------------------------------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not found customer.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("\nInformation of all customers:");
                        goto case "4";
                    case "4":
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine("|               INFORMATION OF ALL CUSTOMER                 |");
                        Console.WriteLine("-------------------------------------------------------------");
                        customers.Sort((x, y) => string.Compare(x.Name, y.Name));
                        foreach (var customer in customers)
                        {
                            Console.WriteLine($"Name customer: {customer.Name}");
                            if (customer.Kind == 1)
                            {
                                Console.WriteLine("Type of customer: Household ");
                                Console.WriteLine($"Number of members: {customer.Members}");
                            }
                            else if (customer.Kind == 2) Console.WriteLine("Type of customer: Administrative agency, public services");
                            else if (customer.Kind == 3) Console.WriteLine("Type of customer: Production units");
                            else Console.WriteLine("Type of customer: Business services");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"| Last month’s water meter  (m3) | {customer.WaterLastMonth,-18} |");
                            Console.WriteLine($"| This month’s water meter  (m3) | {customer.WaterThisMonth,-18} |");
                            Console.WriteLine($"| Total water consumption   (m3) | {customer.WaterThisMonth - customer.WaterLastMonth,-18} |");
                            Console.WriteLine($"| Tax                      (VND) | {customer.PriceWithTax - (customer.PriceWithTax / 1.21),-18} |");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"Total payment: {customer.PriceWithTax,-10} VNĐ");
                            Console.WriteLine("-------------------------------------------------------------");
                        }
                        customers.Sort((x, y) => string.Compare(x.Name, y.Name));
                        goto default;
                    default:
                        Console.Write("Do you want to delete or change customer information (y / any key): ");
                        string remove = Console.ReadLine();
                        if (remove == "y")
                        {
                            Console.WriteLine("Press 1 to edit customer information");
                            Console.WriteLine("Press 2 to delete customer information");

                            Console.Write("Enter your choice: ");
                            string YesOrNo = Console.ReadLine();
                            while (YesOrNo != "1" && YesOrNo != "2")
                            {
                                Console.Write("Error, re-enter: ");
                                YesOrNo = Console.ReadLine();
                            }
                            Console.Write("Enter the customer location you want to rename or delete: ");
                            int location = Convert.ToInt32(Console.ReadLine());

                            location -= 1;
                            if (YesOrNo == "2")
                            {
                                customers.RemoveAt(location);
                            }
                            else if (YesOrNo == "1")
                            {
                                customers.RemoveAt(location);
                                infoCustomer = GetCustomerInfo(customers);
                                
                            }

                        }
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
       
        public static Customer SearchCustomer(List<Customer> customers, string searchName)
        {
            foreach (Customer customer in customers)
            {
                if (customer.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    return customer;
                }
            }
            return null; // Trả về null nếu không tìm thấy khách hàng
        }
        public static Customer GetCustomerInfo(List<Customer> customers)
        {
            Customer newCustomer = new Customer();
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            int type = 0;
            int members = 0;
            
            int kind = choice(type);
            if (kind == 1)
            {
                Console.Write("Enter member number: ");
                while (!int.TryParse(Console.ReadLine(), out members) || members <= 0)
                {
                    Console.Write("Error, Re-enter: ");
                }
            }
            Console.Write("Enter amount of water last month: ");
            double waterlastmonth;
            while (!double.TryParse(Console.ReadLine(), out waterlastmonth) || waterlastmonth < 0)
            {
                Console.Write("Error, Re-enter: ");
            }
            Console.Write("Enter amount of water this month: ");
            double waterthismonth;
            while (!double.TryParse(Console.ReadLine(), out waterthismonth) 
                || waterthismonth < 0 || waterthismonth < waterlastmonth)
            {
                Console.Write("Error, Re-enter: ");
            }
            double water = waterthismonth - waterlastmonth;
            double priceWithTax = CalculateWaterBill(kind, water, members);
            newCustomer.Name = name;
            newCustomer.Water = water;
            newCustomer.PriceWithTax = priceWithTax;
            newCustomer.Kind = kind;
            newCustomer.Members = members;
            newCustomer.WaterLastMonth = waterlastmonth;
            newCustomer.WaterThisMonth = waterthismonth;
            customers.Add(newCustomer);
            return new Customer
            {
                Name = name,
                Kind = kind,
                Water = water,
                PriceWithTax = priceWithTax,
                Members = members,
                WaterLastMonth = waterlastmonth,
                WaterThisMonth = waterthismonth,
            };
        }
        static void Menu()
        {
            Console.WriteLine("======= MENU =======");
            Console.WriteLine("1. Calculate water bill");
            Console.WriteLine("2. search for customers ");
            Console.WriteLine("3. Print out the customer list");
            Console.WriteLine("4. Edit or delete customer information");
            Console.WriteLine("5. Exit");
        }
        static string getchoice()
        {
            Console.Write("Enter your choice: ");
            string menuchoice = Console.ReadLine();
            while (menuchoice != "1" && menuchoice != "2" && menuchoice != "3" && menuchoice != "4" && menuchoice != "5")
            {
                Console.Write("Error, Re-enter: ");
                menuchoice = Console.ReadLine();
            }

            return menuchoice;
        }
        static int choice(int type)
        {
            Console.WriteLine("Type of customer");
            Console.WriteLine("1.Household customer" + "\n2.Administrative agency, public services" +
                "\n3.Production units" + "\n4.Business services" );
            Console.Write("Enter your choice: ");
            while (!int.TryParse(Console.ReadLine(), out type)  || type <=0  || type >= 5)
            {
                Console.Write("Error, Re-enter: ");
            }
            return type;
        }
        static double CalculateWaterBill(double price, double water, double members)
        {
            switch (price)
            {
                case 1:                   
                    price = CarlculateHouseHoldWaterBill(price, water, members);
                    break;

                case 2:
                    price = 9955 * water;
                    break;
                case 3:
                    price = 11615 * water;
                    break;
                case 4:
                    price = 22068 * water;
                    break;
            }
            return price = 1.21 * price;
        }
        static double CarlculateHouseHoldWaterBill(double price, double water ,  double members)
        {
            water = water / members;
            if (water > 30)
            {
                price = (5973 * 10 + 7052 * 10 + 8699 * 10 + 15929 * (water - 30));
            }
            else if (20 < water && water <= 30)
            {
                price = (5973 * 10 + 7052 * 10 + 8699 * (water - 20));
            }
            else if (10 < water && water <= 20)
            {
                price = (5973 * 10 + 7052 * (water - 10));
            }
            else
            {
                price = (5973 * water);
            }


            return price * members;
        }
    }
}
