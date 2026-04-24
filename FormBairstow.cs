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
                // Cortamos el texto por cada espacio que haya
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1;

                // Creamos el arreglo y acomodamos los coeficientes desde el grado mayor hasta a0
                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    a[n - i] = double.Parse(partes[i].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                }

                double r0 = double.Parse(txtR0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double s0 = double.Parse(txtS0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                string raices = metodos.Bairstow(a, r0, s0, tol, dgvBairstow);
                lblRaiz.Text = "Raíces: " + raices;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica los datos. Recuerda separarlos con ESPACIOS, no con comas: " + ex.Message);
            }
        }
    }
}
