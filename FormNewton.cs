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
    public partial class FormNewton : Form
    {
        public FormNewton()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFuncionNewton.Text) || string.IsNullOrWhiteSpace(txtDerivada.Text) ||
        string.IsNullOrWhiteSpace(txtVI.Text) || string.IsNullOrWhiteSpace(txtTolNewton.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos de Newton-Raphson.");
                return;
            }

            try
            {
                string funcion = txtFuncionNewton.Text;
                string derivada = txtDerivada.Text;
                double x0 = double.Parse(txtVI.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolNewton.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                int maxIter = int.Parse(txtMaxIterNewton.Text);

                MetodosNumericos metodos = new MetodosNumericos();
                metodos.NewtonRaphson(funcion, derivada, x0, tol, maxIter, dgvNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Revisa la sintaxis de la función o los valores: " + ex.Message);
            }
        }
    }
}
