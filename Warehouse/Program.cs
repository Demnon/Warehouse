using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта склад
            Warehouse w_WareHouse = new Warehouse();
            while (true)
            {
                try
                {
                    Console.WriteLine("Склад\nМеню\n" +
                        "1 Добавить паллеты в склад\n" +
                        "2 Удалить паллеты из склада\n" +
                        "3 Добавить коробку в заданную паллету\n" +
                        "4 Группировка\n" +
                        "5 Выход");
                    int i = int.Parse(Console.ReadLine());
                    switch (i)
                    { 
                        case 1:
                            Console.WriteLine("Добавление паллет");
                            Console.WriteLine("Введите количество добавляемых паллет:");
                            int i_Count = 0;
                            if (!int.TryParse(Console.ReadLine(),out i_Count) || i_Count<0)
                            {
                                throw new ApplicationException("Введите корректное число добавляемых паллет.");
                            }
                            Console.WriteLine("Введите имя паллеты:");
                            string s_Name = Console.ReadLine();
                            Console.WriteLine("Введите ширину паллеты:");
                            double d_Width = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите высоту паллеты:");
                            double d_Height = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите глубину паллеты:");
                            double d_Depth = Convert.ToDouble(Console.ReadLine());

                            w_WareHouse.AddPallet(i_Count, s_Name, d_Width, d_Height, d_Depth);

                            // Вывод содержимого
                            w_WareHouse.Output();
                            break; 
                        case 2:
                            Console.WriteLine("Удаление паллеты");
                            Console.WriteLine("Введите id удаляемой паллеты:");
                            Guid g_Id = new Guid(Console.ReadLine());

                            w_WareHouse.DeletePallet(g_Id);

                            // Вывод содержимого
                            w_WareHouse.Output();
                            break;
                        case 3:
                            Console.WriteLine("Добавление коробки в заданную паллету");
                            Console.WriteLine("Введите id паллеты:");
                            g_Id = new Guid(Console.ReadLine());
                            Console.WriteLine("Введите количество долбавляемых коробок:");
                            i_Count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите имя коробки:");
                            s_Name = Console.ReadLine();
                            Console.WriteLine("Введите ширину коробки:");
                            d_Width = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите высоту коробки:");
                            d_Height = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите глубину коробки:");
                            d_Depth = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите вес коробки:");
                            double d_Weight = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите срок годности либо дату производства в формате дд.мм.гггг коробки:");
                            string s_Help = Console.ReadLine();
                            if (s_Help.Contains("."))
                            {
                                DateTime d_Date = new DateTime(Convert.ToInt32(s_Help.Split('.')[2]), Convert.ToInt32(s_Help.Split('.')[1]), Convert.ToInt32(s_Help.Split('.')[0]));
                                w_WareHouse.AddBoxInPallet(i_Count, g_Id, s_Name, d_Width, d_Height, d_Depth, d_Weight, d_Date);
                            }
                            else
                            {
                                w_WareHouse.AddBoxInPallet(i_Count, g_Id, s_Name, d_Width, d_Height, d_Depth, d_Weight, Convert.ToInt32(s_Help));
                            }

                            // Вывод содержимого
                            w_WareHouse.Output();
                            break;
                        case 4:
                            Console.WriteLine("Группировка");
                            w_WareHouse.GroupPallets();
                            break;

                        case 5:
                            Console.WriteLine("выход");
                            return;

                        default:
                            Console.WriteLine("Введите корректный пункт меню");
                            break;
                    }
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите корректный пункт меню.");
                }
            }
        }
    }
}
