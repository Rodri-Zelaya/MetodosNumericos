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
            if (string.IsNullOrWhiteSpace(txtFuncionMuller.Text) || string.IsNullOrWhiteSpace(txtX0.Text) ||
       string.IsNullOrWhiteSpace(txtX1.Text) || string.IsNullOrWhiteSpace(txtX2.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }
            try
            {
                string funcion = txtFuncionMuller.Text;
                double x0 = double.Parse(txtX0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double x1 = double.Parse(txtX1.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double x2 = double.Parse(txtX2.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                string raizEncontrada = metodos.Muller(funcion, x0, x1, x2, tol, dgvMuller);
                lblRaiz.Text = "Raíz: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica los datos bro: " + ex.Message);
            }
        }
    }
}
