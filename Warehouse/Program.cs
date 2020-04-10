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
            Console.WriteLine("Введите имя склада:");
            Warehouse w_Warehouse = new Warehouse(Console.ReadLine());
            Console.WriteLine("Склад "+w_Warehouse.GetSetName);

            // Главное меню с вызовами соответствующих функций
            while (true)
            {
                try
                {
                    Console.WriteLine("\nСклад "+w_Warehouse.GetSetName+"\nМеню\n" +
                        "1. Добавить палеты в склад\n" +
                        "2. Удалить палеты из склада\n" +
                        "3. Добавить коробки в заданную палету\n" +
                        "4. Удалить коробки из заданной палеты\n" +
                        "5. Содержимое склада\n" +
                        "6. Содержимое заданной палеты\n" +
                        "7. Группировка палет\n" +
                        "8. Вывести 3 палеты\n" +
                        "9. Добавить данные в склад из файла\n" +
                        "10. Записать данные из склада в файл\n" +
                        "11. Выход\n");
                    int i_MenuItem = 0;
                    if (!int.TryParse(Console.ReadLine(),out i_MenuItem))
                    {
                        throw new ApplicationException("Введите корректный пункт меню.");
                    }
                    switch (i_MenuItem)
                    { 
                        case 1:
                            Console.WriteLine("1. Добавить палеты в склад");
                            Console.WriteLine("Введите количество добавляемых палет:");
                            int i_Count = 0;
                            if (!int.TryParse(Console.ReadLine(),out i_Count) || i_Count<=0)
                            {
                                throw new ApplicationException("Введите корректное число добавляемых палет.");
                            }

                            // Массив значений 
                            object[,] o_Information = new object[i_Count,4];
                            // Цикл ввода значений
                            for (int i = 0; i < i_Count; i++)
                            {
                                Console.WriteLine("Введите имя " + (i+1).ToString() +" палеты:");
    
                                string s_Name = Console.ReadLine();
                                Console.WriteLine("Введите ширину " + (i + 1).ToString() + " палеты:");
                                double d_Width = Convert.ToDouble(Console.ReadLine());
                                if (d_Width <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите высоту " + (i + 1).ToString() + " палеты:");
                                double d_Height = Convert.ToDouble(Console.ReadLine());
                                if (d_Height <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите глубину " + (i + 1).ToString() + " палеты:");
                                double d_Depth = Convert.ToDouble(Console.ReadLine());
                                if (d_Depth <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                o_Information[i, 0] = s_Name;
                                o_Information[i, 1] = d_Width;
                                o_Information[i, 2] = d_Height;
                                o_Information[i, 3] = d_Depth;
                            }

                            // Добавление
                            w_Warehouse.AddPallets(o_Information);
                            Console.WriteLine("Добавление завершено");
                            break; 
                        case 2:
                            Console.WriteLine("2. Удалить палеты из склада");
                            Console.WriteLine("Введите количество удаляемых палет:");
                            i_Count = 0;
                            if (!int.TryParse(Console.ReadLine(), out i_Count) || i_Count <= 0)
                            {
                                throw new ApplicationException("Введите корректное число удаляемых палет.");
                            }
                            // Массив id 
                            Guid[] g_Ids = new Guid[i_Count];
                            // Цикл ввода id
                            for (int i = 0; i < i_Count; i++)
                            {
                                Console.WriteLine("Введите id удаляемой " + (i + 1).ToString() + " палеты:");
                                g_Ids[i] = new Guid(Console.ReadLine());
                            }

                            // Удаление
                            w_Warehouse.DeletePallets(g_Ids);
                            Console.WriteLine("Удаление завершено");
                            break;
                        case 3:
                            Console.WriteLine("3. Добавить коробки в заданную палету");
                            Console.WriteLine("Введите id палеты:");
                            Guid g_Id = new Guid(Console.ReadLine());

                            Console.WriteLine("Введите количество добавляемых коробок:");
                            i_Count = 0;
                            if (!int.TryParse(Console.ReadLine(), out i_Count) || i_Count <= 0)
                            {
                                throw new ApplicationException("Введите корректное число добавляемых коробок.");
                            }

                            // Массив значений 
                            o_Information = new object[i_Count, 6];
                            // Цикл ввода значений
                            for (int i = 0; i < i_Count; i++)
                            {
                                Console.WriteLine("Введите имя " + (i + 1).ToString() + " коробки:");
                                string s_Name = Console.ReadLine();
                                Console.WriteLine("Введите ширину " + (i + 1).ToString() + " коробки:");
                                double d_Width = Convert.ToDouble(Console.ReadLine());
                                if (d_Width <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите высоту " + (i + 1).ToString() + " коробки:");
                                double d_Height = Convert.ToDouble(Console.ReadLine());
                                if (d_Height <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите глубину " + (i + 1).ToString() + " коробки:");
                                double d_Depth = Convert.ToDouble(Console.ReadLine());
                                if (d_Depth <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите вес " + (i + 1).ToString() + " коробки:");
                                double d_Weight = Convert.ToDouble(Console.ReadLine());
                                if (d_Weight <= 0)
                                {
                                    throw new ApplicationException("Введите корректное значение.");
                                }
                                Console.WriteLine("Введите срок годности либо дату производства в формате дд.мм.гггг " + (i + 1).ToString() + " коробки:");
                                object o_Help = Console.ReadLine();
                                string s_Help = o_Help.ToString();
                                if (s_Help.Contains("."))
                                {
                                    o_Help = new DateTime(Convert.ToInt32(s_Help.Split('.')[2]), Convert.ToInt32(s_Help.Split('.')[1]), Convert.ToInt32(s_Help.Split('.')[0]));
                                }
                                else
                                {
                                    int i_ShelfLife = Convert.ToInt32(s_Help);
                                    if (i_ShelfLife <= 0)
                                    {
                                        throw new ApplicationException("Введите корректное значение.");
                                    }
                                    o_Help = i_ShelfLife;
                                }

                                o_Information[i, 0] = s_Name;
                                o_Information[i, 1] = d_Width;
                                o_Information[i, 2] = d_Height;
                                o_Information[i, 3] = d_Depth;
                                o_Information[i, 4] = d_Weight;
                                o_Information[i, 5] = o_Help;
                            }

                            // Добавление
                            w_Warehouse.AddBoxesInPallet(g_Id,o_Information);
                            Console.WriteLine("Добавление завершено");
                            break;
                        case 4:
                            Console.WriteLine("4. Удалить коробки из заданной палеты");
                            Console.WriteLine("Введите id палеты:");
                            g_Id = new Guid(Console.ReadLine());

                            Console.WriteLine("Введите количество удаляемых коробок:");
                            i_Count = 0;
                            if (!int.TryParse(Console.ReadLine(), out i_Count) || i_Count <= 0)
                            {
                                throw new ApplicationException("Введите корректное число удаляемых коробок.");
                            }
                            // Массив id 
                            g_Ids = new Guid[i_Count];
                            // Цикл ввода id
                            for (int i = 0; i < i_Count; i++)
                            {
                                Console.WriteLine("Введите id удаляемой " + (i + 1).ToString() + " коробки:");
                                g_Ids[i] = new Guid(Console.ReadLine());
                            }

                            // Удаление
                            w_Warehouse.DeleteBoxesInPallet(g_Id,g_Ids);
                            Console.WriteLine("Удаление завершено");
                            break;

                        case 5:
                            Console.WriteLine("5. Содержимое склада");
                            OutputInfo.OutputAllPallet(w_Warehouse.GetPallets);
                            break;
                        case 6:
                            Console.WriteLine("6. Содержимое заданной палеты");
                            Console.WriteLine("Введите id палеты:");
                            g_Id = new Guid(Console.ReadLine());
                            OutputInfo.OutputPallet(g_Id, w_Warehouse.GetPallets);
                            break;
                        case 7:
                            Console.WriteLine("7. Группировка палет");
                            OutputInfo.GroupPallets(w_Warehouse.GetPallets);
                            break;
                        case 8:
                            Console.WriteLine("8. Вывести 3 палеты");
                            OutputInfo.OutputTreePallets(w_Warehouse.GetPallets);
                            break;
                        case 9:
                            Console.WriteLine("9. Добавить данные в склад из файла");
                            Console.WriteLine("Введите полный путь к файлу:");
                            string s_Path = Console.ReadLine();
                            GetSetInfoFromFile.ReadFile(s_Path, w_Warehouse);
                            Console.WriteLine("Данные добавлены в склад");
                            break;
                        case 10:
                            Console.WriteLine("10. Записать данные из склада в файл");
                            Console.WriteLine("Введите полный путь к файлу:");
                            s_Path = Console.ReadLine();
                            GetSetInfoFromFile.WriteFile(s_Path, w_Warehouse);
                            Console.WriteLine("Данные записаны в файл");
                            break;

                        case 11:
                            Console.WriteLine("11. Выход");
                            return;

                        default:
                            Console.WriteLine("Введите корректный пункт меню.");
                            break;
                    }
                }
                catch (ApplicationException e_Ex)
                {
                    Console.WriteLine(e_Ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите корректное значение.");
                }
                catch (Exception e_Ex)
                {
                    Console.WriteLine("Код ошибки: {0}. Обратитесь в службу поддержки.",Math.Abs(e_Ex.HResult));
                }
            }
        }
    }
}
