using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Draw
{
    abstract class Figura : IComparable
    {
        public Punto punto1 { get; set; }
        public Punto punto2 { get; set; }

        public SolidBrush brocha { get; set; }
        public Pen pluma { get; set; }
        public int anchoPluma { get; set; }
        public Color colorRelleno { get; set; }
        public Color colorContorno { get; set; }

        public int Alto
        {
            get 
            {
                return punto2.y - punto1.y;
            }
        }
        public int Ancho
        {
            get
            {
                return punto2.x - punto1.x;
            }
        }
        public int Area
        {
            get
            {
                return Ancho * Alto;
            }
        }
        public Figura(Punto punto1, Punto punto2)
        {
            if (punto1.x <= punto2.x && punto1.y <= punto2.y)
            {
                this.punto1 = punto1;
                this.punto2 = punto2;
            }
            else if (punto1.x >= punto2.x && punto1.y <= punto2.y)
            {
                int temp;
                temp = punto1.x;
                punto1.x = punto2.x;
                punto2.x = temp;
                this.punto1 = punto1;
                this.punto2 = punto2;
            }
            else if (punto1.x <= punto2.x && punto1.y >= punto2.y)
            {
                int temp;
                temp = punto1.y;
                punto1.y = punto2.y;
                punto2.y = temp;
                this.punto1 = punto1;
                this.punto2 = punto2;
            }
            else if (punto1.x >= punto2.x && punto1.y >= punto2.y)
            {
                this.punto1 = punto2;
                this.punto2 = punto1;
            }
            colorRelleno = Color.White;
            colorContorno = Color.Black;
        }

        public abstract void Dibuja(Form forma);

        public bool EstaDentro(int x, int y)
        {
            return (x >= punto1.x && x <= punto2.x && y >= punto1.y && y <= punto2.y) ;
        }

        public int CompareTo(object obj)
        {
            return this.Area.CompareTo((obj as Figura).Area);
        }
    }
    class Rectangulo : Figura
    {
        public Rectangulo(Punto punto1, Punto punto2) : base(punto1, punto2)
        {

        }
        public override void Dibuja(Form forma)
        {
            Graphics graphics = forma.CreateGraphics();
            graphics.FillRectangle(new SolidBrush(colorRelleno), punto1.x, punto1.y, punto2.x - punto1.x, punto2.y - punto1.y);
            graphics.DrawRectangle(new Pen(colorContorno), punto1.x, punto1.y, punto2.x - punto1.x, punto2.y - punto1.y);
        }
    }

    class Elipse : Figura
    {
        public Elipse(Punto punto1, Punto punto2) : base(punto1, punto2)
        {

        }
        public override void Dibuja(Form forma)
        {
            Graphics graphics = forma.CreateGraphics();
            graphics.FillEllipse(new SolidBrush(colorRelleno), punto1.x, punto1.y, punto2.x - punto1.x, punto2.y - punto1.y);
            graphics.DrawEllipse(new Pen(colorContorno), punto1.x, punto1.y, punto2.x - punto1.x, punto2.y - punto1.y);
        }
    }
    class Triangulo : Figura
    {
        public Triangulo(Punto punto1, Punto punto2) : base(punto1, punto2)
        {

        }
        public override void Dibuja(Form forma)
        {
            Graphics graphics = forma.CreateGraphics();
            Point[] puntos = { new Point(punto1.x, punto2.y), new Point((punto2.x + punto1.x) / 2, punto1.y), new Point(punto2.x, punto2.y)};
            graphics.FillPolygon(new SolidBrush(colorRelleno), puntos);
            graphics.DrawPolygon(new Pen(colorContorno), puntos);
        }
    }
}
