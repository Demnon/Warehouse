using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    // Паллета
    public class Pallet : Container
    {
        // Список коробок
        List<Box> b_Boxes;
        // Срок годности (количество оставшихся дней)
        int i_ShelfLife;

        public Pallet(string s_Name, double d_Width, double d_Height, double d_Depth)
           : base(s_Name, d_Width, d_Height, d_Depth, 0)
        {
            b_Boxes = new List<Box>();
            i_ShelfLife = 0;
            d_Weight = 30;
        }

        // Свойства
        public List<Box> GetBoxes 
        { 
            get { return b_Boxes; } 
        }

        public int GetShelfLife
        {
            get { return i_ShelfLife; }
        }

        public override double GetSetWeight 
        {
            get { return d_Weight; }
        }

        // Добавление коробок в паллету c указанным сроком годности
        // Аргументы: 1 - количество коробок, остальные аргументы для создания коробки с указанным сроком годности
        public void AddBoxes(int i_Count,string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight, int i_ShelfLife)
        {
            // Создание экземпляров коробок и добавление в список, а также пересчет срока годности паллеты
            for (int i=0;i<i_Count;i++)
            {
                Box b_Box = new Box(s_Name, d_Width, d_Height, d_Depth, d_Weight, i_ShelfLife);
                b_Boxes.Add(b_Box);

                // Если срок годности добавляемой коробки меньше, то присваиваем его паллете
                if (b_Box.GetShelfLife < this.i_ShelfLife)
                {
                    this.i_ShelfLife = b_Box.GetShelfLife;
                }

                // Увеличиваем вес паллеты
                d_Weight += d_Weight;
            }
        }

        // Добавление коробок в паллету c указанным сроком годности
        // Аргументы: 1 - количество коробок, остальные аргументы для создания коробки с указанной датой производства
        public void AddBoxes(int i_Count, string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight, DateTime d_ProductionDate)
        {
            // Создание экземпляров коробок и добавление в список, а также пересчет срока годности паллеты
            for (int i = 0; i < i_Count; i++)
            {
                Box b_Box = new Box(s_Name, d_Width, d_Height, d_Depth, d_Weight, d_ProductionDate);
                b_Boxes.Add(b_Box);

                // Если срок годности добавляемой коробки меньше, то присваиваем его паллете
                if (b_Box.GetShelfLife < this.i_ShelfLife)
                {
                    this.i_ShelfLife = b_Box.GetShelfLife;
                }

                // Увеличиваем вес паллеты
                d_Weight += d_Weight;
            }
        }

        // Удалить коробку из палеты по id
        public void DeleteBox(Guid g_Id)
        {
            Box b_Box = b_Boxes.Find(x => x.GetId == g_Id);
            if (b_Box!=null)
            {
                // Уменьшаем вес паллеты
                d_Weight -= b_Box.GetSetWeight;

                // Удаление
                b_Boxes.Remove(b_Box);

                // Если больше нет коробки с таким сроком годности, то пересчитываем срок годности паллеты
                b_Box = b_Boxes.Find(x => x.GetShelfLife == this.i_ShelfLife);
                if (b_Box == null)
                {
                    // Находим коробку с минимальным сроком годности
                    int i_Help = 0;
                    for (int i=0;i<b_Boxes.Count;i++)
                    {
                        if (i == 0)
                        {
                            i_Help = b_Boxes[0].GetShelfLife;
                        }
                        if (i_Help<b_Boxes[i].GetShelfLife)
                        {
                            i_Help = b_Boxes[i].GetShelfLife;
                        }
                    }
                    // Присваиваем сроку годности паллеты
                    this.i_ShelfLife = i_Help;
                }
            }
        }

        // Вычисление объема паллеты
        public override double GetVolume()
        {
            // Вычисляем сумму всех объемов коробок
            double d_SumVolumeBoxes = 0;
            for (int i=0;i<b_Boxes.Count;i++)
            {
                d_SumVolumeBoxes += b_Boxes[i].GetVolume();
            }

            return d_Width * d_Height * d_Depth + d_SumVolumeBoxes;
        }
    }
}
