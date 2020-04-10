using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Warehouse
{
    // Чтение и запись данных из файла
    public static class GetSetInfoFromFile
    {

        // Чтение информации из файла и добавление в склад
        public static void ReadFile(string s_Path, Warehouse w_Warehouse)
        {
            if (!File.Exists(s_Path) || Path.GetExtension(s_Path)!=".txt")
            {
                throw new ApplicationException("Введите существующий файл с расширением .txt!");
            }

            // Считывание строк файла
            string[] s_Separator = { Environment.NewLine };
            string[] s_Lines = null;
            using (StreamReader s_Reader = new StreamReader(s_Path))
            {
                s_Lines = s_Reader.ReadToEnd().Split(s_Separator, StringSplitOptions.RemoveEmptyEntries);
            }

            // Добавление информации
            // Первая строка должна содержать название склада, совпадающее с загруженным
            if (s_Lines[0].Split(':')[0]!=w_Warehouse.GetType().Name || s_Lines[0].Split(':')[1]!=w_Warehouse.GetSetName)
            {
                throw new ApplicationException("Неверные данные в файле, вид первой строки: Warehouse:имя_склада");
            }

            // Добавление палет и коробок
            AddPalletFromFile(s_Lines, w_Warehouse);
        }

        // Добавление паллет и коробок
        private static void AddPalletFromFile(string[] s_Lines, Warehouse w_Warehouse)
        {
            // считывание палет
            for (int i = 1; i < s_Lines.Length;)
            {
                // Если палета, иначе исключение
                if (s_Lines[i].Split(':')[0] == "Pallet")
                {
                    // Имя
                    string s_Name = s_Lines[i].Split(':')[1].Split(';')[0];
                    // Ширина
                    double d_Width = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[1]);
                    if (d_Width <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Высота
                    double d_Height = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[2]);
                    if (d_Height <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Глубина
                    double d_Depth = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[3]);
                    if (d_Depth <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }

                    // Добавление паллеты
                    object[,] o_PalletInformation = new object[1, 4];
                    o_PalletInformation[0, 0] = s_Name;
                    o_PalletInformation[0, 1] = d_Width;
                    o_PalletInformation[0, 2] = d_Height;
                    o_PalletInformation[0, 3] = d_Depth;
                    w_Warehouse.AddPallets(o_PalletInformation);
                    // Получение id
                    Guid g_Id = w_Warehouse.GetPallets.Last().GetId;
                    // Счетчик
                    i++;
                    // Добавление коробки в палету
                    i = AddBoxInPalletFromFile(s_Lines, g_Id, i, w_Warehouse);
                }
                else
                {
                    throw new ApplicationException("Неверные данные в файле, вид строк с палетами: Pallet:данные_палеты.");
                }
            }
        }

        // Добавление коробки в палету
        private static int AddBoxInPalletFromFile(string[] s_Lines,Guid g_PalletId, int i, Warehouse w_Warehouse)
        {
            // Считывание коробок паллеты
            for (; i < s_Lines.Length && s_Lines[i].Split(':')[0] != "Pallet"; i++)
            {
                // Если коробка, иначе исключение
                if (s_Lines[i].Split(':')[0] == "Box")
                {
                    // Имя
                    string s_Name = s_Lines[i].Split(':')[1].Split(';')[0];
                    // Ширина
                    double d_Width = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[1]);
                    if (d_Width <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Высота
                    double d_Height = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[2]);
                    if (d_Height <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Глубина
                    double d_Depth = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[3]);
                    if (d_Depth <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Вес
                    double d_Weight = Convert.ToDouble(s_Lines[i].Split(':')[1].Split(';')[4]);
                    if (d_Weight <= 0)
                    {
                        throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                    }
                    // Срок годности либо дата производства
                    object o_Help = s_Lines[i].Split(':')[1].Split(';')[5];
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
                            throw new ApplicationException("Неверные данные в файле, введите корректное значение.");
                        }
                        o_Help = i_ShelfLife;
                    }

                    // Добавление коробки
                    object[,] o_BoxInformation = new object[1, 6];
                    o_BoxInformation[0, 0] = s_Name;
                    o_BoxInformation[0, 1] = d_Width;
                    o_BoxInformation[0, 2] = d_Height;
                    o_BoxInformation[0, 3] = d_Depth;
                    o_BoxInformation[0, 4] = d_Weight;
                    o_BoxInformation[0, 5] = o_Help;
                    w_Warehouse.AddBoxesInPallet(g_PalletId, o_BoxInformation);
                }
                else
                {
                    throw new ApplicationException("Неверные данные в файле, вид строк с коробками: Box:данные_коробки.");
                }
            }
            return i;
        }

        // Запись информации в файл из склада
        public static void WriteFile(string s_Path, Warehouse w_Warehouse)
        {
            if (!File.Exists(s_Path) || Path.GetExtension(s_Path) != ".txt")
            {
                throw new ApplicationException("Введите существующий файл с расширением .txt!");
            }

            // Запись строк в файл (режим перезаписи файла)
            using (StreamWriter s_Writer = new StreamWriter(s_Path, false))
            {
                // Имя склада
                s_Writer.WriteLine("Warehouse:" + w_Warehouse.GetSetName);

                // Цикл палет
                for (int i = 0; i < w_Warehouse.GetPallets.Count; i++)
                {
                    // Записываем палету
                    s_Writer.WriteLine("Pallet:{0};{1};{2};{3}", w_Warehouse.GetPallets[i].GetSetName,
                        w_Warehouse.GetPallets[i].GetSetWidth, w_Warehouse.GetPallets[i].GetSetHeight,
                        w_Warehouse.GetPallets[i].GetSetDepth);
                    // Записываем коробки для паллеты, если есть
                    foreach (Box b_Box in w_Warehouse.GetPallets[i].GetBoxes)
                    {
                        // Если указана дата производства, записываем ее, иначе срок годности
                        if (b_Box.GetProductionDate == DateTime.MinValue)
                        {
                            s_Writer.WriteLine("Box:{0};{1};{2};{3};{4};{5}", b_Box.GetSetName, b_Box.GetSetWidth, b_Box.GetSetHeight,
                            b_Box.GetSetDepth, b_Box.GetSetWeight, b_Box.GetShelfLife);
                        }
                        else
                        {
                            s_Writer.WriteLine("Box:{0};{1};{2};{3};{4};{5}", b_Box.GetSetName, b_Box.GetSetWidth, b_Box.GetSetHeight,
                            b_Box.GetSetDepth, b_Box.GetSetWeight, b_Box.GetProductionDate.ToShortDateString());
                        }
                    }
                }
            }
        }
    }
}
