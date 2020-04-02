using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Warehouse
{
    // Склад, содержащий список паллет
    public class Warehouse
    {
        List<Pallet> p_Pallets;

        public Warehouse()
        {
            p_Pallets = new List<Pallet>();
        }

        // Добавление паллеты на склад
        // Аргументы: 1 - количество паллет, остальные аргументы для создания паллеты 
        public void AddPallet(int i_Count, string s_Name, double d_Width, double d_Height, double d_Depth)
        {
            for (int i = 0; i < i_Count; i++)
            {
                Pallet p_Pallet = new Pallet(s_Name, d_Width, d_Height, d_Depth);
                p_Pallets.Add(p_Pallet);
            }
        }

        // Добавить коробку в паллету (срок годности)     
        public void AddBoxInPallet(int i_Count,Guid g_Id, string s_Name, double d_Width, double d_Height, double d_Depth,double d_Weight, int i_ShelfLife)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            p_Pallet.AddBoxes(i_Count,s_Name, d_Width, d_Height, d_Depth,d_Weight,i_ShelfLife);
        }

        // Добавить коробку в паллету (дата производства)

        public void AddBoxInPallet(int i_Count, Guid g_Id, string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight, DateTime d_ProductionDate)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            if (p_Pallet != null)
            {
                p_Pallet.AddBoxes(i_Count, s_Name, d_Width, d_Height, d_Depth, d_Weight, d_ProductionDate);
            }
        }

        // Удаление паллеты со склада по id
        public void DeletePallet(Guid g_Id)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            if (p_Pallet != null)
            {
                // Удаление коробок у паллеты
                for (int i = 0; i < p_Pallet.GetBoxes.Count; i++)
                {
                    p_Pallet.DeleteBox(p_Pallet.GetBoxes[i].GetId);
                }
                // Удаление паллеты
                p_Pallets.Remove(p_Pallet);
            }
        }

        // Группировка паллет по сроку годности, сортировка групп по возрастанию срока годности,
        // в группе сортировка паллет по весу (не успел реализовать), вывод на экран
        public void GroupPallets()
        {
            Console.WriteLine("Паллеты после группировки:");
            //IOrderedEnumerable<IGrouping<int, Pallet>>
            // Группировка по сроку годности
            var Groups = from p_Pallet in p_Pallets group p_Pallet by p_Pallet.GetShelfLife into newGroup
                         orderby newGroup.Key
                         select newGroup;
            foreach (var ShelfLifeGroup in Groups)
            {
                foreach (var p_Pallet in ShelfLifeGroup)
                {
                    Console.WriteLine(p_Pallet.GetId + " " + p_Pallet.GetSetName + " " + p_Pallet.GetSetWidth + " " +
                        p_Pallet.GetSetHeight + " " + p_Pallet.GetSetDepth + " " + p_Pallet.GetSetWeight + " " +
                        p_Pallet.GetShelfLife + " ");
                }
            }
        }

        // Вывод содержимого
        public void Output()
        {
           for (int i=0;i<p_Pallets.Count;i++)
            {
                Console.WriteLine("Паллета: {0}, {1}, {2}, {3}, {4}, {5}, {6}", p_Pallets[i].GetId, p_Pallets[i].GetSetName, p_Pallets[i].GetSetWidth,
                    p_Pallets[i].GetSetHeight, p_Pallets[i].GetSetDepth, p_Pallets[i].GetSetWeight, p_Pallets[i].GetShelfLife);
                Console.WriteLine("Коробки:");
                for (int j=0;j<p_Pallets[i].GetBoxes.Count;j++)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", p_Pallets[i].GetBoxes[j].GetId,
                        p_Pallets[i].GetBoxes[j].GetSetName, p_Pallets[i].GetBoxes[j].GetSetWidth,
                    p_Pallets[i].GetBoxes[j].GetSetHeight, p_Pallets[i].GetBoxes[j].GetSetDepth, p_Pallets[i].GetBoxes[j].GetSetWeight,
                    p_Pallets[i].GetBoxes[j].GetShelfLife, p_Pallets[i].GetBoxes[j].GetProductionDate.ToShortDateString());
                }
            }
        }
    }
}
