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
    public partial class Form1 : Form
    {
        List<Figura> figuras = new List<Figura>();

        private string estado = "normal";
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
            if (estado == "dibujando")
            {
                estado = "moviendo";
                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");
                p1_actual = new Punto(e.X, e.Y);
            }
            else if (estado == "seleccionando")
            {
                figuraSeleccionada = Selecciona(e.X, e.Y);
                if(figuraSeleccionada!=null)
                {
                    button2.Text = String.Format($"x:{figuraSeleccionada.punto1.x}, y:{figuraSeleccionada.punto1.y}");
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (estado == "moviendo")
            {
                estado = "dibujando";

                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");

                if (e.Button == MouseButtons.Left)
                {
                    Rectangulo r = new Rectangulo(p1_actual, new Punto(e.X, e.Y));
                    r.Dibuja(this);
                    figuras.Add(r);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Elipse elipse = new Elipse(p1_actual, new Punto(e.X, e.Y));
                    elipse.Dibuja(this);
                    figuras.Add(elipse);
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (estado == "dibujando")
            {
                label1.Text = String.Format($"x:{e.X}, y:{e.Y}");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.DibujaFiguras();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            estado = "seleccionando";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            estado = "dibujando";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.colorRelleno = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            figuras.Sort();
            figuras.Reverse();
            DibujaFiguras();
        }
    }
}
