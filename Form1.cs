using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw
{
    //Implementar otra figura (triangulo) 
    public partial class Form1 : Form
    {
        public enum DibujaEstaFigura { rectangulo, elipse, triangulo}
        public enum Estado { normal, dibujando, moviendo, seleccionando }
        List<Figura> figuras = new List<Figura>();

        private int estado = (int)Estado.normal;
        private int dibujaEstaFigura = (int)DibujaEstaFigura.rectangulo;
        private Punto p1_actual;
        private Figura figuraSeleccionada;


        private Figura Selecciona(int x, int y)
        {
            for (int i = figuras.Count-1; i >= 0; i--)
            {
                if (figuras[i].EstaDentro(x, y))
                    return figuras[i];
            }
            return null;
        }

        private void DibujaFiguras()
        {
            foreach (var figura in figuras)
            {
                figura.Dibuja(this);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (estado == (int)Estado.dibujando)
            {
                estado = (int)Estado.moviendo;
                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");
                p1_actual = new Punto(e.X, e.Y);
            }
            else if (estado == (int)Estado.seleccionando)
            {
                figuraSeleccionada = Selecciona(e.X, e.Y);
                if(figuraSeleccionada!=null)
                {
                    label1.Text = String.Format($"x:{figuraSeleccionada.punto1.x}, y:{figuraSeleccionada.punto1.y}");
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (estado == (int)Estado.moviendo)
            {
                estado = (int)Estado.dibujando;

                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");

                if (dibujaEstaFigura == (int)DibujaEstaFigura.rectangulo)
                {
                    Rectangulo r = new Rectangulo(p1_actual, new Punto(e.X, e.Y));
                    r.Dibuja(this);
                    figuras.Add(r);
                }
                else if (dibujaEstaFigura == (int)DibujaEstaFigura.elipse)
                {
                    Elipse elipse = new Elipse(p1_actual, new Punto(e.X, e.Y));
                    elipse.Dibuja(this);
                    figuras.Add(elipse);
                }
                else if (dibujaEstaFigura == (int)DibujaEstaFigura.triangulo)
                {
                    Triangulo triangulo = new Triangulo(p1_actual, new Punto(e.X, e.Y));
                    triangulo.Dibuja(this);
                    figuras.Add(triangulo);
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (estado == (int)Estado.dibujando)
            {
                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.DibujaFiguras();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Red;
            }
        }

        private void dibujarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estado = (int)Estado.seleccionando;
        }

        private void dibujarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            estado = (int)Estado.dibujando;
        }

        private void ordenarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figuras.Sort();
            figuras.Reverse();
            DibujaFiguras();
        }

        private void rectanguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dibujaEstaFigura = (int)DibujaEstaFigura.rectangulo;
        }

        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dibujaEstaFigura = (int)DibujaEstaFigura.elipse;
        }

        private void trianguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dibujaEstaFigura = (int)DibujaEstaFigura.triangulo;
        }
    }
}
