using System;

namespace Warehouse
{
    // Содержимое пунктов меню
    public static class Menu
    {
        // Пункт 1
        public static void MenuItem1(object[,] o_Information)
        {
            Console.WriteLine("Введите количество добавляемых палет:");
            int i_Count = 0;
            if (!int.TryParse(Console.ReadLine(), out i_Count) || i_Count <= 0)
            {
                throw new ApplicationException("Введите корректное число добавляемых палет.");
            }

            o_Information = new object[i_Count, 4];
            // Цикл ввода значений
            for (int i = 0; i < i_Count; i++)
            {
                Console.WriteLine("Введите имя " + (i + 1).ToString() + " палеты:");

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
        }

        // Пункт 2
        public static void MenuItem2(Guid[] g_Ids)
        {
            Console.WriteLine("Введите количество удаляемых палет:");
            int i_Count = 0;
            if (!int.TryParse(Console.ReadLine(), out i_Count) || i_Count <= 0)
            {
                throw new ApplicationException("Введите корректное число удаляемых палет.");
            }
            // Массив id 
            g_Ids = new Guid[i_Count];
            // Цикл ввода id
            for (int i = 0; i < i_Count; i++)
            {
                Console.WriteLine("Введите id удаляемой " + (i + 1).ToString() + " палеты:");
                g_Ids[i] = new Guid(Console.ReadLine());
            }
        }

        // Пункт 3
        public static Guid MenuItem3(object[,] o_Information)
        {
            Console.WriteLine("Введите id палеты:");
            Guid g_Id = new Guid(Console.ReadLine());

            Console.WriteLine("Введите количество добавляемых коробок:");
            int i_Count = 0;
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

            return g_Id;
        }

        // Пункт 4
        public static Guid MenuItem4(Guid[] g_Ids)
        {
            Console.WriteLine("Введите id палеты:");
            Guid g_Id = new Guid(Console.ReadLine());

            Console.WriteLine("Введите количество удаляемых коробок:");
            int i_Count = 0;
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

            return g_Id;
        }
    }
}
