using System;

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

                            // Массив значений 
                            object[,] o_Information = null;
                            // Заполнение информации пользователем
                            Menu.MenuItem1(o_Information);

                            // Добавление
                            w_Warehouse.AddPallets(o_Information);
                            Console.WriteLine("Добавление завершено");
                            break; 
                        case 2:
                            Console.WriteLine("2. Удалить палеты из склада");

                            // Массив id 
                            Guid[] g_Ids = null;
                            // Заполнение информации пользователем
                            Menu.MenuItem2(g_Ids);

                            // Удаление
                            w_Warehouse.DeletePallets(g_Ids);
                            Console.WriteLine("Удаление завершено");
                            break;
                        case 3:
                            Console.WriteLine("3. Добавить коробки в заданную палету");

                            o_Information = null;
                            // Заполнение информации пользователем
                            Guid g_Id = Menu.MenuItem3(o_Information);

                            // Добавление
                            w_Warehouse.AddBoxesInPallet(g_Id,o_Information);
                            Console.WriteLine("Добавление завершено");
                            break;
                        case 4:
                            Console.WriteLine("4. Удалить коробки из заданной палеты");

                            g_Ids = null;
                            // Заполнение информации пользователем
                            g_Id = Menu.MenuItem4(g_Ids);

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
