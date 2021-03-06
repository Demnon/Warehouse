﻿using System;

namespace Warehouse
{
    // Коробка
    public class Box : Container
    {
        // Срок годности (количество оставшихся дней)
        private int i_ShelfLife;
        // Дата производства (формат: дд.мм.гггг)
        private DateTime d_ProductionDate;

        // Конструктор для задания срока годности
        public Box(string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight, int i_ShelfLife) 
            :base(s_Name,d_Width,d_Height,d_Depth,d_Weight)
        {
            this.i_ShelfLife = i_ShelfLife;
            d_ProductionDate = DateTime.MinValue;
        }

        // Конструктор для задания даты производства
        public Box(string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight, DateTime d_ProductionDate)
            : base(s_Name, d_Width, d_Height, d_Depth, d_Weight)
        {
            this.d_ProductionDate = d_ProductionDate;
            // Если дата производства, то вычисляем срок годности + 100 дн
            i_ShelfLife = (DateTime.Now - d_ProductionDate).Days + 100;
        }

        // Свойства
        public int GetShelfLife
        {
            get { return i_ShelfLife; }
        }

        public DateTime GetProductionDate
        {
            get { return d_ProductionDate; }
        }

        // Вычисление объема коробки
        public override double GetVolume()
        {
            return d_Width * d_Height * d_Depth;
        }
    }
}
