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
    public partial class FormMuller : Form
    {
        public FormMuller()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Validar que no falte nada
            if (string.IsNullOrWhiteSpace(txtCoeficientes.Text) || string.IsNullOrWhiteSpace(txtX0.Text) ||
                string.IsNullOrWhiteSpace(txtX1.Text) || string.IsNullOrWhiteSpace(txtX2.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Llena todos los campos bro (Recuerda los coeficientes separados por espacios).");
                return;
            }

            try
            {
                MetodosNumericos metodos = new MetodosNumericos();

                // 2. Leemos puntos y tolerancia con el traductor universal
                double x0 = metodos.ConvertirADouble(txtX0.Text);
                double x1 = metodos.ConvertirADouble(txtX1.Text);
                double x2 = metodos.ConvertirADouble(txtX2.Text);
                double tol = metodos.ConvertirADouble(txtTolerancia.Text);

                // 🛡️ 3. REGLA MATEMÁTICA: PUNTOS DIFERENTES 
                if (x0 == x1 || x1 == x2 || x0 == x2)
                {
                    MessageBox.Show("Los tres puntos iniciales deben ser diferentes para la parábola de Müller.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. PREPARAMOS LOS COEFICIENTES (Al estilo Bairstow)
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1;

                if (n < 2)
                {
                    MessageBox.Show("Bro, Müller necesita al menos un polinomio de grado 2 (3 coeficientes).");
                    return;
                }

                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    // Usamos el traductor universal para cada numerito del arreglo
                    a[n - i] = metodos.ConvertirADouble(partes[i]);
                }

                // 5. Llamamos al motor de Müller
                string raizEncontrada = metodos.Muller(a, x0, x1, x2, tol, dgvMuller);
                lblRaiz.Text = "Raíz: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica los datos bro: " + ex.Message);
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
                metodos.ExportarAExcel(dgvMuller);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }
    }
}
