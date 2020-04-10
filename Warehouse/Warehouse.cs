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
        // Имя склада
        private string s_Name;
        // Список палет склада
        private List<Pallet> p_Pallets;

        public Warehouse(string s_Name)
        {
            this.s_Name = s_Name;
            p_Pallets = new List<Pallet>();
        }

        public string GetSetName
        {
            get { return s_Name; }
            set { s_Name = value; }
        }
        public List<Pallet> GetPallets
        {
            get { return p_Pallets; }
        }

        // Добавление палет на склад
        public void AddPallets(object[,] o_Information)
        {
            for (int i = 0; i < o_Information.GetUpperBound(0)+1; i++)
            {
                Pallet p_Pallet = new Pallet((string)o_Information[i,0], (double)o_Information[i, 1], (double)o_Information[i, 2],
                    (double)o_Information[i, 3]);
                p_Pallets.Add(p_Pallet);
            }
        }

        // Удаление палет со склада по id
        public void DeletePallets(Guid[] g_Ids)
        {
            // Формирование массива удаляемых палет(проверка всех id на наличие)
            int[] i_FindPallets = new int[g_Ids.Length];
            for (int i = 0; i < g_Ids.Length; i++)
            {
                i_FindPallets[i] = p_Pallets.FindIndex(x => x.GetId == g_Ids[i]);
                if (i_FindPallets[i] == -1)
                {
                    throw new ApplicationException("Палета с указанным id не найдена!");
                }
            }
            // Удаление
            for (int i = 0; i < i_FindPallets.Length; i++)
            {
                // Удаление коробок
                Guid[] g_IdsBoxes = new Guid[p_Pallets[i_FindPallets[i]].GetBoxes.Count];
                for (int j=0;j<p_Pallets[i_FindPallets[i]].GetBoxes.Count;j++)
                {
                    g_IdsBoxes[j] = p_Pallets[i_FindPallets[i]].GetBoxes[j].GetId;
                }
                p_Pallets[i_FindPallets[i]].DeleteBoxes(g_IdsBoxes);
                // Удаление
                p_Pallets.RemoveAt(i_FindPallets[i]);
            }
        }

        // Добавить коробки в паллету  
        public void AddBoxesInPallet(Guid g_Id, object[,] o_Information)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            if (p_Pallet != null)
            {
                p_Pallet.AddBoxes(o_Information);
            }
            else
            {
                throw new ApplicationException("Палета с указанным id не найдена!");
            }
        }

        // Удалить заданные коробки у палеты
        public void DeleteBoxesInPallet(Guid g_Id, Guid[] g_Ids)
        {
            Pallet p_Pallet = p_Pallets.Find(x => x.GetId == g_Id);
            if (p_Pallet != null)
            {
                p_Pallet.DeleteBoxes(g_Ids);
            }
            else
            {
                throw new ApplicationException("Паллета с указанным id не найдена!");
            }
        }
    }
}
