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
    public partial class FormSecante : Form
    {
        public FormSecante()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFuncionSecante.Text) || string.IsNullOrWhiteSpace(txtVI.Text) ||
        string.IsNullOrWhiteSpace(txtV2.Text) || string.IsNullOrWhiteSpace(txtTolSecante.Text))
            {
                MessageBox.Show("Llena todos los campos. Usa X0 para C0 y X1 para C1.");
                return;
            }

            try
            {
                string funcion = txtFuncionSecante.Text;
                double c0 = double.Parse(txtVI.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double c1 = double.Parse(txtV2.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolSecante.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                int maxIter = int.Parse(txtMaxIterSecante.Text);

                MetodosNumericos metodos = new MetodosNumericos();
                metodos.Secante(funcion, c0, c1, tol, maxIter, dgvSecante);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los datos bro: " + ex.Message);
            }
        }
    }
}
