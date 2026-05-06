using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormHornerNewton : Form
    {
        public FormHornerNewton()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCoeficientes.Text))
            {
                MessageBox.Show("Pon los coeficientes bro, separados por espacios.");
                return;
            }

            try
            {
                // Partimos el texto en pedacitos cada vez que haya un espacio
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1; // Calculamos el grado del polinomio

                // Acomodamos los números de izquierda a derecha (an hasta a0)
                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    a[n - i] = double.Parse(partes[i].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                }

                double r0 = double.Parse(txtR0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                string raizEncontrada = metodos.HornerNewton(a, r0, tol, dgvHornerNewton);
                lblRaiz.Text = "Raíz encontrada: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica los datos. Recuerda poner los coeficientes separados por espacios: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciamos tu clase cerebro donde metimos el código universal
                MetodosNumericos metodos = new MetodosNumericos();

                // 2. Llamamos al método y le mandamos la tabla de esta ventana
                // OJO: Si tu tabla se llama distinto (ej. dgvNewton o dgvBairstow), cámbiale el nombre aquí
                metodos.ExportarAExcel(dgvHornerNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }
    }
}
