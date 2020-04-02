using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    // Общий родитель для коробок и паллет. Абстрактный для того, чтобы нельзя было создать экземпляры этого класса
    public abstract class Container
    {
        private protected Guid g_Id;
        // Имя
        private protected string s_Name;
        // Ширина 
        private protected double d_Width;
        // Высота
        private protected double d_Height;
        // Глубина
        private protected double d_Depth;
        // Вес
        private protected double d_Weight;

        public Container(string s_Name, double d_Width, double d_Height, double d_Depth, double d_Weight)
        {
            this.s_Name = s_Name;
            this.d_Width = d_Width;
            this.d_Height = d_Height;
            this.d_Depth = d_Depth;
            this.d_Weight = d_Weight;
            g_Id = Guid.NewGuid();
        }

        // Cвойства
        public Guid GetId
        {
            get { return g_Id; }
        }
        public string GetSetName
        {
            get { return s_Name; }
            set { s_Name = value; }
        }
        public double GetSetWidth
        {
            get { return d_Width; }
            set { d_Width = value; }
        }
        public double GetSetHeight
        {
            get { return d_Height; }
            set { d_Height = value; }
        }
        public double GetSetDepth
        {
            get { return d_Depth; }
            set { d_Depth = value; }
        }
        public virtual double GetSetWeight
        {
            get { return d_Weight; }
            set { d_Weight = value; }
        }

        // Абстрактный метод вычисления объема
        public abstract double GetVolume();
    }
}
