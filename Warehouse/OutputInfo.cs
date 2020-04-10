using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    // Вывод информации (в том числе и сгруппированной) на консоль
    public static class OutputInfo
    {
        // Вывод содержимого всех палет
        public static void OutputAllPallet(List<Pallet> p_Pallets)
        {
            if (p_Pallets.Count == 0)
            {
                Console.WriteLine("...");
            }
            for (int i = 0; i < p_Pallets.Count; i++)
            {
                Console.WriteLine("\nПаллета: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}", p_Pallets[i].GetId, p_Pallets[i].GetSetName, p_Pallets[i].GetSetWidth,
                    p_Pallets[i].GetSetHeight, p_Pallets[i].GetSetDepth, p_Pallets[i].GetVolume(), p_Pallets[i].GetSetWeight, p_Pallets[i].GetShelfLife);

                Console.WriteLine("Коробки:");
                if (p_Pallets[i].GetBoxes.Count == 0)
                {
                    Console.WriteLine("...");
                }
                for (int j = 0; j < p_Pallets[i].GetBoxes.Count; j++)
                {
                    // Если нет даты производства, то не выводим ее
                    if (p_Pallets[i].GetBoxes[j].GetProductionDate == DateTime.MinValue)
                    {
                        Console.WriteLine("Коробка: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}", p_Pallets[i].GetBoxes[j].GetId,
                        p_Pallets[i].GetBoxes[j].GetSetName, p_Pallets[i].GetBoxes[j].GetSetWidth,
                        p_Pallets[i].GetBoxes[j].GetSetHeight, p_Pallets[i].GetBoxes[j].GetSetDepth, p_Pallets[i].GetBoxes[j].GetVolume(), p_Pallets[i].GetBoxes[j].GetSetWeight,
                        p_Pallets[i].GetBoxes[j].GetShelfLife);
                    }
                    else
                    {
                        Console.WriteLine("Коробка: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}, дата производства: {8}", p_Pallets[i].GetBoxes[j].GetId,
                        p_Pallets[i].GetBoxes[j].GetSetName, p_Pallets[i].GetBoxes[j].GetSetWidth,
                        p_Pallets[i].GetBoxes[j].GetSetHeight, p_Pallets[i].GetBoxes[j].GetSetDepth, p_Pallets[i].GetBoxes[j].GetVolume(), p_Pallets[i].GetBoxes[j].GetSetWeight,
                        p_Pallets[i].GetBoxes[j].GetShelfLife, p_Pallets[i].GetBoxes[j].GetProductionDate.ToShortDateString());
                    }
                }
            }
        }

        // Вывод содержимого заданной паллеты
        public static void OutputPallet(Guid g_Id,List<Pallet> p_Pallets)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            if (p_Pallet != null)
            {
                Console.WriteLine("\nПаллета: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}", p_Pallet.GetId, p_Pallet.GetSetName,
                    p_Pallet.GetSetWidth, p_Pallet.GetSetHeight, p_Pallet.GetSetDepth, p_Pallet.GetVolume(), p_Pallet.GetSetWeight, p_Pallet.GetShelfLife);

                Console.WriteLine("Коробки:");
                if (p_Pallet.GetBoxes.Count == 0)
                {
                    Console.WriteLine("...");
                }
                for (int i = 0; i < p_Pallet.GetBoxes.Count; i++)
                {
                    // Если нет даты производства, то не выводим ее
                    if (p_Pallet.GetBoxes[i].GetProductionDate == DateTime.MinValue)
                    {
                        Console.WriteLine("Коробка: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}", p_Pallet.GetBoxes[i].GetId,
                        p_Pallet.GetBoxes[i].GetSetName, p_Pallet.GetBoxes[i].GetSetWidth,
                        p_Pallet.GetBoxes[i].GetSetHeight, p_Pallet.GetBoxes[i].GetSetDepth, p_Pallet.GetBoxes[i].GetVolume(), p_Pallet.GetBoxes[i].GetSetWeight,
                        p_Pallet.GetBoxes[i].GetShelfLife);
                    }
                    else
                    {
                        Console.WriteLine("Коробка: id: {0}, имя: {1}, ширина: {2}, высота: {3}, глубина: {4}, объем: {5}, вес: {6}, срок годности: {7}, дата производства: {8}", p_Pallet.GetBoxes[i].GetId,
                        p_Pallet.GetBoxes[i].GetSetName, p_Pallet.GetBoxes[i].GetSetWidth,
                        p_Pallet.GetBoxes[i].GetSetHeight, p_Pallet.GetBoxes[i].GetSetDepth, p_Pallet.GetBoxes[i].GetVolume(), p_Pallet.GetBoxes[i].GetSetWeight,
                        p_Pallet.GetBoxes[i].GetShelfLife, p_Pallet.GetBoxes[i].GetProductionDate.ToShortDateString());
                    }
                }
            }
            else
            {
                throw new ApplicationException("Паллета с указанным id не найдена!");
            }
        }

        // Группировка паллет по сроку годности, сортировка групп по возрастанию срока годности,
        // в группе сортировка паллет по весу, вывод на экран
        public static void GroupPallets(List<Pallet> p_Pallets)
        {
            var g_Groups = p_Pallets.GroupBy(x => x.GetShelfLife)
                .Select(x => new { x.Key, Items = x.OrderBy(z => z.GetSetWeight).ToList() })
                .OrderBy(p => p.Key).ToArray();
            foreach (var ShelfLifeGroup in g_Groups)
            {
                foreach (var p_Pallet in ShelfLifeGroup.Items)
                {
                    OutputPallet(p_Pallet.GetId, ShelfLifeGroup.Items);
                }
            }
        }
        // Вывести на экран 3 паллеты, которые содержат коробки
        //с наибольшим сроком годности, отсортированные по возрастанию объема
        public static void OutputTreePallets(List<Pallet> p_Pallets)
        {
            List<Pallet> p_SortPallets = p_Pallets.OrderBy(x => x.GetBoxes[x.GetBoxes.FindIndex(y => y == x.GetBoxes.OrderBy(z => z.GetShelfLife).First())]).ToList();
            for (int i=0;i<3;i++)
            {
                OutputPallet(p_SortPallets[i].GetId, p_SortPallets);
            }
        }
    }
}
