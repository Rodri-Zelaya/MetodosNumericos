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
    public partial class FormBairstow : Form
    {
        public FormBairstow()
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
                // 1. Preparamos los datos
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1;

                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    a[n - i] = double.Parse(partes[i].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                }

                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                // 2. Preparamos variables vacías para atrapar r0 y s0
                double r0_calculado, s0_calculado;

                // 3. Ejecutamos el método
                string resultado = metodos.Bairstow(a, tol, dgvBairstow, out r0_calculado, out s0_calculado);

                // 4. Mostramos resultados en la pantalla
                lblRaiz.Text = "Raíces encontradas: " + resultado;

                // Asegúrate de tener creados estos dos Labels en tu diseño:
                lblR0.Text = "r0 automático: " + r0_calculado.ToString("F4");
                lblS0.Text = "s0 automático: " + s0_calculado.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error bro. Revisa la sintaxis: " + ex.Message);
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
                metodos.ExportarAExcel(dgvBairstow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }
    }
}
