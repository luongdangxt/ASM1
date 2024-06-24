using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASM_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            

            while (true)
            {
                Menu();
                string menuchoice = getchoice();
                if (menuchoice == "2") { }
                else if (menuchoice == "3") { }
                else if (menuchoice == "4") { }
                else if (menuchoice == "5") { break; }
                else
                {
                    Console.Write("nhập tên: ");
                    string name = Console.ReadLine();
                    double type = 0;
                    double kind = choice(type);
                    double water = caculatewater(kind);
                    if (kind == 5) { break; }
                    double money = TYPE(kind, water);
                    Console.WriteLine("số tiền nước tháng này là: " + money + " VNĐ");
                }
                Console.WriteLine("Nhan phim bat ki de tiep tuc");
                Console.ReadLine();
                Console.Clear();

            }
            

        }
        static double caculatewater(double water)
        {
            Console.Write("nhập tiền nước tháng trước: ");
            double waterlastmonth;
            while (!double.TryParse(Console.ReadLine(), out waterlastmonth) || waterlastmonth < 0)
            {
                Console.Write("Nhap lai: ");
            }   
            Console.Write("nhập tiền nước tháng này: ");
            double waterthismonth;
            while (!double.TryParse(Console.ReadLine(), out waterthismonth) || waterthismonth < 0 || waterthismonth < waterlastmonth)
            {
                Console.Write("Nhap lai: ");
            }
             water = waterthismonth - waterlastmonth;
            return water;
        }
        static void Menu()
        {
            Console.WriteLine("======= MENU =======");
            Console.WriteLine("1. Tính tiền nước");
            Console.WriteLine("2. tìm kiếm khách hàng ");
            Console.WriteLine("3. sắp xếp khách hàng");
            Console.WriteLine("4. in ra danh sách khách hàng");
            Console.WriteLine("5. Exit");
        }
        static string getchoice()
        {
            Console.Write("Nhập lựa chọn của bạn: ");
            string menuchoice = Console.ReadLine();
            while (menuchoice != "1" && menuchoice != "2" && menuchoice != "3" && menuchoice != "4" && menuchoice != "5") 
            {
                Console.WriteLine(" nhập lại");
                menuchoice = Console.ReadLine();
            }
            
            return menuchoice;
        }
    static double choice(double type)
        {
            Console.WriteLine("bạn thuộc đối tượng nào dưới đây");
            Console.WriteLine("1.Household customer" + "\n2.Administrative agency, public services" +
                "\n3.Production units" + "\n4.Business services" + "\n5. Exit");
            Console.Write("Nhập lựa chọn của bạn: " );
            while (!double.TryParse(Console.ReadLine(), out type))
            {
                Console.Write("Nhap lai: ");
            }
            return type;
        }
    static double TYPE(double money, double water)
        {
            switch(money)
            {
                case 1:
                    money = Moneyhouse( money, water);
                    break;

                case 2:
                    money = 9955 * water ;
                    break;

                case 3:
                    money = 11615 * water ;
                    break ;
                case 4:
                    money = 22068 * water ;
                    break ;
            }    
            return money = 1.21 * money;
        }
        static double Moneyhouse (double money, double water) 
        {
            
            double members;
            Console.Write("nhập số thành viên: ");
            while (!double.TryParse(Console.ReadLine(), out members) || members <=0) 
            {
                Console.Write("Nhap lai: ");
            }
            water = water / members;
            if (water > 30)
            {
                money = (5973 * 10 + 7052 * 10 + 8699 * 10 + 15929 * (water - 30)) ;
            }
            else if (20 < water && water <= 30)
            {
                money = (5973 * 10 + 7052 * 10 + 8699 * (water - 20)) ;
            }
            else if (10 < water && water <= 20)
            {
                money = (5973 * 10 + 7052 * (water - 10)) ;
            }    
            else
            {
                money = (5973 * water) ;
            }    


            return money* members;
        }



    }
}
