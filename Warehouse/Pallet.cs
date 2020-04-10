using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    // Палета
    public class Pallet : Container
    {
        // Список коробок
        private List<Box> b_Boxes;
        // Срок годности (количество оставшихся дней)
        private int i_ShelfLife;

        public Pallet(string s_Name, double d_Width, double d_Height, double d_Depth)
           : base(s_Name, d_Width, d_Height, d_Depth, 0)
        {
            b_Boxes = new List<Box>();
            i_ShelfLife = 0;
            this.d_Weight = 30;
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

        // Добавление коробок в палету
        public void AddBoxes(object[,] o_Information)
        {
            // Создание экземпляров коробок и добавление в список, а также пересчет срока годности палеты
            for (int i=0;i<o_Information.GetUpperBound(0)+1;i++)
            {
                // В зависимости от типа последнего параметра вызываем соответствующий конструктор
                Box b_Box = o_Information[i, 5].GetType().Name == "DateTime" ?
                    new Box((string)o_Information[i, 0], (double)o_Information[i, 1], (double)o_Information[i, 2],
                        (double)o_Information[i, 3], (double)o_Information[i, 4], (DateTime)o_Information[i, 5]):
                        new Box((string)o_Information[i, 0], (double)o_Information[i, 1], (double)o_Information[i, 2],
                        (double)o_Information[i, 3], (double)o_Information[i, 4], (int)o_Information[i, 5]);
                
                b_Boxes.Add(b_Box);

                // Если коробка одна, то ее срок годности - срок годности палеты
                if (b_Boxes.Count == 1)
                {
                    i_ShelfLife = b_Box.GetShelfLife;
                }
                // Если срок годности добавляемой коробки меньше, то присваиваем его палете
                if (b_Box.GetShelfLife < i_ShelfLife)
                {
                    i_ShelfLife = b_Box.GetShelfLife;
                }

                // Увеличиваем вес паллеты
                d_Weight += b_Box.GetSetWeight;
            }
        }

        // Удалить заданные коробки из палеты по id
        public void DeleteBoxes(Guid[] g_Ids)
        {
            // Формирование массива удаляемых коробок (проверка всех id на наличие)
            Box[] b_FindBoxes = new Box[g_Ids.Length];
            for (int i = 0; i < g_Ids.Length; i++)
            {
                b_FindBoxes[i] = b_Boxes.Find(x => x.GetId == g_Ids[i]);
                if (b_FindBoxes[i] == null)
                {
                    throw new ApplicationException("Коробка с указанным id не найдена!");
                }
            }
            // Удаление
            for (int i = 0; i < b_FindBoxes.Length; i++)
            {
                // Уменьшаем вес палеты
                d_Weight -= b_FindBoxes[i].GetSetWeight;

                // Удаление
                b_Boxes.Remove(b_FindBoxes[i]);
            }
            // После удаления находим коробку с минимальным сроком годности
            int i_Help = 0;
            for (int i = 0; i < b_Boxes.Count; i++)
            {
                if (i == 0)
                {
                    i_Help = b_Boxes[0].GetShelfLife;
                }
                if (i_Help < b_Boxes[i].GetShelfLife)
                {
                    i_Help = b_Boxes[i].GetShelfLife;
                }
            }
            // Присваиваем сроку годности палеты
            i_ShelfLife = i_Help;
        }

        // Вычисление объема палеты
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
